using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefDecrease : BuffBase
{
    private float multiplier;
    private float originDef;

    public DefDecrease(float duration, GameObject target, TargetType type, float multiplier) : base(duration, target, type)
    {
        this.multiplier = multiplier;
        if (type == TargetType.Player) player = GameManager.instance.player;
        else enemy = target.GetComponent<EnemyBase>();
    }

    public override void Start()
    {

        if (type == TargetType.Player)
        {
            return;
        }
        else if (type == TargetType.Enemy)
        {
            originDef = enemy.defence;
        }
    }

    public override void Run()
    {
        curTime += Time.deltaTime;
        if (type == TargetType.Player)
        {
            return;
        }
        else if (type == TargetType.Enemy)
        {
            enemy.defence = originDef * multiplier;
        }
    }

    public override void End()
    {
        if (type == TargetType.Player)
        {
            return;
        }
        else if (type == TargetType.Enemy)
        {
            enemy.defence = originDef * multiplier;
        }
    }
}
