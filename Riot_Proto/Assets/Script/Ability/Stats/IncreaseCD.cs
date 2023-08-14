using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseCD : AbilityBase
{

    public int increaseValue;

    bool isOne = false;

    public override void Ability()
    {
        GameManager.instance.player.CritDamage += increaseValue * level;
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
        return "크리티컬 데미지 " + GameManager.instance.player.CritDamage * 100 + "% → " + (GameManager.instance.player.CritDamage + (0.5f * Mathf.Pow((1 + 0.2f), level))) * 100 + "%";
    }
}
