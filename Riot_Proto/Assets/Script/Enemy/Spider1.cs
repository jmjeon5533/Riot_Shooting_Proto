using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider1 : EnemyBase
{
    protected override void Attack()
    {
        
    }
    protected override void Move()
    {
        transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed);
    }
    protected override void Start()
    {
        InitStat();
        StatMultiplier();
        var g = GameManager.instance;
        int[] m = {1, -1};
        var y = Random.Range(3f, 6f) * m[Random.Range(0,2)];
        transform.position = new Vector3(15, y, 0);
    }
    protected override void Update()
    {
        base.Update();
        var range = MovePos - transform.position;
        if (Mathf.Abs(transform.position.x) >= GameManager.instance.MoveRange.x + 5)
        {
            PoolManager.Instance.PoolObject(EnemyTag, gameObject);
            GameManager.instance.curEnemys.Remove(gameObject);
        }
    }
}