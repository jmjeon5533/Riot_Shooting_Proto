
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

    protected float maxCool;
    protected float minCool;

    public float GetMinCool()
    {
        return minCool;
    }

    public float GetMaxCool()
    {
        return maxCool;
    }

    public bool IsSkillCool()
    {
        return useSkill;
    }
 
    protected bool useSkill = false;

    public abstract string GetStatText();

    protected virtual int GetCalculateDamage(int value)
    {
        return (int)(GameManager.instance.player.damage * 0.5f + value);
    }
    

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
