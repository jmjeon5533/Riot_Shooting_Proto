using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat3 : EnemyBase
{
    public Vector3 movedir;
    [SerializeField] Animator anim;
    protected override void Attack()
    {

    }
    public override void Init()
    {
        HP = baseHp;
        StatMultiplier();
    }
    protected override void Move()
    {
        transform.Translate(movedir * Time.deltaTime * MoveSpeed);
    }
    protected override void Update()
    {
        base.Update();
        if (Mathf.Abs(transform.position.x) >= GameManager.instance.MoveRange.x + 8
        || Mathf.Abs(transform.position.y) >= GameManager.instance.MoveRange.y + 8)
        {
            PoolManager.Instance.PoolObject(EnemyTag, gameObject);
            GameManager.instance.curEnemys.Remove(gameObject);
        }
    }
    protected override void Dead()
    {
        base.Dead();
        anim.SetBool("Death",IsDeath());
    }
}
