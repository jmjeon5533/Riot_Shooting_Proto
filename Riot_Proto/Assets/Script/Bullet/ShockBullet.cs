using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockBullet : BulletBase
{
    Player player;

    [SerializeField] float curTime = 0;
    [SerializeField] float livingTime;

    protected override void Start()
    {
        dir = Vector2.right;
    }

    void UpdateLivingTime()
    {
        curTime += Time.deltaTime;
        if (curTime >= livingTime)
        {
           Destroy(gameObject);
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        UpdateLivingTime();
        var hit = Physics.OverlapSphere(transform.position, radius);
        player = GameManager.instance.player;

        foreach (var h in hit)
        {
            if (h.CompareTag("Enemy"))
            {

                h.GetComponent<EnemyBase>().Damage((Random.Range(0, 100f) <= player.CritRate)
                    ? (int)(Damage * player.CritDamage) : Damage);
                
            }
        }
    }
}
