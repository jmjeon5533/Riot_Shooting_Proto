using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton1 : EnemyBase
{
    [SerializeField] Animator anim;
    [SerializeField] float spawnDelay;
    [SerializeField] float spawnTime;

    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        var g = GameManager.instance;
        isAttack = true;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.7f);

        for (int i = 0; i < 8; i++)
        {

            var b = PoolManager.Instance.GetObject("EnemyBullet", transform.position + (Vector3.up * 0.7f), Quaternion.identity).GetComponent<EnemyBullet>();
            b.dir = Vector3.left;
            b.SetMoveSpeed(i+1f);
        }
        yield return new WaitForSeconds(1f);
        MovePos = new Vector3(Random.Range(0, g.MoveRange.x / 2), Random.Range((-g.MoveRange.y + 1), (g.MoveRange.y - 1)), 0);
        isAttack = false;
    }

    protected override void Dead()
    {
        base.Dead();
        anim.SetBool("Death", IsDeath());
    }

    protected override void Move()
    {
        base.Move();
    }

    protected override void Start()
    {
        //base.Start();

    }
}
