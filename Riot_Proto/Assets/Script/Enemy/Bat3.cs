using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat3 : EnemyBase
{
    public Vector3 movedir;

    float time = 0;

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
        time += Time.deltaTime;
        if (time > 10)
        {
            time = 0;
            PoolManager.Instance.PoolObject(EnemyTag, this.gameObject);
        }
        if (transform.position.x <= -GameManager.instance.MoveRange.x -8
        || Mathf.Abs(transform.position.y) >= GameManager.instance.MoveRange.y + 20)
        {
            GameManager.instance.curEnemys.Remove(gameObject);
            PoolManager.Instance.PoolObject(EnemyTag, gameObject);
        }
    }
    protected override void Dead()
    {
        base.Dead();
        anim.SetBool("Death",IsDeath());
    }
}
