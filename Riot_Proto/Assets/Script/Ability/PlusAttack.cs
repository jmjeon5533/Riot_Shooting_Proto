using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlusAttack : AbilityBase
{
    [SerializeField] float maxSkillCooltime;
    [SerializeField] float curSkillCooltime = 0;

    [SerializeField] GameObject bullet;

    public override void Ability()
    {
        if(Player.Instance.isAttack)
        {
            curSkillCooltime += Time.deltaTime;
            if(curSkillCooltime > maxSkillCooltime)
            {
                curSkillCooltime = 0;
                Instantiate(bullet,Player.Instance.transform.position,Quaternion.identity);
            }
        }
    }

    // Start is called before the first frame update
    public override void Start()
    {
        Initalize();
    }

    public override void Initalize()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ability();
    }
}
