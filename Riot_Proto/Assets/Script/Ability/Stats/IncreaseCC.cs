using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseCC : AbilityBase
{

    public int increaseValue;

    bool isOne = false;

    public override void Ability()
    {
        GameManager.instance.player.CritRate += increaseValue;
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

    // Update is called once per frame
    void Update()
    {
        //if (!isOne) Ability();
    }

    public override void LevelUp()
    {
        base.LevelUp();
        Ability();
    }

    public override string GetStatText()
    {
        return "ũ��Ƽ�� Ȯ�� " + GameManager.instance.player.CritRate + "% �� " + (GameManager.instance.player.CritRate + increaseValue) + "%";
    }
}
