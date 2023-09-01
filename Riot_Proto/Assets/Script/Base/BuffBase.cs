using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    Buff, Debuff
}

public abstract class BuffBase : MonoBehaviour
{

    protected float curTime = 0;
    protected float duration;

    protected Player player;
    protected EnemyBase enemy;

    public enum TargetType
    {
        Player, Enemy
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

    public abstract void UpdateBuff();

    public abstract void Init();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
