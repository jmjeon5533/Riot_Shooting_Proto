using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBolt : BulletBase
{
    // Start is called before the first frame update
    public Transform target;

    [SerializeField] float time;
    

    [SerializeField] float damageRate;

    [SerializeField] float livingTime;

    [SerializeField] float power;

    bool isAttack = false;

    Vector3 startPos;
    Vector3 middlePos;

    Vector3 prevPos;

    

    Player player;
    protected override void Start()
    {
        base.Start();
        Destroy(gameObject, livingTime);
        startPos = transform.position;
        middlePos = transform.position + ((Vector3)Random.insideUnitCircle * power) + (transform.right * -1 * power);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Vector3 targetPos;
        if(target != null) targetPos = target.position;
        else
        {
            target = FindClosestEnemy();
            if (target == transform)
            {
                targetPos = (prevPos - transform.position).normalized;
                Destroy(gameObject);
            }
            else
                targetPos = target.position;
        }
        time += Time.deltaTime * MoveSpeed;
        prevPos = targetPos;
        Vector3 up = (targetPos - startPos).normalized;
        //Vector3 middlePos = ((startPos + targetPos) / 2) + (up * power);
        Vector3 pos = GameManager.CalculateBezier(startPos, middlePos, targetPos, time);
        transform.LookAt(pos);
        transform.position = pos;
        if(!isAttack)
            Attack();

    }

    public Transform FindClosestEnemy()
    {
        Transform closestEnemy = null;
        float closestDistanceSqr = Mathf.Infinity;
        
        foreach (var enemyTransform in GameManager.instance.curEnemys)
        {
            Vector3 directionToEnemy = enemyTransform.transform.position - GameManager.instance.player.transform.position;
            float distanceSqrToEnemy = directionToEnemy.sqrMagnitude;

            if (distanceSqrToEnemy < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqrToEnemy;
                closestEnemy = enemyTransform.transform;
            }
        }
        if (closestEnemy == null) Destroy(gameObject);
        return closestEnemy;
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

                float chance = Random.Range(0, 100f);
                h.GetComponent<EnemyBase>().Damage((chance <= player.CritRate)
                        ? (int)(damage * player.CritDamage) : damage, (chance <= player.CritRate) ? true : false, "KaisaHit");
                isAttack = true;
                Destroy(gameObject, 1);
            }
        }
        
    }
}
