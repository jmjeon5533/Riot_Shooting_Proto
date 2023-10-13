using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime1 : EnemyBase
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
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(2.5f);
        var b = PoolManager.Instance.GetObject("GravityBullet", transform.position - (Vector3.down * 0.3f), Quaternion.identity).GetComponent<GravityBullet>();
        b.SetMoveSpeed(700);
        b.SetGravity(3);
        b.Bounce();

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
}
