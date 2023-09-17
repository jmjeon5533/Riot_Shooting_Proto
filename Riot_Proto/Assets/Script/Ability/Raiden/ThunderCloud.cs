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

    public override void Ability()
    {
        curCooltime+=Time.deltaTime;
        if(curCooltime > maxCooltime)
        {
            curCooltime=0;
            Instantiate(cloud,GameManager.instance.player.transform.position, Quaternion.identity).GetComponent<Cloud>().Duration(livingDuration, speed,defaultDamage);
        }
    }

    // Start is called before the first frame update
    public override void Start()
    {
        
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
        maxCooltime -= (0.5f * Mathf.Pow((1 + 0.1f), level));
        
    }

    public override string GetStatText()
    {
        return "스킬 데미지 " + defaultDamage + " → " + (defaultDamage + (int)(2 * Mathf.Pow((1 + 0.15f), level)))
            + "\n스킬 쿨타임 " + maxCooltime + " → " + (maxCooltime - (0.5f * Mathf.Pow((1 + 0.1f), level)));
        

    }
}
