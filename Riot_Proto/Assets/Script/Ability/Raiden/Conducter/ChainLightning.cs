using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : AbilityBase
{
    [SerializeField] float maxCooltime;
    [SerializeField] float curCooltime;

    [SerializeField] int defaultDamage;
    [SerializeField] float damageRate;

    [SerializeField] float increaseValue;

    [SerializeField] int maxAttack;

    [SerializeField] GameObject bullet;         

    



    public override void Ability()
    {
        curCooltime += Time.deltaTime;
        if(curCooltime >= maxCooltime)
        {
            curCooltime = 0;
           
            var b = Instantiate(bullet, player.transform.position,Quaternion.identity).GetComponent<ChainBullet>();
            b.Damage = (defaultDamage + (int)(player.damage * damageRate));
            b.maxAttack = maxAttack;
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

    // Start is called before the first frame update
    public override void Start()
    {
        Initalize();
    }

   
         
    // Update is called once per frame
    void Update()
    {
        Ability();
    }
}
