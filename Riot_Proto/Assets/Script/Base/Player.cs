using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{

    public int HP;
    public int MaxHP = 3;
    public int damage;

    public int CritRate;
    public float CritDamage = 2;

    public int bulletLevel = 1;
    public int bulletSpeed;

    public float MoveSpeed;


    public PlayerBullet[] bulletPrefab;
    public bool IsShield = false;
    bool isShieldOn = false;
    Coroutine ShieldCoroutine;
    [SerializeField] GameObject ShieldObj;

    [SerializeField] List<BuffBase> PlayerBuffList = new List<BuffBase>();

    Vector3 MoveRange;
    Vector3 MovePivot;
    public float AttackCooltime;
    private float AttackCurtime;

    bool IsMove = false;

    bool inv = false;

    Rigidbody rigid;
    Animator anim;
    Joystick joystick;

    Coroutine protect;
    Coroutine fadeOn;
    Coroutine fadeOff;

    protected virtual void Awake()
    {
        GameManager.instance.player = this;
    }
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        joystick = GameManager.instance.joystick;
        MoveRange = GameManager.instance.MoveRange;
        MovePivot = GameManager.instance.MovePivot;
        StartCoroutine(Started());
    }
    protected virtual void Update()
    {
        if (IsMove)
        {
            BuffTimer();
            Movement();
            AttackInput();
        }
    }

    void AttackInput()
    {
        if (AttackCurtime >= AttackCooltime)
        {
            AttackCurtime -= AttackCooltime;
            Attack();
            EventManager.Instance.PostNotification(Event_Type.PlayerAttack, this);
        }
        else
        {
            AttackCurtime += Time.deltaTime;
        }
    }
    protected abstract void Attack();
    protected void InitBullet(GameObject bullet)
    {
        var b = bullet.GetComponent<PlayerBullet>();
        b.MoveSpeed = bulletSpeed;
        b.Damage = damage;
        b.dir = Vector3.right;
        b.CritRate = CritRate;
        b.CritDamage = CritDamage;
    }

    public void Damage()
    {
        var g = GameManager.instance;
        if (IsShield)
        {
            EventManager.Instance.PostNotification(Event_Type.PlayerDefend,this);
            return;
        }
        HP--;
        if(bulletLevel >= 2)
        {
            bulletLevel--;
        }

        StartCoroutine(Dead());
        Instantiate(g.Bomb,g.player.transform.position,Quaternion.Euler(-90,0,0));
        UIManager.instance.InitHeart();
    }

    public void SetInvincibility(float time)
    {
        StartCoroutine(Invincibility(time));
    }

    IEnumerator Invincibility(float time)
    {
        inv = true;
        yield return new WaitForSeconds(time);
        inv = false;
    }

    IEnumerator Dead()
    {
        IsMove = false;
        rigid.useGravity = true;
        IsShield = true;
        rigid.AddForce(Vector3.left, ForceMode.Impulse);
        if (HP <= 0)
        {
            UIManager.instance.InitRate();
            UIManager.instance.UseOverTab();
            GameManager.instance.IsGame = false;
        }
        else
        {
            yield return new WaitForSeconds(1.3f);
            rigid.useGravity = false;
            rigid.velocity = Vector3.zero;
            StartCoroutine(Spawned());
        }
    }
    IEnumerator Started()
    {
        yield return StartCoroutine(Spawned());
        GameManager.instance.IsGame = true;
        UIManager.instance.InitHeart();
        UIManager.instance.ShowImg();
    }
    IEnumerator Spawned()
    {
        anim.SetInteger("MoveState", 1);
        transform.position = new Vector3(-12, 0, 0);
        Shield(3f);
        while (Vector3.Distance(transform.position, new Vector3(-8, 0, 0)) >= 0.1f)
        {
            yield return null;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-8, 0, 0), 2 * Time.deltaTime);
        }
        IsMove = true;
    }
    public void Shield(float time)
    {
        if(time < 0)
        {

        }
        if (isShieldOn) return;
        if (ShieldCoroutine != null) StopCoroutine(ShieldCoroutine);
        ShieldCoroutine = StartCoroutine(Protect(time));
    }

    public void ShieldOn()
    {
        IsShield = true;
        isShieldOn = true;
        ShieldObj.SetActive(true);
        var mesh = ShieldObj.GetComponent<MeshRenderer>();
        StopCoroutine(fadeOn);
        StopCoroutine(ShieldCoroutine);
        StopCoroutine(fadeOff);
        mesh.material.SetFloat("_Dissolve", 0.75f);
        StartCoroutine(FadeShield(false, mesh));
    }

    public void ShieldOff()
    {
       StartCoroutine(ShieldOffMotion());
        
    }
    IEnumerator ShieldOffMotion()
    {
        var mesh = ShieldObj.GetComponent<MeshRenderer>();
        yield return StartCoroutine(FadeShield(true, mesh));
        IsShield = false;
        isShieldOn = false;
    }

    IEnumerator Protect(float time)
    {

        
        IsShield = true;
        ShieldObj.SetActive(true);
        var mesh = ShieldObj.GetComponent<MeshRenderer>();
        mesh.material.SetFloat("_Dissolve", 0.75f);
        
        fadeOn = StartCoroutine(FadeShield(false, mesh));
        yield return new WaitForSeconds(time);
        fadeOff = StartCoroutine(FadeShield(true, mesh));
        yield return fadeOn;
        IsShield = false;
        
    }

    IEnumerator FadeShield(bool fade, MeshRenderer mesh)
    {
        float time = (fade ? 0 : 0.75f);
       if(fade)
        {
            while(time < 0.75f)
            {
                time += Time.deltaTime/1;
                mesh.material.SetFloat("_Dissolve", time);
                yield return null;  

            }
            ShieldObj.SetActive(false);
        } else
        {
            while (time > 0)
            {
                time -= Time.deltaTime / 1;
                mesh.material.SetFloat("_Dissolve", time);
                yield return null;
            }

        }
    }
    void Movement()
    {
        Vector2 input = joystick.Input;
        input = SceneManager.instance.DetailCtrl ? input : input.normalized ;

        anim.SetInteger("MoveState", Mathf.RoundToInt(input.x));

        transform.Translate(input * MoveSpeed * Time.deltaTime);
        PCMove();
        transform.position
         = new Vector3(Mathf.Clamp(transform.position.x, -MoveRange.x + MovePivot.x, MoveRange.x + MovePivot.x),
         Mathf.Clamp(transform.position.y, -MoveRange.y + MovePivot.y, MoveRange.y + MovePivot.y));
    }

    void PCMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector3((float)x, (float)y, 0) * MoveSpeed * Time.deltaTime * (Input.GetKey(KeyCode.LeftShift) ? 0.25f : 1));
    }

    public void AddBuff(BuffBase buff)
    {
        BuffBase _buff = buff;
        _buff.Start();
        PlayerBuffList.Add(_buff);
    }

    protected void BuffTimer()
    {
        List<BuffBase> list = null;
        if (PlayerBuffList.Count > 0)
        {
            list = new List<BuffBase>();
            for (int i = 0; i < PlayerBuffList.Count; i++)
            {
                PlayerBuffList[i].Run();
                if (PlayerBuffList[i].IsOnTimer())
                {
                    PlayerBuffList[i].End();
                    list.Add(PlayerBuffList[i]);

                }
            }
        }
        if (list != null && list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                PlayerBuffList.Remove(list[i]);
            }
        }
    }


}
