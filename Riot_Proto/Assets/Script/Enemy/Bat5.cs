using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat5 : EnemyBase
{
    Vector3 movedir;
    [SerializeField] Animator anim;
    protected override void Attack()
    {

    }
    protected override void Awake()
    {
        InitStat();
        //StatMultiplier();
    }
    protected override void Move()
    {
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
