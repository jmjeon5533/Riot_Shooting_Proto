using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBolt : BulletBase
{
    // Start is called before the first frame update
    public Transform target;

    [SerializeField] float time;
    [SerializeField] float moveSpeed;

    [SerializeField] float damageRate;

    [SerializeField] float livingTime;

    [SerializeField] float power;

    

    Player player;
    protected override void Start()
    {
        base.Start();
        Destroy(gameObject, livingTime);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Vector3 targetPos;
        if(target != null) targetPos = target.position;
        else targetPos = transform.position;
        time += Time.deltaTime * moveSpeed;
        Vector3 up = (targetPos - transform.position).normalized;
        Vector3 pos = GameManager.CalculateBezier(transform.position, ((transform.position + target.position) / 2) + (up * power), targetPos, time);
        transform.LookAt(pos);
        transform.position = pos;
        Attack();

    }

    void Attack()
    {
        var hit = Physics.OverlapBox(transform.position, new Vector3(radius, 10, 2), Quaternion.identity);
        player = GameManager.instance.player;
        int damage = (int)(player.damage * damageRate);
        foreach (var h in hit)
        {
            if (h.CompareTag("Enemy"))
            {

                h.GetComponent<EnemyBase>().Damage((Random.Range(0, 100f) <= player.CritRate)
                    ? (int)(damage * player.CritDamage) : damage);

            }
        }
        
    }
}
