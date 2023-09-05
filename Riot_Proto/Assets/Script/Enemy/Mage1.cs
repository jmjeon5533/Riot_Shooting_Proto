using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage1 : EnemyBase
{
    [SerializeField] Animator anim;
    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }
    protected override void Start()
    {
        base.Start();
        var g = GameManager.instance;

        var x = Random.Range(0, g.MoveRange.x + g.MovePivot.x);
        var y = Random.Range(-g.MoveRange.y + g.MovePivot.y, g.MoveRange.y + g.MovePivot.y);
        transform.position = new Vector3(x, y, 0);
    }
    protected override void Move()
    {
        
    }
    IEnumerator AttackCoroutine()
    {
        var g = GameManager.instance;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.3f);

        var b = PoolManager.Instance.GetObject("EnemyBullet",transform.position,Quaternion.identity).GetComponent<BulletBase>();
        b.dir = (GameManager.instance.player.transform.position - transform.position).normalized;
        
        yield return new WaitForSeconds(2);
        var x = Random.Range(0, g.MoveRange.x + g.MovePivot.x);
        var y = Random.Range(-g.MoveRange.y + g.MovePivot.y, g.MoveRange.y + g.MovePivot.y);
        transform.position = new Vector3(x, y, 0);
    }
}
