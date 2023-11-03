using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticShield : AbilityBase, IListener
{
    [SerializeField] bool isDamaged = true;
    [SerializeField] float curCooltime;
    [SerializeField] float maxCooltime;

    [SerializeField] int increaseValue;

    float totalMinusCooltime = 0;    

    public override void Ability()
    {
        curCooltime += Time.deltaTime;
        minCool = curCooltime;
        if(curCooltime >= maxCooltime)
        {
            curCooltime = 0;
            isDamaged = false;
            GameManager.instance.player.ShieldOn();
            useSkill = false;
            ResetTimerUI(0);
        }
    }

    public override string GetStatText()
    {
        return $"ÄðÅ¸ÀÓ {maxCooltime} ¡æ {maxCooltime-2.5f} °¨¼Ò";
    }

    // Start is called before the first frame update
    public override void Start()
    {
        maxCool = maxCooltime;
        useSkill = true;
        EventManager.Instance.AddListener(Event_Type.PlayerDefend, this);
        base.Start();
        originCooltime = maxCooltime;
        Initalize();
    }

    public override void ResizingCooldown()
    {
        maxCooltime = originCooltime - (originCooltime * SubtractCool);
        maxCooltime -= totalMinusCooltime;
        maxCool = maxCooltime;
    }

    public override void Initalize()
    {
        base.Initalize();
    }

    public override void LevelUp()
    {
        base.LevelUp();
        maxCooltime -= 2.5f;
        totalMinusCooltime += 2.5f;
        maxCool = maxCooltime;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDamaged)
            Ability();
    }

    public void OnEvent(Event_Type type, Component sender, object param = null)
    {
        if(type == Event_Type.PlayerDefend && GameManager.instance.player.IsShield && !useSkill)
        {
            isDamaged = true;
            ResetTimerUI(1);
            useSkill = true;
            GameManager.instance.player.ShieldOff();
        }
    }
}
