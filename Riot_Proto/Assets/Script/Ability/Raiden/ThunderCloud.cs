using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderCloud : AbilityBase
{

    [SerializeField] float speed;

    [SerializeField] GameObject cloud;

    [SerializeField] int defaultDamage;

    [SerializeField] float curCooltime = 0;
    [SerializeField] float maxCooltime;

    [SerializeField] float livingDuration;

    float totalMinusCooltime = 0;

    public override void Ability()
    {
        curCooltime+=Time.deltaTime;
        minCool = curCooltime;
        if(curCooltime > maxCooltime)
        {
            curCooltime=0;
            ResetTimerUI(1);
            Instantiate(cloud,GameManager.instance.player.transform.position, Quaternion.identity).GetComponent<Cloud>().Duration(livingDuration, speed,defaultDamage);
        }
    }

    // Start is called before the first frame update
    public override void Start()
    {
        useSkill = true;
        Initalize();
        maxCool = maxCooltime;
        originCooltime = maxCooltime;
    }

    public override void ResizingCooldown()
    {
        maxCooltime = originCooltime - (originCooltime * SubtractCool);
        maxCooltime -= totalMinusCooltime;
        maxCool = maxCooltime;
    }

    // Update is called once per frame
    void Update()
    {
        Ability();
    }

    public override void LevelUp()
    {
        base.LevelUp();
        defaultDamage += (int)(2 * Mathf.Pow((1 + 0.15f), level));
        totalMinusCooltime += (Mathf.Round(((0.4f * Mathf.Pow((1 + 0.1f), level))) * 100) / 100);
        maxCooltime -= (Mathf.Round(((0.4f * Mathf.Pow((1 + 0.1f), level))) * 100) / 100);
        maxCool = maxCooltime;
        
    }

    public override string GetStatText()
    {
        return "스킬 데미지 " + defaultDamage + " → " + (defaultDamage + (int)(2 * Mathf.Pow((1 + 0.15f), level+1)))
            + "\n스킬 쿨타임 " + maxCooltime + " → " + (Mathf.Round((maxCooltime - (0.4f * Mathf.Pow((1 + 0.1f), level+1))) * 100) / 100);
        

    }
}
