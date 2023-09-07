using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BulletBase
{
    protected override void Update()
    {
        base.Update();
        
        var hit = Physics.OverlapSphere(transform.position,radius);
        foreach (var h in hit)
        {
            if (h.CompareTag("Player"))
            {
                h.GetComponent<Player>().Damage();
                Destroy(gameObject);
            }
        }
    }
}
