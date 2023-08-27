using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    public int HP;
    public int damage;

    public int CritRate;
    public float CritDamage = 2;

    public int bulletLevel = 1;
    public int bulletSpeed;

    public float MoveSpeed;

    public PlayerBullet[] bulletPrefab;
    CapsuleCollider capsuleCollider;
    Coroutine ShieldCoroutine;
    [SerializeField] GameObject ShieldObj;

    Vector3 MoveRange;
    Vector3 MovePivot;
    public float AttackCooltime;
    private float AttackCurtime;

    bool IsMove = false;

    Rigidbody rigid;
    Animator anim;
    Joystick joystick;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();  
        joystick = GameManager.instance.joystick;
        MoveRange = GameManager.instance.MoveRange;
        MovePivot = GameManager.instance.MovePivot;
        GameManager.instance.player = this;
        StartCoroutine(Started());
    }
    protected virtual void Update()
    {
        if (IsMove)
        {
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
        b.Damage = damage;
        b.dir = Vector3.right;
        b.CritRate = CritRate;
        b.CritDamage = CritDamage;
    }

    public void Damage()
    {
        HP--;
        StartCoroutine(Dead());
    }

    IEnumerator Dead()
    {
        IsMove = false;
        rigid.useGravity = true;
        capsuleCollider.enabled = false;
        rigid.AddForce(Vector3.left, ForceMode.Impulse);
        yield return new WaitForSeconds(2);
        rigid.useGravity = false;
        rigid.velocity = Vector3.zero;
        StartCoroutine(Spawned());
    }
    IEnumerator Started()
    {
        yield return StartCoroutine(Spawned());
        GameManager.instance.IsGame = true;
    }
    IEnumerator Spawned()
    {
        anim.SetInteger("MoveState",1);
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
        if(ShieldCoroutine != null) StopCoroutine(ShieldCoroutine);
        ShieldCoroutine = StartCoroutine(Protect(time));
    }
    IEnumerator Protect(float time)
    {
        capsuleCollider.enabled = false;
        ShieldObj.SetActive(true);
        yield return new WaitForSeconds(time);
        capsuleCollider.enabled = true;
        ShieldObj.SetActive(false);
    }
    void Movement()
    {
        Vector2 input = joystick.input.normalized;
        

        anim.SetInteger("MoveState",Mathf.RoundToInt(input.x));

        transform.Translate(input * MoveSpeed * Time.deltaTime);

        transform.position
         = new Vector3(Mathf.Clamp(transform.position.x, -MoveRange.x + MovePivot.x, MoveRange.x + MovePivot.x),
         Mathf.Clamp(transform.position.y, -MoveRange.y + MovePivot.y, MoveRange.y + MovePivot.y));
    }


}
