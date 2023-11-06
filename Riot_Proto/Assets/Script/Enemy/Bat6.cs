using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat6 : EnemyBase
{
    [SerializeField] Animator anim;
    public Vector2 sinLine;
    public float axisHorizon;
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
        transform.Translate(Vector2.left * Time.deltaTime * MoveSpeed);
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

        float moveValue = transform.position.x;
        transform.position = new Vector3(moveValue, axisHorizon + Mathf.Cos(moveValue * sinLine.x) * sinLine.y, 0);
    }
    protected override void Dead()
    {
        base.Dead();
        anim.SetBool("Death", IsDeath());
    }
}
