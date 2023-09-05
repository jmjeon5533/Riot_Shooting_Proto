using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricLine : AbilityBase, IListener
{
    [SerializeField] float curCooltime = 0;
    [SerializeField] int maxCooltime;

    [SerializeField] int defaultDamage;
    [SerializeField] float duration;

    [SerializeField] List<GameObject> beams;

    [SerializeField] int increaseValue;
    [SerializeField] float damageRate;


    public override void Ability()
    {
        if(!useSkill)
        {
            curCooltime = 0;
            minCool = curCooltime;
            maxCool = maxCooltime;
            useSkill = true;
            var b = Instantiate(beams[level-1], player.transform.position, Quaternion.Euler(0,90,0)).GetComponent<ElectricBeam>();
            b.damage = defaultDamage + (int)(player.damage * damageRate);
        }    
 
    }

    public override string GetStatText()
    {
        if ((level + 1) == 3 || (level + 1) == 5)
        {
            return "스킬 지속 시간 " + duration + "s → " + (beams[level - 1].GetComponent<ElectricBeam>().duration) +
           "스킬 데미지 " + defaultDamage + " → " + (defaultDamage + (int)(increaseValue / 2 * Mathf.Pow((1 + 0.1f), level)));
        }
        else
        {
            return 
            "스킬 데미지 " + defaultDamage + " → " + (defaultDamage + (int)(increaseValue / 2 * Mathf.Pow((1 + 0.1f), level)));


        }

       
    }

    public void OnEvent(Event_Type type, Component sender, object param = null)
    {
        if (type == Event_Type.PlayerAttack)
        {
           
        }
    }

    public override void LevelUp()
    {
        base.LevelUp();
        //defaultDamage += (int)(increaseValue * Mathf.Pow((1 + 0.2f), level));
        duration = beams[level - 1].GetComponent<ElectricBeam>().duration;
        defaultDamage += (int)(increaseValue / 2 * Mathf.Pow((1 + 0.1f), level));
    }

    // Start is called before the first frame update
    public override void Start()
    {
        //EventManager.Instance.AddListener(Event_Type.PlayerAttack, this);
        Initalize();
        duration = beams[level - 1].GetComponent<ElectricBeam>().duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (useSkill)
        {
            curCooltime += Time.deltaTime;
            minCool = curCooltime;
            maxCool = maxCooltime;
            if (curCooltime >= maxCooltime)
            {
                curCooltime = 0;
                useSkill = false;
            }
        }
    }
}
