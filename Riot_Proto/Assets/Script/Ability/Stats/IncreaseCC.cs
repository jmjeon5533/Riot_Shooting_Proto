using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseCC : AbilityBase
{

    public int increaseValue;

    bool isOne = false;

    public override void Ability()
    {
        GameManager.instance.player.CritRate += increaseValue * level;
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
        Ability();
        base.LevelUp();
    }
}
