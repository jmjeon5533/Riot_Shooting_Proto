using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefDecrease : BuffBase
{
    private float multiplier;
    private float originDef;

    private Color prevColor;
    private float depth;

    public DefDecrease(float duration, GameObject target, TargetType type, float multiplier, BuffList buff) : base(duration, target, type, buff)
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
            return;
        }
        else if (type == TargetType.Enemy)
        {
            originDef = enemy.damagedMultiplier;
            var e = target.GetComponent<EnemyBase>();
            if (!e.IsDeath())
            {
                if (e.mesh.Equals(null)) return;
                prevColor = e.mesh.material.GetColor("_OutlineColor");
                depth = e.mesh.material.GetFloat("_Outline_Bold");
                e.mesh.material.SetColor("_OutlineColor", Color.blue);
                e.mesh.material.SetFloat("_Outline_Bold", 0.3f);
            }
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
            enemy.damagedMultiplier = originDef + multiplier;
            Debug.Log(enemy.damagedMultiplier);
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
            enemy.damagedMultiplier = originDef;
            var e = target.GetComponent<EnemyBase>();
            if (e.mesh.Equals(null)) return;
            Debug.Log(e.name + "End");
            e.mesh.material.SetColor("_OutlineColor", prevColor);
            e.mesh.material.SetFloat("_Outline_Bold", depth);
        }
    }
}
