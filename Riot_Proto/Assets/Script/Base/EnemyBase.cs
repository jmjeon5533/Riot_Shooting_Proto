using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public float HP;
    [HideInInspector] public float baseHp;
    [Space(10)]
    public float XPRate;
    [HideInInspector] public float baseXPRate;
    [Space()]
    public float MoveSpeed;
    public float AttackCooltime;
    private float AttackCurtime;

    public float damagedMultiplier = 1;

    public Vector3 MovePos;
    public string EnemyTag;

    protected bool isAttack = false;

    [SerializeField] List<BuffBase> EnemyBuffList = new List<BuffBase>();

    protected virtual void Start()
    {
        var g = GameManager.instance;
        var x = Random.Range(0, g.MoveRange.x + g.MovePivot.x);
        var y = Random.Range(-g.MoveRange.y + g.MovePivot.y, g.MoveRange.y + g.MovePivot.y);
        MovePos = new Vector3(x, y, 0);
        InitStat();
        StatMultiplier();
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
        XPRate = Mathf.Round(p * baseXPRate);
    }

    public void AddBuff(BuffBase buff) 
    {
        BuffBase _buff = buff;
        if(!CheckBuff(_buff))
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
            if(buff.GetBuffClass().Equals(_buff.GetBuffClass()))
            {
                return false;
            }
        }
        return true;
    }

    protected void BuffTimer()
    {
        List<BuffBase> list = null; 
        if(EnemyBuffList.Count > 0)
        {
            list = new List<BuffBase>();    
            for(int i = 0; i < EnemyBuffList.Count; i++)
            {
                EnemyBuffList[i].Run();
                if(EnemyBuffList[i].IsOnTimer())
                {
                    EnemyBuffList[i].End();
                    list.Add(EnemyBuffList[i]);
                }
            }
        }
        if(list != null && list.Count > 0)
        {
            for(int i = 0; i < list.Count;i++)
            {
                EnemyBuffList.Remove(list[i]);
            }
        }
    } 

   
    protected virtual void Update()
    {
        BuffTimer();
        if (AttackCurtime >= AttackCooltime)
        {
            AttackCurtime -= AttackCooltime;
            if(IsInScreen())
                Attack();
        }
        else
        {
            if(!isAttack)
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
        if(transform.position.x <= -10) return false;
        else return true;
    }

    public virtual void Damage(int damage, bool isCrit)
    {
        if (IsSpawning()) return;
        HP -= damage * damagedMultiplier;
        if (HP <= 0)
        {
            GameManager.instance.curEnemys.Remove(this.gameObject);
            for (int i = 0; i < XPRate; i++)
            {
                Dead();
            }
            GameManager.instance.GetMoney += (int)(XPRate * 25);
            UIManager.instance.InitRate();
            PoolManager.Instance.PoolObject(EnemyTag, gameObject);
        }
        PoolManager.Instance.GetObject("Hit", transform.position, Quaternion.identity);
        var DamageTextPos = (Vector2)transform.position + (Random.insideUnitCircle * 2);
        var DmgText = PoolManager.Instance.GetObject("DamageText",UIManager.instance.canvas)
            .GetComponent<DamageText>();
        DmgText.rect.position = DamageTextPos;
        DmgText.text.text = damage.ToString();
        DmgText.timeCount = 1 + (damage * ((isCrit) ? 0.02f : 0.01f));
        DmgText.color = (isCrit) ? Color.red : Color.white;
        if(isCrit) DmgText.text.fontStyle = FontStyle.Bold;
        else DmgText.text.fontStyle = FontStyle.Normal;
    }
    protected virtual void Dead()
    {
        PoolManager.Instance.GetObject("XP", transform.position, Quaternion.identity);
    }
    protected abstract void Attack();

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Damage();
        }
    }
}
