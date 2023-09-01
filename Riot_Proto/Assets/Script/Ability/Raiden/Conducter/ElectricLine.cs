using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricLine : AbilityBase, IListener
{
    [SerializeField] int stack = 0;
    [SerializeField] int maxStack;

    [SerializeField] int defaultDamage;
    [SerializeField] float duration;

    [SerializeField] List<GameObject> beams;

    Player player;

    [SerializeField] int increaseValue;
    [SerializeField] float damageRate;


    public override void Ability()
    {
        if(!useSkill)
        {

            stack = 0;
            minCool = stack;
            maxCool = maxStack;
            useSkill = true;
            var b = Instantiate(beams[level-1], player.transform.position, Quaternion.Euler(0,90,0)).GetComponent<ElectricBeam>();
            b.damage = defaultDamage + (int)(player.damage * damageRate);
        }    
 
    }

    public override string GetStatText()
    {
        return "스킬 지속 시간 " + duration + "s → " + (beams[level - 1].GetComponent<ElectricBeam>().duration) +
            "s 필요 공격 횟수 " + maxStack + " → " + (maxStack - 1);
    }

    public void OnEvent(Event_Type type, Component sender, object param = null)
    {
        if (type == Event_Type.PlayerAttack)
        {
            stack++;
           
        }
    }

    public override void LevelUp()
    {
        base.LevelUp();
        //defaultDamage += (int)(increaseValue * Mathf.Pow((1 + 0.2f), level));
        duration = beams[level - 1].GetComponent<ElectricBeam>().duration;
        maxStack--;
    }

    // Start is called before the first frame update
    public override void Start()
    {
        EventManager.Instance.AddListener(Event_Type.PlayerAttack, this);
        player = GameManager.instance.player;
        duration = beams[level - 1].GetComponent<ElectricBeam>().duration;
    }

    // Update is called once per frame
    void Update()
    {
        minCool = stack;
        maxCool = maxStack;
        if (useSkill)
        {
            if(stack >= maxStack)
            {
                stack = 0;
                useSkill = false;
            }
        }
    }
}
