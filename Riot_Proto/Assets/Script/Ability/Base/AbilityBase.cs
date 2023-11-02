
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    public enum AbilityType
    {
        Passive, Active, Stats
    }

    public int cardLevel = 1;

    public int level = 1;

    public AbilityType type;

    public Sprite skillImage;

    public SkillIcon skillIcon;

    public string skillName;
    public string skillDescription;

    public string stats;

    protected float maxCool;
    protected float minCool;

    protected static float subtractCool = 0;

    public static float SubtractCool { get { return subtractCool; } private set { subtractCool = value; } }

    protected static void SetSubtractCool(float value)
    {
        SubtractCool = value;
    }

    protected void ResetTimerUI(float value)
    {
        skillIcon.ResetTimer(value);
    }
         
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

    protected Player player;
 
    protected bool useSkill = false;

    public abstract string GetStatText();

    protected virtual int GetCalculateDamage(int value)
    {
        return (int)(GameManager.instance.player.damage * 0.5f + value);
    }
    

    public virtual void Start()
    {
        player = GameManager.instance.player;
    }

    public virtual void LevelUp()
    {
        level++;
    }

    void Update()
    {
       
    }

    protected virtual void ResizingCooldown()
    {

    }

    public virtual void Initalize()
    {
        player = GameManager.instance.player;
        if (type == AbilityType.Passive) ResetTimerUI(1);
    }

    public abstract void Ability();
}
