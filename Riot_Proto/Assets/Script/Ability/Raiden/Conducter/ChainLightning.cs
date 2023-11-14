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
        minCool = curCooltime;
        if(curCooltime >= maxCooltime)
        {
            isAttack = true;
            useSkill = false;
            ResetTimerUI(0);
        }
    }

    public override void ResizingCooldown()
    {
        maxCooltime = originCooltime - (originCooltime * SubtractCool);
        maxCool = maxCooltime;
    }

    public override string GetStatText()
    {
        return "스킬 데미지 " + defaultDamage + " → " + (defaultDamage + (int)(increaseValue * Mathf.Pow((1 + 0.2f), level+1)))
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
                ResetTimerUI(1);
                useSkill = true;
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
        maxCool = maxCooltime;
        curCooltime = maxCooltime-1;
        minCool = curCooltime;
        useSkill = true;
        originCooltime = maxCooltime;
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
        line.positionCount = 2;
        //line.SetPosition(0, player.transform.position);

        for (int i = 0; i < 1; i++)
        {

            line.SetPosition(0, target.position);
            var enemy = target.GetComponent<EnemyBase>();
            float chance = Random.Range(0, 100f);
            enemy.Damage((chance <= player.CritRate)
                    ? (int)(damage * player.CritDamage) : damage, (chance <= player.CritRate) ? true : false);

        }
        StartCoroutine(Delay());
    }

    private GameObject GetNearbyEnemy(Transform origin)
    {


        GameObject nearbyEnemy = origin.gameObject;
        float distance = Mathf.Infinity;
        foreach (GameObject enemy in GameManager.instance.curEnemys)
        {
            if (target == enemy || targets.Contains(enemy.transform) || target.GetComponent<Alert>() != null) continue;
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
        Transform _t = targets[0];
        if (targets != null && targets.Count > 0)
        {
            
                bool isSound = false;
                int count = 0;
                for (int i = 0; i < targets.Count; i++)
                {
                    time = 0;
                    while(time < 0.55f)
                    {
                        line.SetPosition(0, _t.position);
                        if (targets[i] == null) continue;
                        if(!isSound)
                        {
                            //SoundManager.instance.SetAudio("ChainLightning_Contact",SoundManager.SoundState.SFX,false); isSound = true;

                        }
                        line.SetPosition(1, targets[i].transform.position);
                        _t = targets[i];
                        time += Time.deltaTime;
                        yield return null;
                    }
                    isSound = false;
                    //yield return new WaitForSeconds(0.1f);
                }
                yield return null;
                time += Time.deltaTime;
            
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
