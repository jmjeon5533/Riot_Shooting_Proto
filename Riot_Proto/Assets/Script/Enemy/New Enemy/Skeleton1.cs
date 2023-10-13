using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton1 : EnemyBase
{
    [SerializeField] Animator anim;
    [SerializeField] float spawnDelay;
    [SerializeField] float spawnTime;

    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(2.5f);
        float spawnSlimeY = 3.5f;
        int spawnPosNum = Random.Range(0, 4);

        for (int i = 0; i < 5; i++)
        {
            if (spawnPosNum != i)
            PoolManager.Instance.GetObject("Slime2", new Vector3(13, spawnSlimeY, 0), Quaternion.identity).GetComponent<Slime2>();
            spawnSlimeY -= 2.5f;
        }
        yield return new WaitForSeconds(5f);
        isAttack = false;
    }

    protected override void Dead()
    {
        base.Dead();
        anim.SetBool("Death", IsDeath());
    }

    protected override void Move()
    {
        base.Move();
    }
}
