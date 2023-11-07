using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage6 : EnemyBase
{
    [SerializeField] Animator anim;

    [SerializeField] GameObject Shield;

    [SerializeField] Transform ShieldPoint;
    [SerializeField] float rotSpeed;

    bool isSpawned = false;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    private void OnEnable()
    {
        if(!isSpawned)
        {
            isSpawned = true;
            return;
        }    
        var shield = PoolManager.Instance.GetObject("Shield", ShieldPoint);
            shield.transform.position = ShieldPoint.position + (Vector3.up * 3f);
            shield = PoolManager.Instance.GetObject("Shield", ShieldPoint);
            shield.transform.position = ShieldPoint.position + ((Vector3.down * 3f) + (Vector3.left * 1.5f));
    }

    IEnumerator AttackCoroutine()
    {
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.3f);
        float radius = 70;

        float amount = radius / (7 - 1);
        float z = radius / -2f;

        for (int i = 0; i < 7; i++)
        {
            var b = PoolManager.Instance.GetObject("EnemyBullet", transform.position, Quaternion.identity).GetComponent<BulletBase>();
            float _angle = z * Mathf.Deg2Rad; // ������ �������� ��ȯ
            Vector3 direction = new Vector3(Mathf.Cos(_angle), Mathf.Sin(_angle), 0); // ���� ������ ���� ���� ����
            b.dir = -direction;
            z += amount;
        }
        yield return new WaitForSeconds(0.7f);

    }
    public override void Init()
    {
        HP = baseHp;
        StatMultiplier();
        batSpawn();
    }
    protected override void Update()
    {
        base.Update();
        ShieldPoint.Rotate(new Vector3(0,0,rotSpeed * Time.deltaTime)); 
    }

    public override void Damage(int damage, bool isCrit, string hitTag = null)
    {
        if(ShieldPoint.childCount>0)
        {
            damage = 0;
        }
        base.Damage(damage,isCrit,hitTag);
    }
    protected override void Dead()
    {
        base.Dead();
        anim.SetBool("Death",IsDeath());
    }
}
