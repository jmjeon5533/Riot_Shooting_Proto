using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle1 : EnemyBase
{
    protected override void Attack()
    {
        
    }
    protected override void Start()
    {
        InitStat();
        StatMultiplier();
        var g = GameManager.instance;
        var y = Random.Range(1.5f,1.5f);
        transform.position = new Vector3(15, y, 0);
        MovePos = new Vector3(-15, y, 0);
    }
    protected override void Update()
    {
        var range = MovePos - transform.position;
        if(range.magnitude <= 0.1f)
        {
            PoolManager.Instance.PoolObject(EnemyTag,gameObject);
            GameManager.instance.curEnemys.Remove(gameObject);
        }
    }
}
