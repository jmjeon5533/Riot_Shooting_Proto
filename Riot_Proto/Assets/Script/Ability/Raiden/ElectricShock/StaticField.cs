    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticField : AbilityBase
{
    [SerializeField] float curTime = 0;
    [SerializeField] float maxCooltime;

    [SerializeField] int defaultDamage;

    [SerializeField] float duration;
    [SerializeField] float delay;
    [SerializeField] int range;

    [SerializeField] GameObject field;

    [SerializeField] int increaseValue;
    [SerializeField] float damageRate;

    public override void Ability()
    {
        curTime += Time.deltaTime;
        minCool = curTime;
        if(curTime >= maxCooltime)
        {
            useSkill = false;
            curTime = 0;
            minCool = curTime;
            ResetTimerUI(1);
            var b = Instantiate(field,player.transform.position,Quaternion.identity).GetComponent<StaticZone>();
            b.Init(defaultDamage + (int)(player.damage * damageRate), range, duration, delay);
            useSkill = true;
            
        }
    }

    public override string GetStatText()
    {
        return "스킬 데미지 " + defaultDamage + " → " + (defaultDamage + (int)(increaseValue * Mathf.Pow((1 + 0.2f), level+1))) + 
            " 스킬 지속 시간 " + duration + "s → " + (duration +1) + "s";
    }

    public override void LevelUp()
    {
        base.LevelUp();
        defaultDamage += (int)(increaseValue * Mathf.Pow((1 + 0.2f), level));
        duration++;

    }

    // Start is called before the first frame update
    public override void Start()
    {
        player = GameManager.instance.player;
        useSkill = true;
        maxCool = maxCooltime;
        originCooltime = maxCooltime;
        Initalize();
    }

    // Update is called once per frame
    void Update()
    {
        Ability();
    }
}
