using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricFlow : AbilityBase
{
    [SerializeField] float maxCooltime;
    [SerializeField] float curCooltime;

    [SerializeField] int defaultDamage;

    [SerializeField] int increaseValue;
    [SerializeField] float damageRate;


    public override void ResizingCooldown()
    {
        maxCooltime = originCooltime - (originCooltime * SubtractCool);

        maxCool = maxCooltime;
    }

    public override void Ability()
    {
        curCooltime += Time.deltaTime;
        minCool = curCooltime;
        if (curCooltime >= maxCooltime)
        {
            curCooltime = 0;
            useSkill = false;
            ResetTimerUI(1);
            var lazer = PoolManager.Instance.GetObject("Lazer",GameManager.instance.player.transform).GetComponent<FlowLazer>();
            lazer.transform.rotation = Quaternion.Euler(0, -90, 0);
            lazer.Init(defaultDamage + (int)(player.damage * damageRate));
            useSkill = true;
        }
    }

    public override string GetStatText()
    {
        return $"������ {defaultDamage} �� {defaultDamage + (int)(increaseValue * Mathf.Pow((1 + 0.2f), level))}";
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        originCooltime = maxCooltime;
        useSkill = true;
        maxCool = maxCooltime;
    }

    public override void Initalize()
    {
        base.Initalize();
    }

    public override void LevelUp()
    {
        base.LevelUp();
        defaultDamage += (int)(increaseValue * Mathf.Pow((1 + 0.2f), level));
    }

    // Update is called once per frame
    void Update()
    {
        Ability();
    }
}