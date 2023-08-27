using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : EnemyBase
{
    public Animator anim;
    protected override void Start()
    {
        MovePos = new Vector3(6,0,0);
    }
    protected override void Attack()
    {

    }
}
