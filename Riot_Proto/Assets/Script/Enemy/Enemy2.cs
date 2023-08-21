using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : EnemyBase
{
    public GameObject Bullet;
    protected override void Attack()
    {
        var b = Instantiate(Bullet,transform.position,Quaternion.identity).GetComponent<BulletBase>();
        b.dir = (GameManager.instance.player.transform.position - transform.position).normalized;
    }
}
