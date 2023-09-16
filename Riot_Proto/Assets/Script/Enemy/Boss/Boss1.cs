using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : BossBase
{

    [SerializeField] int pattern = 0;
     
    [SerializeField] Vector3 AttackPivot;
    [SerializeField] Vector2 AttackRange;

    protected override void Attack()
    {
        var AState = pattern;
        anim.SetInteger("AttackState", AState);
        anim.SetTrigger("Attack");
        print(AState);
        isAttack = true;
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
        pattern++;
        if (pattern > 2) pattern = 0;
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
            foreach(var e in enemy) e.GetComponent<Player>().Damage();
        isAttack = false;
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
        isAttack = false;
    }
    
    IEnumerator Attack3()
    {
        yield return new WaitForSeconds(1f);
        int angle = 0;
        for (int i = 0; i < 50; i++)
        {
            
            for (int j = angle; j < (360 + angle); j += 360 / 20)
            {
                var b = PoolManager.Instance.GetObject("EnemyBullet", transform.position, Quaternion.identity).GetComponent<BulletBase>();
                b.SetMoveSpeed(4.5f);
                float _angle = j * Mathf.Deg2Rad; // 각도를 라디안으로 변환
                Vector3 direction = new Vector3(Mathf.Cos(_angle), Mathf.Sin(_angle), 0); // 라디안 각도로 방향 벡터 생성
                b.dir = direction;  
            }
            angle += 15;
            if (angle > 360) angle = angle - 360;
            Debug.Log(angle);
            if (i > 0 && i % 10 == 0) StartCoroutine(Attack3_2());
            yield return new WaitForSeconds(0.45f);
            
        }
        isAttack = false;

    }

    IEnumerator Attack3_2()
    {
        Vector3 shootPos1 = transform.position + (Vector3.up * 2);
        Vector3 shootPos2 = transform.position;
        Vector3 shootPos3 = transform.position + (Vector3.down * 2);
        var p = GameManager.instance.player;
        for(int i = 0; i < 15;i++)
        {
            var b1 = PoolManager.Instance.GetObject("EnemyBullet", shootPos1, Quaternion.identity).GetComponent<BulletBase>();
            b1.dir = GetTargetDir(shootPos1, p.transform.position);
            var b2 = PoolManager.Instance.GetObject("EnemyBullet", shootPos2, Quaternion.identity).GetComponent<BulletBase>();
            b2.dir = GetTargetDir(shootPos2, p.transform.position);
            var b3 = PoolManager.Instance.GetObject("EnemyBullet", shootPos3, Quaternion.identity).GetComponent<BulletBase>();
            b3.dir = GetTargetDir(shootPos3, p.transform.position);
            yield return new WaitForSeconds(0.1f);
        }
    
    }

    Vector3 GetTargetDir(Vector3 origin, Vector3 target)
    {
        Vector3 dir = target - origin;
        dir = dir.normalized;
        return dir;
    }
}
