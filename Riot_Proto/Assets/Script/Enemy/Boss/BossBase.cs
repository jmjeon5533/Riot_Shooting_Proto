using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : EnemyBase
{
    public Animator anim;
    public float maxHp;
    public override void Init()
    {
        UIManager.instance.Bossbar.SetActive(true);
        MovePos = new Vector3(6, 0, 0);
        InitStat();
        StatMultiplier();
        maxHp = HP;
        UIManager.instance.StartCoroutine(UIManager.instance.NextStageCoroutine(true,SceneManager.instance.StageIndex));
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
