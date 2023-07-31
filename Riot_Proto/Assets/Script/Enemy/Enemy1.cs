using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : EnemyBase
{
    public GameObject Bullet;
    protected override void Attack()
    {
        StartCoroutine(AttackMove());
    }
    IEnumerator AttackMove()
    {
        var x = Random.Range(0f,10f);
        var y = Random.Range(-5f,5f);
        MovePos = new Vector3(x,y,0);

        yield return new WaitUntil(()=>Vector3.Distance(transform.position,MovePos) <= 0.1f);
        var b = Instantiate(Bullet,transform.position,Quaternion.identity).GetComponent<BulletBase>();
        b.dir = (GameManager.instance.player.position - transform.position).normalized;
    }
}
