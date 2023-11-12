using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamage : AbilityBase
{

    public int increaseValue;

    bool isOne = false;

    public override void Ability()
    {
        GameManager.instance.player.damage += (int)(increaseValue * Mathf.Pow((1 + 0.2f), level));
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
        return "µ¥¹ÌÁö " + GameManager.instance.player.damage + " ¡æ " + (GameManager.instance.player.damage + (int)(increaseValue * Mathf.Pow((1 + 0.1f), level+1)));
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
