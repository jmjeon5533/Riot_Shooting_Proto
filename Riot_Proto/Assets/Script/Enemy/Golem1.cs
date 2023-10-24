using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem1 : EnemyBase
{
    [SerializeField] Animator anim1, anim2;
    [SerializeField] SkinnedMeshRenderer ShieldMaterial;
    public bool IsShield = true;

    private float bulletSpd;

    public void SetBulletSpeed(float value)
    {
        bulletSpd = value;
    }

    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }
    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        anim1.SetTrigger("Attack");
        anim2.SetTrigger("Attack");
        yield return new WaitForSeconds(0.75f);
        for (int i = 0; i < 360; i += 360 / 25)
        {
            var b = PoolManager.Instance.GetObject("EnemyBullet", transform.position, Quaternion.identity).GetComponent<BulletBase>();
            float angle = i * Mathf.Deg2Rad; // 각도를 라디안으로 변환
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0); // 라디안 각도로 방향 벡터 생성
            b.dir = direction; // 방향을 총알에 할당
            b.SetMoveSpeed(5f);
        }
        yield return new WaitForSeconds(1.5f);
        isAttack = false;
    }
    protected override void Update()
    {
        base.Update();
        var a = ShieldMaterial.material.color.a;
        var alpha = Mathf.MoveTowards(a, 0, Time.deltaTime);
        ShieldMaterial.material.SetColor("_Color", new Color(0.5f, 0.5f, 1, alpha));
    }
    public override void Damage(int damage, bool isCrit)
    {
        if (IsShield)
        {
            if(!isAttack)
            {
                ShieldMaterial.material.SetColor("_Color", new Color(0.5f, 0.5f, 1, 0.5f));
            }
            else
            {
                base.Damage(damage, isCrit);
            }
        }
        else
        {
            base.Damage(damage, isCrit);
        }
    }
    protected override void Dead()
    {
        base.Dead();
        anim1.SetBool("Death", IsDeath());
        anim2.SetBool("Death", IsDeath());

    }
}
