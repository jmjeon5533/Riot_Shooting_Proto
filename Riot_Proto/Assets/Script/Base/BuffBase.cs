using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    Buff, Debuff
}

public abstract class BuffBase
{

    protected float curTime = 0;
    protected float duration;

    protected Player player;
    protected EnemyBase enemy;

    public enum TargetType
    {
        Player, Enemy
    }

    public bool IsOnTimer()
    {
        if(curTime >= duration)
        {
            return true;
        } else
        {
            return false;
        }
    }

    protected BuffType buffType;

    protected GameObject target;

    protected TargetType type;
    
    public BuffBase(float duration, GameObject target, TargetType type)
    {
        this.duration = duration;
        this.target = target;
        this.type = type;
    }

    public abstract void Start();

    public abstract void Run();

    public abstract void End();
    
   
}
