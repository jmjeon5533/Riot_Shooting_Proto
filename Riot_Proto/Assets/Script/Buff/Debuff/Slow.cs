using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : BuffBase
{
    private float multiplier;
    private float originSpeed;
    
    public Slow(float duration, GameObject target, TargetType type, float multiplier) : base(duration, target, type)
    {
        this.multiplier = multiplier;
        if (type == TargetType.Player) player = GameManager.instance.player;
        else enemy = target.GetComponent<EnemyBase>();
    }

    public override void Start()
    {
        
        if (type == TargetType.Player)
        {
            originSpeed = player.MoveSpeed;
        }
        else if (type == TargetType.Enemy)
        {
            originSpeed = enemy.MoveSpeed;
        }
    }

    public override void Run()
    {
            curTime += Time.deltaTime;
            
            if (type == TargetType.Player)
            {
                player.MoveSpeed = originSpeed * multiplier;
            }
            else if (type == TargetType.Enemy)
            {       
                enemy.MoveSpeed = originSpeed * multiplier;
            }
    }

    public override void End()
    {
        if (type == TargetType.Player)
        {
            player.MoveSpeed = originSpeed;
        }
        else if (type == TargetType.Enemy)
        {
            enemy.MoveSpeed = originSpeed;
        }
    }




}
