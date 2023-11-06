using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage7 : EnemyBase
{
    [SerializeField] Animator anim;
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
        //float height = Random.Range(-5.5f, 1.5f);
        float height = GameManager.instance.player.transform.position.y;
        MovePos = new Vector3(Random.Range(4, 9), height);

        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < monsterCnt; i++)
        {
            var enemy = PoolManager.Instance.GetObject("Bat6", new Vector3(13, 0, 0)).GetComponent<Bat6>();
            var e = PoolManager.Instance.GetObject("Bat6", new Vector3(13, 0, 0)).GetComponent<Bat6>();
            e.sinLine.y *= -1;
            e.axisHorizon = height;
            enemy.axisHorizon = height;
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
