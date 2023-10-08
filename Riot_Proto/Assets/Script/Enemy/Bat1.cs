using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat1 : EnemyBase
{
    [SerializeField] Animator anim;
    protected override void Attack()
    {
        StartCoroutine(AttackMove());
    }
    IEnumerator AttackMove()
    {
        var g = GameManager.instance;
        var x = Random.Range(0,g.MoveRange.x + g.MovePivot.x);
        var y = Random.Range(-g.MoveRange.y + g.MovePivot.y, g.MoveRange.y + g.MovePivot.y);
        MovePos = new Vector3(x,y,0);

        yield return new WaitUntil(()=>Vector3.Distance(transform.position,MovePos) <= 0.1f);
        var b = PoolManager.Instance.GetObject("EnemyBullet",transform.position,Quaternion.identity).GetComponent<BulletBase>();
        b.dir = (GameManager.instance.player.transform.position - transform.position).normalized;
    }
    protected override void Dead()
    {
        base.Dead();
        anim.SetBool("Death",IsDeath());
    }
}
