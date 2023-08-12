using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseWeapon : AbilityBase
{
    Player player;
    
    public override void Ability()
    {
        player.bulletLevel++;
    }

    public override void Initalize()
    {
        player = GameManager.instance.player;

        Ability();
    }

    public override void LevelUp()
    {
        base.LevelUp();
        Ability();
    }

    public override string GetStatText()
    {
        return "무기 레벨 +" + 1;
    }

    // Start is called before the first frame update
    public override void Start()
    {
        
        Initalize();

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
