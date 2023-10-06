using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : BuffBase
{
    private float multiplier;
    private float originSpeed;

    private Color prevColor;
    private float depth;

    
    public Slow(float duration, GameObject target, TargetType type, BuffList buff, float multiplier) : base(duration, target, type, buff)
    {
        this.multiplier = multiplier;
        if (type == TargetType.Player) player = GameManager.instance.player;
        else enemy = target.GetComponent<EnemyBase>();
    }

    public override void Dupe(float duration)
    {
        base.Dupe(duration);
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

            var e = target.GetComponent<EnemyBase>();
            if(!e.IsDeath())
            {
                if (e.mesh.Equals(null)) return;
                prevColor = e.mesh.material.GetColor("_OutlineColor");
                depth = e.mesh.material.GetFloat("_Outline_Bold");
                e.mesh.material.SetColor("_OutlineColor", Color.white);
                e.mesh.material.SetFloat("_Outline_Bold", 0.3f);
            }
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
            var e = target.GetComponent<EnemyBase>();
            if (e.mesh.Equals(null)) return;
            Debug.Log(e.name + "End");
            e.mesh.material.SetColor("_OutlineColor", prevColor);
            e.mesh.material.SetFloat("_Outline_Bold", depth);
        }
    }




}
