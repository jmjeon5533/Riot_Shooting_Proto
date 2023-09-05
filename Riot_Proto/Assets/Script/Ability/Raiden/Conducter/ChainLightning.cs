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
        return "��ų ������ " + defaultDamage + " �� " + (defaultDamage + (int)(increaseValue * Mathf.Pow((1 + 0.2f), level)))
            + " ���� ���� Ƚ�� " + maxAttack + " �� " + (maxAttack +1);
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
