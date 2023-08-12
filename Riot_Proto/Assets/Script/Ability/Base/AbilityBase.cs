
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    public enum AbilityType
    {
        Passive, Active, Stats
    }

    public int level = 1;

    public AbilityType type;

    public Sprite skillImage;

    public string skillName;
    public string skillDescription;

    public string stats;

    public abstract string GetStatText();
    

    public virtual void Start()
    {
        
    }

    public virtual void LevelUp()
    {
        level++;
    }

    void Update()
    {
       
    }

    public virtual void Initalize()
    {
        
    }

    public abstract void Ability();
}
