using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBullet : BulletBase
{
    public int maxAttack;
    
    [SerializeField] Transform target;
    [SerializeField] List<Transform> targets;
    [SerializeField] LineRenderer line;

    Player player;

    bool isAttack = false;

    protected override void Start()
    {
        dir = Vector3.right;
        player = GameManager.instance.player;
    }

    void ChainAttack()
    {

        isAttack = true;
        Transform prevTarget = transform;
       
        for (int i = 0; i < maxAttack; i++)
        {
            if (target != null) prevTarget = target;
            target = (GetNearbyEnemy(prevTarget).transform);
            if (prevTarget == target) break;
            targets.Add(target);
        }
        line.positionCount = targets.Count + 1;
        line.SetPosition(0, player.transform.position);
        for (int i = 0; i < targets.Count; i++)
        {
            line.SetPosition(i + 1, target.position);
            var enemy = target.GetComponent<EnemyBase>();
            enemy.Damage((Random.Range(0, 100f) <= player.CritRate)
                    ? (int)(Damage * player.CritDamage) : Damage);

        }
        StartCoroutine(Delay());
    }

    private GameObject GetNearbyEnemy(Transform origin)
    {


        GameObject nearbyEnemy = origin.gameObject;
        float distance = Mathf.Infinity;
        foreach (GameObject enemy in GameManager.instance.curEnemys)
        {
            if (target == enemy || targets.Contains(enemy.transform)) continue;
            float newDist = Vector3.Distance(origin.position, enemy.transform.position);
            if (newDist <= distance)
            {
                nearbyEnemy = enemy;
                distance = newDist;
            }
        }
        return nearbyEnemy;
    }

    IEnumerator Delay()
    {
        float time = 0;

        if (targets != null && targets.Count > 0)
        {
            while (time < 0.3f)
            {
                int count = 0;
                line.SetPosition(0, player.transform.position);
                for (int i = 0; i < targets.Count; i++)
                {
                    if (targets[i] == null) continue;
                    line.SetPosition(i + 1, targets[i].transform.position);


                }
                yield return null;
                time += Time.deltaTime;
            }
        }
        line.positionCount = 0;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        var hit = Physics.OverlapSphere(transform.position, radius);

        if (isAttack) return;
        foreach (var h in hit)
        {
            if (h.CompareTag("Enemy"))
            {
                h.GetComponent<EnemyBase>().Damage((Random.Range(0, 100f) <= player.CritRate)
                    ? (int)(Damage * player.CritDamage) : Damage);
                ChainAttack();
                break;
            }
        }
    }
}
