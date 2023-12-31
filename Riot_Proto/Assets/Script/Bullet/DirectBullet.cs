using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectBullet : BulletBase
{
    // Start is called before the first frame update

    Player player;
    protected override void Start()
    {
        dir = Vector2.right;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        var hit = Physics.OverlapSphere(transform.position, radius);
        player = GameManager.instance.player;
       
        foreach (var h in hit)
        {
            if (h.CompareTag("Enemy"))
            {
                float chance = Random.Range(0, 100f);
                h.GetComponent<EnemyBase>().Damage((chance <= player.CritRate)
                        ? (int)(Damage * player.CritDamage) : Damage, (chance <= player.CritRate) ? true : false);
               
                Destroy(this.gameObject);
            }
        }
    }
}
