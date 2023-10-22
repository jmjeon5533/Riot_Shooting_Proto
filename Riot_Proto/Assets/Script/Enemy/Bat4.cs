using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat4 : EnemyBase
{
    float moveRate;
    [SerializeField] Animator anim;
    float randA,randB, randC, firevalue;
    bool isfire;
    protected override void Attack()
    {

    }
    public override void Init()
    {
        moveRate = 0;
        isfire = false;
        ItemAddCount = 0.2f;
        StatMultiplier();
        transform.position = new Vector3(15, 4, 0);
        randA = Random.Range(18f,13f);
        randC = Random.Range(3f, 5f);
        randB = Random.Range(-11f,-4f);
        firevalue = Random.Range(0.3f,0.7f);
    }
    protected override void Move()
    {
        moveRate += Time.deltaTime * Random.Range(0.5f, 1f);
        var ab = Vector2.Lerp(new Vector2(randA, randC), new Vector2(randB,0), moveRate);
        var bc = Vector2.Lerp(new Vector2(randB,0), new Vector2(randA, -randC), moveRate);
        var abbc = Vector2.Lerp(ab, bc, moveRate);
        transform.position = abbc;
        if (moveRate >= 1 && !isDeath)
        {
            PoolManager.Instance.PoolObject(EnemyTag, gameObject);
            GameManager.instance.curEnemys.Remove(gameObject);
            isDeath = true;
        }
        if(moveRate >= firevalue && !isfire) 
        {
            var b = PoolManager.Instance.GetObject("EnemyBullet",transform.position,Quaternion.identity).GetComponent<BulletBase>();
            b.dir = (GameManager.instance.player.transform.position - transform.position).normalized;
            isfire = true;
        }
    }
    protected override void Update()
    {
        base.Update();

        if ((Mathf.Abs(transform.position.x) >= GameManager.instance.MoveRange.x + 5
        || Mathf.Abs(transform.position.y) >= GameManager.instance.MoveRange.y + 5)
        && !isDeath)
        {
            isDeath = true;
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
