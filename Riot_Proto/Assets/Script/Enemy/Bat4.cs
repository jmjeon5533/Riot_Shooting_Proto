using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat4 : EnemyBase
{
    float moveRate;
    [SerializeField] Animator anim;
    protected override void Attack()
    {

    }
    protected override void Start()
    {
        ItemAddCount = 0.2f;
        InitStat();
        StatMultiplier();
        transform.position = new Vector3(15, 4, 0);
    }
    protected override void Move()
    {
        moveRate += Time.deltaTime * Random.Range(0.3f, 0.5f);
        var ab = Vector2.Lerp(new Vector2(15,4),Vector2.zero,moveRate);
        var bc = Vector2.Lerp(Vector2.zero,new Vector2(15,-4),moveRate);
        var abbc = Vector2.Lerp(ab,bc,moveRate);
        transform.position = abbc;
        if(moveRate >= 1) PoolManager.Instance.PoolObject(EnemyTag,gameObject);
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
        anim.SetBool("Death",IsDeath());
    }
}
