using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat2 : EnemyBase
{
    [SerializeField] Animator anim;
    protected override void Attack()
    {
        var b = PoolManager.Instance.GetObject("EnemyBullet",transform.position,Quaternion.identity).GetComponent<BulletBase>();
        b.dir = (GameManager.instance.player.transform.position - transform.position).normalized;
    }
    protected override void Dead()
    {
        base.Dead();
        anim.SetBool("Death",IsDeath());
    }
}
