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
                float chance = Random.Range(0, 100f);
                h.GetComponent<EnemyBase>().Damage((chance <= CritRate)
                        ? (int)(Damage * CritDamage) : Damage, (chance <= CritRate) ? true : false);
                if (h != null && h.GetComponent<EnemyBase>() != null)
                    EventManager.Instance.PostNotification(Event_Type.PlayerAttacked, this, h.GetComponent<EnemyBase>());
                PoolManager.Instance.PoolObject(BulletTag,gameObject);
                break;
            }
        }
    }
}
