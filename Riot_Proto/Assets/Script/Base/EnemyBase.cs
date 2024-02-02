using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public float HP;
    public float baseHp;
    [Space(10)]
    public float XPRate;
    public float baseXPRate;
    [Space]
    public float MoveSpeed;
    public float AttackCooltime;
    protected float AttackCurtime;

    public float damagedMultiplier = 1;

    [SerializeField] Shader shader;

    public Renderer mesh;

    Collider col;

    public Vector3 MovePos;
    public string EnemyTag;
    public float ItemAddCount = 1;

    [SerializeField] protected bool isDeath = false;

    protected bool isAttack = false;

    [SerializeField] protected List<BuffBase> EnemyBuffList = new List<BuffBase>();

    protected Collider collider;

    public virtual void Init()
    {
        var g = GameManager.instance;
        var x = Random.Range(0, g.MoveRange.x + g.MovePivot.x);
        var y = Random.Range(-g.MoveRange.y + g.MovePivot.y, g.MoveRange.y + g.MovePivot.y);

        MovePos = new Vector3(x, y, 0);
        HP = baseHp;
        StatMultiplier();
    }

    protected virtual void Awake()
    {
        InitStat();
    }
    void Start()
    {
        collider = GetComponent<Collider>();
        Init();
    }
    public void MoveVecInit(Vector3 movePos)
    {
        MovePos = movePos;
    }

    private void OnEnable()
    {
        col = GetComponent<Collider>();
        col.enabled = true;
        isDeath = false;
    }

    protected void InitStat()
    {
        baseHp = HP;
        baseXPRate = XPRate;
    }
    public virtual void StatMultiplier()
    {
        var p = GameManager.instance.EnemyPower;
        HP = Mathf.Round(p * baseHp);
        XPRate = Mathf.Round(p/2 * baseXPRate);
    }

    public void AddBuff(BuffBase buff)
    {
        BuffBase _buff = buff;
        if (!CheckBuff(_buff))
        {
            List<BuffBase> list = new List<BuffBase>(EnemyBuffList);
            foreach (BuffBase b in list)
            {
                if (_buff.GetBuffClass().Equals(b.GetBuffClass()))
                {
                    b.Dupe(_buff.GetDuration());
                    return;
                }
            }
        }
        _buff.Start();
        EnemyBuffList.Add(_buff);
    }

    bool CheckBuff(BuffBase buff)
    {
        List<BuffBase> list = new List<BuffBase>(EnemyBuffList);
        foreach (BuffBase _buff in list)
        {
            if (buff.GetBuffClass().Equals(_buff.GetBuffClass()))
            {
                return false;
            }
        }
        return true;
    }

    protected void BuffTimer()
    {
        List<BuffBase> list = null;
        if (EnemyBuffList.Count > 0)
        {
            list = new List<BuffBase>();
            for (int i = 0; i < EnemyBuffList.Count; i++)
            {
                EnemyBuffList[i].Run();
                if (EnemyBuffList[i].IsOnTimer())
                {
                    EnemyBuffList[i].End();
                    list.Add(EnemyBuffList[i]);
                }
            }
        }
        if (list != null && list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                EnemyBuffList.Remove(list[i]);
            }
        }
    }


    protected virtual void Update()
    {
        if (isDeath) return;
        BuffTimer();
        if (AttackCurtime >= AttackCooltime)
        {
            AttackCurtime -= AttackCooltime;
            if (IsInScreen())
                Attack();
        }
        else
        {
            if (!isAttack)
                AttackCurtime += Time.deltaTime;
        }
        Move();
    }
    protected virtual void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, MovePos, MoveSpeed * Time.deltaTime);
    }

    protected bool IsSpawning()
    {
        if (transform.position.x <= 10) return false;
        else return true;
    }

    protected bool IsInScreen()
    {
        if (transform.position.x <= -10 || transform.position.x >= 10) return false;
        else return true;
    }

    public bool IsDeath()
    {
        return isDeath;
    }

    public virtual void Damage(int damage, bool isCrit, string hitTag = null)
    {
        if (IsSpawning() || IsDeath() || !IsInScreen() || !GameManager.instance.IsGame) return;
        HP -= damage * damagedMultiplier;
        if (HP <= 0)
        {
            isDeath = true;
            GameManager.instance.curEnemys.Remove(this.gameObject);
            StartCoroutine(DeadEffect());
            Dead();
            if (GameManager.instance.IsGame) GameManager.instance.GetMoney += (int)(XPRate * 1000);
            UIManager.instance.InitRate();
            for (int i = 0; i < EnemyBuffList.Count; i++)
            {

                EnemyBuffList[i].End();

            }
            col = GetComponent<Collider>();
            col.enabled = false;
            StartCoroutine(DeathMotion());
            Item();
        }
        GameManager.instance.GetMoney += Mathf.RoundToInt(damage * 50f);
        SoundManager.instance.SetAudio("Hit", SoundManager.SoundState.SFX, false, Random.Range(0.3f, 0.7f));
        if (hitTag != null)
        {
            PoolManager.Instance.GetObject(hitTag, transform.position, Quaternion.identity);
        }
        else
        {
            var x = Random.Range(-collider.bounds.size.x / 4, collider.bounds.size.x / 4);
            var y = Random.Range(-collider.bounds.size.y / 4, collider.bounds.size.y / 4);

            var rand = new Vector3(x, y, 0);
            if(isCrit) PoolManager.Instance.GetObject("CritHit", transform.position + rand, Quaternion.identity);
            else PoolManager.Instance.GetObject("Hit", transform.position + rand, Quaternion.identity);
        }
    }
    protected virtual void Item()
    {
        if (GameManager.instance.itemCoolCount >= 20)
        {

            PoolManager.Instance.GetObject("Power", transform.position, Quaternion.identity);
            GameManager.instance.itemCoolCount = 0;
        }
        else
        {
            GameManager.instance.itemCoolCount += ItemAddCount;
        }
    }
    protected virtual void Dead()
    {

    }
    IEnumerator DeathMotion()
    {

        if (mesh == null)
        {
            PoolManager.Instance.PoolObject(EnemyTag, gameObject);

        }
        else
        {

            mesh.material.shader = GameManager.instance.dissolveShader;
            //mesh.material.SetFloat("_Dissolve_Power", 0); 
            mesh.material.SetTexture("_NoiseTex", GameManager.instance.dissolveSprite);
            mesh.material.SetTextureOffset("_NoiseTex", new Vector2(0, 0));
            mesh.material.SetTextureScale("_NoiseTex", new Vector2(3, 3));
            float power = 0;

            while (power < 0.5f)
            {
                //Material mat = new Material(mesh.material);
                power += Time.deltaTime / 1.5f;
                mesh.material.SetFloat("_DissolvePower", power);

                //Debug.Log(mat.GetFloat("Dissolve_Power"));
                //mesh.material = mat;
                yield return null;
            }
            yield return new WaitForSeconds(0.05f);
            GameManager.instance.AddKillCount();
            //Debug.Log("Test");
            PoolManager.Instance.PoolObject(EnemyTag, gameObject);
        }
        isDeath = false;


    }

    protected virtual IEnumerator DeadEffect()
    {
        float XPcount;
        if(XPRate > 0)
        {
            XPcount = XPRate + GameManager.instance.CalculateAddValue(6);
        }
        else XPcount = 0;
        for (int i = 0; i < XPcount; i++)
        {
            var xp = PoolManager.Instance.GetObject("XP", transform.position, Quaternion.identity).GetComponent<XP>();
            xp.curtime = 0;
            yield return new WaitForSeconds(0.015f);
        }
    }
    protected abstract void Attack();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Damage();
        }
    }
}
