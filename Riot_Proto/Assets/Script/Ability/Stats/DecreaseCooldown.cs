using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseCooldown : AbilityBase
{
    public override void Ability()
    {
        
    }

    public override string GetStatText()
    {
        return "";
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

    public override void Initalize()
    {
        base.Initalize();
    }

    public override void LevelUp()
    {
        base.LevelUp();
    }
}
