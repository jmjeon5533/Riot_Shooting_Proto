    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticField : AbilityBase
{
    [SerializeField] float curTime = 0;
    [SerializeField] float maxCooltime;

    [SerializeField] int defaultDamage;

    [SerializeField] float duration;
    [SerializeField] float delay;
    [SerializeField] int range;

    [SerializeField] GameObject field;

    [SerializeField] int increaseValue;
    [SerializeField] float damageRate;

    public override void Ability()
    {
        curTime += Time.deltaTime;
        if(curTime >= maxCooltime)
        {
            curTime = 0;
            var b = Instantiate(field,player.transform.position,Quaternion.identity).GetComponent<StaticZone>();
            b.Init(defaultDamage + (int)(player.damage * damageRate), range, duration, delay);
        }
    }

    public override string GetStatText()
    {
        return "��ų ������ " + defaultDamage + " �� " + (defaultDamage + (int)(increaseValue * Mathf.Pow((1 + 0.2f), level))) + 
            " ��ų ���� �ð� " + duration + "s �� " + (duration +1) + "s";
    }

    public override void LevelUp()
    {
        base.LevelUp();
        defaultDamage += (int)(increaseValue * Mathf.Pow((1 + 0.2f), level));
        duration++;
    }

    // Start is called before the first frame update
    public override void Start()
    {
        player = GameManager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        Ability();
    }
}