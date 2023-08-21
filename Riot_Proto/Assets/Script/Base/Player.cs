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
        }
        else
        {
            AttackCurtime += Time.deltaTime;
        }
    }
    protected abstract void Attack();
    protected void InitBullet(PlayerBullet bullet)
    {
        var b = bullet;
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
        GetComponent<CapsuleCollider>().enabled = false;
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
        while (Vector3.Distance(transform.position, new Vector3(-8, 0, 0)) >= 0.1f)
        {
            yield return null;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-8, 0, 0), 2 * Time.deltaTime);
        }
        IsMove = true;
        yield return new WaitForSeconds(2f);
        GetComponent<CapsuleCollider>().enabled = true;
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
