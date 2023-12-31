using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime2 : EnemyBase
{
    Vector3 movedir;
    [SerializeField] Animator anim;
    protected override void Attack()
    {

    }
    public override void Init()
    {
        StatMultiplier();
    }
    protected override void Move()
    {
        transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed);
    }

    protected override void Update()
    {
        base.Update();
        if (Mathf.Abs(transform.position.x) >= GameManager.instance.MoveRange.x + 5
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
