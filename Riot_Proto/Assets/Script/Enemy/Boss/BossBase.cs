using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : EnemyBase
{
    public Animator anim;
    public float maxHp;
    protected override void Start()
    {
        UIManager.instance.Bossbar.SetActive(true);
        MovePos = new Vector3(6, 0, 0);
        InitStat();
        StatMultiplier();
        maxHp = HP;
    }
    protected override void Update()
    {
        base.Update();
        UIManager.instance.BossbarImage.fillAmount = HP / maxHp;
    }
    protected override void Attack()
    {

    }
}
