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
        
    }
}
