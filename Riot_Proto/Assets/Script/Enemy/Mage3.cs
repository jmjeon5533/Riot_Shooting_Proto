using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mage3 : EnemyBase
{
    [SerializeField] Animator anim;

    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }
    public override void Init()
    {
        HP = baseHp;
        StatMultiplier();
    }
    protected override void Update()
    {
        base.Update();
    }
    IEnumerator AttackCoroutine()
    {
        var g = GameManager.instance;
        anim.transform.rotation = GetRotation(g.player.transform);

        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.3f);

        float radius = 45;

        float amount = radius / (3 - 1);
        float z = radius / -2f;

        float angle = Mathf.Atan2(-transform.position.y,-transform.position.x) * Mathf.Rad2Deg;

        for (int i = 0; i < 3; i++)
        {
            var b = PoolManager.Instance.GetObject("EnemyBullet", transform.position, Quaternion.identity).GetComponent<BulletBase>();
            float _angle = z * Mathf.Deg2Rad + angle;
            Vector3 direction = new Vector3(Mathf.Cos(_angle), Mathf.Sin(_angle), 0);
            b.dir = direction;
            b.SetMoveSpeed(6f);
            z += amount;
        }
        yield return new WaitForSeconds(0.35f);
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < 360; i += 360 / 8)
        {
            var b = PoolManager.Instance.GetObject("EnemyBullet", transform.position, Quaternion.identity).GetComponent<BulletBase>();
            float angle2 = i * Mathf.Deg2Rad; // 각도를 라디안으로 변환
            Vector3 direction = new Vector3(Mathf.Cos(angle2), Mathf.Sin(angle2), 0); // 라디안 각도로 방향 벡터 생성
            b.dir = direction; // 방향을 총알에 할당
            b.SetMoveSpeed(8f);
        }
    }

    Quaternion GetRotation(Transform target)
    {
        if(transform.position.x > target.transform.position.x)
        {
            return Quaternion.Euler(0, 270, 0);
        } else
        {
            return Quaternion.Euler(0,90,0);
        }
    }
    protected override void Dead()
    {
        base.Dead();
        anim.SetBool("Death",IsDeath());
    }
}
