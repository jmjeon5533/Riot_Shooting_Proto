using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : BossBase
{
    [SerializeField] Vector3 AttackPivot;
    [SerializeField] Vector2 AttackRange;
    protected override void Attack()
    {
        var AState = Random.Range(0, 3);
        anim.SetInteger("AttackState", AState);
        anim.SetTrigger("Attack");
        print(AState);
        switch (AState)
        {
            case 0:
                {
                    StartCoroutine(Attack1());
                    break;
                }
            case 1:
                {
                    StartCoroutine(Attack2());
                    break;
                }
            case 2:
                {
                    StartCoroutine(Attack3());
                    break;
                }
        }
        AttackCooltime = Random.Range(2f,4f);
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.localPosition + AttackPivot, AttackRange);
    }
    IEnumerator Attack1()
    {
        yield return new WaitForSeconds(2f);
        Collider[] enemy = Physics.OverlapBox(transform.localPosition + AttackPivot,AttackRange * 0.5f,
            Quaternion.identity,LayerMask.GetMask("Player"));
        enemy[0].GetComponent<Player>().Damage();
    }
    IEnumerator Attack2()
    {
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < Random.Range(45,60); i++)
        {
            yield return new WaitForSeconds(0.025f);
            Vector2 rand = Vector2.zero;
            while(rand.x >= 0)
            {
                rand = Random.insideUnitCircle;
            }
            var b = PoolManager.Instance.GetObject("EnemyBullet",transform.position,Quaternion.identity).GetComponent<BulletBase>();
            b.dir = rand.normalized;

        }
    }
    IEnumerator Attack3()
    {
        yield return null;
    }
}
