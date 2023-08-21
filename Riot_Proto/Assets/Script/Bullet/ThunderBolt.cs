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

    Vector3 startPos;
    Vector3 middlePos;

    

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
            targetPos = target.position;
        }
        time += Time.deltaTime * MoveSpeed;
        Vector3 up = (targetPos - startPos).normalized;
        //Vector3 middlePos = ((startPos + targetPos) / 2) + (up * power);
        Vector3 pos = GameManager.CalculateBezier(startPos, middlePos, targetPos, time);
        transform.LookAt(pos);
        transform.position = pos;
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
        if (closestEnemy == null) closestEnemy = transform;
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

                h.GetComponent<EnemyBase>().Damage((Random.Range(0, 100f) <= player.CritRate)
                    ? (int)(damage * player.CritDamage) : damage);
                Destroy(gameObject);
            }
        }
        
    }
}
