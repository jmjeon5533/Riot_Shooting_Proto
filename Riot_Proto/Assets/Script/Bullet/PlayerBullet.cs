using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BulletBase
{
    protected override void Update()
    {
        base.Update();
        var hit = Physics.OverlapSphere(transform.position, radius);
        foreach (var h in hit)
        {
            if (h.CompareTag("Enemy"))
            {
                h.GetComponent<EnemyBase>().Damage((Random.Range(0,100f) <= CritRate) 
                    ? (int)(Damage * CritDamage) : Damage);
                Destroy(gameObject);
                
            }
        }
    }
}
