using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat6 : EnemyBase
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

        float moveValue = -transform.position.y * 0.5f;
        //transform.position = new Vector3(transform, Mathf.Sin(moveValue) * 0.5f, 0);
    }
    protected override void Dead()
    {
        base.Dead();
        anim.SetBool("Death", IsDeath());
    }
}
