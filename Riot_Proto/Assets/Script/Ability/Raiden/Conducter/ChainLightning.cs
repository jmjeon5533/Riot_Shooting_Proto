using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : AbilityBase, IListener
{
    [SerializeField] float maxCooltime;
    [SerializeField] float curCooltime;

    [SerializeField] int defaultDamage;
    [SerializeField] float damageRate;

    [SerializeField] float increaseValue;

    [SerializeField] int maxAttack;

    [SerializeField] GameObject bullet;


    [SerializeField] Transform target;
    [SerializeField] List<Transform> targets;
    [SerializeField] LineRenderer line;

    bool isAttack = false;

    public override void Ability()
    {
        curCooltime += Time.deltaTime;
        if(curCooltime >= maxCooltime)
        {
            isAttack = true;
        }
    }

    

    public override string GetStatText()
    {
        return "스킬 데미지 " + defaultDamage + " → " + (defaultDamage + (int)(increaseValue * Mathf.Pow((1 + 0.2f), level)))
            + " 연쇄 공격 횟수 " + maxAttack + " → " + (maxAttack +1);
    }

    public override void LevelUp()
    {
        base.LevelUp();
        defaultDamage += +(int)(increaseValue * Mathf.Pow((1 + 0.2f), level));
        maxAttack++;
    }

    public void OnEvent(Event_Type type, Component sender, object param = null)
    {
        if(type == Event_Type.PlayerAttacked)
        {
            var enemy = param as EnemyBase;
            if(enemy != null && isAttack && GameManager.instance.curEnemys.Contains(enemy.gameObject))
            {
                curCooltime = 0;
                targets.Clear();
                target = enemy.transform;
                ChainAttack();
            }
        }
    }

    // Start is called before the first frame update
    public override void Start()
    {
        Initalize();
        EventManager.Instance.AddListener(Event_Type.PlayerAttacked, this);
    }

    void ChainAttack()
    {
        //line.enabled = true;
        isAttack = false;
        Transform prevTarget = player.transform;
        int damage = defaultDamage + (int)(player.damage * damageRate);
        targets.Add(target);
        for (int i = 0; i < maxAttack; i++)
        {
            
            if (target != null) prevTarget = target;
            target = (GetNearbyEnemy(prevTarget).transform);
            if (prevTarget == target) break;
            Debug.Log(target.name);
            targets.Add(target);
        }
        line.positionCount = targets.Count + 1;
        line.SetPosition(0, player.transform.position);

        for (int i = 0; i < targets.Count; i++)
        {
            line.SetPosition(i + 1, target.position);
            var enemy = target.GetComponent<EnemyBase>();
            enemy.Damage((Random.Range(0, 100f) <= player.CritRate)
                    ? (int)(damage * player.CritDamage) : damage);

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
                line.SetPosition(0, transform.position);
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
        //line.enabled = false;       
    }

    // Update is called once per frame
    void Update()
    {
        Ability();
    }
}
