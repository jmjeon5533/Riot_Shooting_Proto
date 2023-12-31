using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAS : AbilityBase
{

    public float increaseValue;

    bool isOne = false;

    public override void Ability()
    {
        GameManager.instance.player.AttackCooltime -= Mathf.Round((GameManager.instance.player.AttackCooltime * (increaseValue * level)) * 100) / 100;
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

    public override void LevelUp()
    {
        base.LevelUp();
        Ability();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!isOne) Ability();
    }

    public override string GetStatText()
    {
        return "���� �ֱ� " + Mathf.Round((1 / GameManager.instance.player.AttackCooltime) * 100) / 100 + "/s �� " +  (Mathf.Round((1 / ((GameManager.instance.player.AttackCooltime - (GameManager.instance.player.AttackCooltime * (increaseValue * level)))) * 100)) / 100) + "/s";
    }
}
