using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseCooldown : AbilityBase
{
    public override void Ability()
    {
        SetSubtractCool(SubtractCool+0.05f);
        Debug.Log(SubtractCool);
        foreach(var c in AbilityCard.Instance.curAbilityList)
        {
            c.ResizingCooldown();
        }
    }

    public override string GetStatText()
    {
        return $"ÄðÅ¸ÀÓ {SubtractCool * 100}% ¡æ {(SubtractCool+0.05f) * 100}% °¨¼Ò";
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
        Ability();
    }

    public override void LevelUp()
    {
        base.LevelUp();
        Ability();
    }
}
