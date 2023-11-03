using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage7 : EnemyBase
{
    [SerializeField] Animator anim;
    [SerializeField] float cosHeight = 5;
    [SerializeField] float cosWeight = 0.5f;
    [SerializeField] int monsterCnt;
    [SerializeField] float spawnCooltime = 0.08f;
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
        for (int i = 0; i < monsterCnt; i++)
        {
            //float height = Mathf.Sin(i * 2) * cosHeight;
            float height = Mathf.Sin(i * cosWeight) * cosHeight;
            for (int j = 0; j < 2; j++)
            {
                var enemy = PoolManager.Instance.GetObject("Bat3", new Vector3(13, height, 0)).GetComponent<Bat3>();
                enemy.movedir = Vector2.left;
                enemy.MovePos = new Vector3(-13, height, 0);
                height *= -1;
            }
            yield return new WaitForSeconds(spawnCooltime);
        }

        isAttack = false;
    }
    public override void Init()
    {
        StatMultiplier();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void Dead()
    {
        base.Dead();
        anim.SetBool("Death", IsDeath());
    }
}
