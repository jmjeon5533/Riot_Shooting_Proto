
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    public enum AbilityType
    {
        Passive, Active, Stats
    }

    public AbilityType type;

    public Sprite skillImage;

    public string skillName;
    public string skillDescription;

    public virtual void Start()
    {
        
    }

    void Update()
    {
        Ability();
    }

    public virtual void Initalize()
    {

    }

    public abstract void Ability();
}
