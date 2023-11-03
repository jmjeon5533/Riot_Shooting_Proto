using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage7 : EnemyBase
{
    [SerializeField] Animator anim;
    [SerializeField] float cosHeight;
    private int monsterCount;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);

        isAttack = false;
    }
    public override void Init()
    {
        StatMultiplier();
    }
    protected override void Update()
    {
        
        if (isAttack) {
            monsterCount += 1;
            var enemy = PoolManager.Instance.GetObject("Bat3", new Vector3(13, Mathf.Sin(monsterCount) * cosHeight, 0)).GetComponent<Bat3>();
            enemy.movedir = Vector2.left;
            enemy.MovePos = new Vector3(-13, Mathf.Sin(Time.time) * cosHeight, 0);
        }


        base.Update();
    }
    protected override void Dead()
    {
        base.Dead();
        anim.SetBool("Death", IsDeath());
    }
}
