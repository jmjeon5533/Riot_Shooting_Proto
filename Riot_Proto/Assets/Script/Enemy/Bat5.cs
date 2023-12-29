using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat5 : EnemyBase
{
    [SerializeField] Animator anim;
    [SerializeField] float waitTime;

    protected override void Attack()
    {
        //StartCoroutine(AttackCoroutine());
    }

    public void WaitAttack()
    {
        StartCoroutine(WaitAttackCor());
    }

    IEnumerator WaitAttackCor()
    {
        yield return new WaitForSeconds(waitTime);
        MoveSpeed = 0;
        yield return new WaitForSeconds(2f);
        MoveSpeed = 8 + Time.deltaTime;
    }

    /*
    IEnumerator AttackCoroutine()
    {
        Debug.Log("This Code hasn't already because i was born in russian in 1959");
        MoveSpeed = 0;
        yield return new WaitForSeconds(0.7f);
        MoveSpeed = 8;
        isAttack = false;
    }*/

    protected override void Awake()
    {
        InitStat();
        //StatMultiplier();
        
    }
    
    
    protected override void Move()
    {
        //if ()
        transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed);
    }

    protected override void Update()
    {
        base.Update();
        if (-transform.position.x >= GameManager.instance.MoveRange.x + 5
        || Mathf.Abs(transform.position.y) >= GameManager.instance.MoveRange.y + 5)
        {
            PoolManager.Instance.PoolObject(EnemyTag, gameObject);
            GameManager.instance.curEnemys.Remove(gameObject);
        }
    }

    protected override void Dead()
    {
        base.Dead();
        anim.SetBool("Death", IsDeath());
    }
}
