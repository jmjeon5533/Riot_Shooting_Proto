using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int HP;
    public int damage;

    public int CritRate;
    public int CritDamage = 2;

    public int bulletSpeed;

    public float MoveSpeed;

    public GameObject bulletPrefab;

    Vector3 MoveRange;
    Vector3 MovePivot;
    public float AttackCooltime;
    private float AttackCurtime;

    bool IsMove = false;
    void Start()
    {
        MoveRange = GameManager.instance.MoveRange;
        MovePivot = GameManager.instance.MovePivot;
        GameManager.instance.player = this;
        StartCoroutine(Started());
    }
    void Update()
    {
        if(IsMove)
        {
            Movement();
            Attack();
        }
    }

    void Attack()
    {
        if(AttackCurtime >= AttackCooltime)
        {
            AttackCurtime -= AttackCooltime;
            var b = Instantiate(bulletPrefab,transform.position,Quaternion.identity).GetComponent<PlayerBullet>();
            b.Damage = damage;
            b.dir = Vector3.right;
            b.CritRate = CritRate;
            b.CritDamage = CritDamage;
        }
        else
        {
            AttackCurtime += Time.deltaTime;
        }
    }

    public void Damage()
    {
        HP--;
        StartCoroutine(Dead());
    }

    IEnumerator Dead()
    {
        IsMove = false;
        var r = gameObject.AddComponent<Rigidbody>();
        GetComponent<CapsuleCollider>().enabled = false;
        r.AddForce(Vector3.left,ForceMode.Impulse);
        yield return new WaitForSeconds(2);
        Destroy(r);
        StartCoroutine(Spawned());
    }
    IEnumerator Started()
    {
        yield return StartCoroutine(Spawned());
        GameManager.instance.IsGame = true;
    }
    IEnumerator Spawned()
    {
        transform.position = new Vector3(-12,0,0);
        while(Vector3.Distance(transform.position,new Vector3(-8,0,0)) >= 0.1f)
        {
            yield return null;
            transform.position = Vector3.MoveTowards(transform.position,new Vector3(-8,0,0), 2 * Time.deltaTime);
        }
        IsMove = true;
        yield return new WaitForSeconds(2f);
        GetComponent<CapsuleCollider>().enabled = true;
    }

    void Movement()
    {
        Vector2 input
       = new Vector2(Input.GetAxisRaw("Horizontal"),
       Input.GetAxisRaw("Vertical"));

        transform.Translate(input * MoveSpeed * Time.deltaTime);

        transform.position
         = new Vector3(Mathf.Clamp(transform.position.x, (-MoveRange.x + MovePivot.x), (MoveRange.x + MovePivot.x)),
         Mathf.Clamp(transform.position.y, (-MoveRange.y + MovePivot.y), (MoveRange.y + MovePivot.y)));
    }


}
