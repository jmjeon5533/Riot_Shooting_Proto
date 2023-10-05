using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushBullet : BulletBase
{
    // Start is called before the first frame update
    private float duration;
    private float multiplier;

    public void Init(float duration, float multiplier, int Damage, float speed)
    {
        this.duration = duration;
        this.multiplier = multiplier;
        this.Damage = Damage;
        this.MoveSpeed = speed;

    }

    Player player;
    protected override void Start()
    {
        dir = transform.right;
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
                
                h.GetComponent<EnemyBase>().AddBuff(new DefDecrease(duration, h.gameObject, BuffBase.TargetType.Enemy,multiplier,BuffList.DefDecrease));
                PoolManager.Instance.PoolObject("RushBullet", this.gameObject);

            }
        }
    }
}
