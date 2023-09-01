using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider1 : EnemyBase
{
    protected override void Attack()
    {
        
    }
    protected override void Start()
    {
        var g = GameManager.instance;
        var y = Random.Range(1, g.MoveRange.y + g.MovePivot.y) * Random.Range(0, 2) > 0 ? 1 : -1;
        transform.position = new Vector3(15, y, 0);
        MovePos = new Vector3(-15, y, 0);
    }
}
