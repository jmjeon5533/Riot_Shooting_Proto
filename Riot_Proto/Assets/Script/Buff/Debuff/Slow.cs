using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : BuffBase
{
    private float slowRate;
    private float originSpeed;
    
    public Slow(float duration, GameObject target, TargetType type, float slowRate) : base(duration, target, type)
    {
        this.slowRate = slowRate;
        if (type == TargetType.Player) player = GameManager.instance.player;
        else enemy = target.GetComponent<EnemyBase>();
    }

    public override void Init()
    {
        
    }

    public override void UpdateBuff()
    {
        curTime += Time.deltaTime;
        if (type == TargetType.Player)
        {
            originSpeed = player.MoveSpeed;
            player.MoveSpeed = originSpeed * slowRate;
            if(curTime >= duration)
            {
                curTime = 0;
                player.MoveSpeed = originSpeed;
                Destroy(this);
            }
            } else if (type == TargetType.Enemy)
            {
                originSpeed = enemy.MoveSpeed;
                enemy.MoveSpeed = originSpeed * slowRate;
                if(curTime >= duration)
                {
                    curTime = 0;
                    enemy.MoveSpeed = originSpeed;
                    Destroy(this);
                }
            }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        UpdateBuff();
    }

    
}
