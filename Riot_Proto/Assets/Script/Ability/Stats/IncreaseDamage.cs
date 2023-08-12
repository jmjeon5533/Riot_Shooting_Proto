using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamage : AbilityBase
{

    public int increaseValue;

    bool isOne = false;

    public override void Ability()
    {
        GameManager.instance.player.damage += increaseValue * level;
    }

    // Start is called before the first frame update
    public override void Start()
    {
        Initalize();
    }

    public override void Initalize()
    {
        type = AbilityType.Stats;
        Ability();
    }

    public override string GetStatText()
    {
        return "µ¥¹ÌÁö +" + (int)(5 * Mathf.Pow((1 + 0.2f), level));
    }

    // Update is called once per frame
    void Update()
    {
        //if(!isOne) Ability();
    }
    public override void LevelUp()
    {
        base.LevelUp();
        Ability();
    }
}
