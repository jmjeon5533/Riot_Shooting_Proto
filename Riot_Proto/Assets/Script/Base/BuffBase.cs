using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    Buff, Debuff
}

public enum BuffList
{
    Slow, DefDecrease
}

public abstract class BuffBase
{

    protected float curTime = 0;
    protected float duration;

    protected Player player;
    protected EnemyBase enemy;

    protected BuffList buff;

    public enum TargetType
    {
        Player, Enemy
    }

    public bool IsOnTimer()
    {
        if (curTime >= duration)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public BuffList GetBuffClass()
    {
        
        return buff;
    }

    public float GetDuration()
    {
        return duration;
    }

    protected BuffType buffType;

    protected GameObject target;

    protected TargetType type;
    
    public BuffBase(float duration, GameObject target, TargetType type, BuffList buff)
    {
        this.duration = duration;
        this.target = target;
        this.type = type;
        this.buff = buff;
    }

    public virtual void Dupe(float duration)
    {
        this.duration = duration;
        curTime = 0;
    }

    public abstract void Start();

    public abstract void Run();

    public abstract void End();
    
   
}
