using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage2 : EnemyBase
{
    [SerializeField] Animator anim;

    [SerializeField] GameObject Shield;

    [SerializeField] int count;

    [SerializeField] Transform ShieldPoint;
    [SerializeField] float rotSpeed;

    bool isSpawned = false;

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
            shield.transform.position = ShieldPoint.position + (Vector3.up * 1.5f);
            shield = PoolManager.Instance.GetObject("Shield", ShieldPoint);
            shield.transform.position = ShieldPoint.position + (Vector3.down * 1.5f);
            shield = PoolManager.Instance.GetObject("Shield", ShieldPoint);
            shield.transform.position = ShieldPoint.position + (Vector3.right * 1.5f);
            shield = PoolManager.Instance.GetObject("Shield", ShieldPoint);
            shield.transform.position = ShieldPoint.position + (Vector3.left * 1.5f);
    }

    IEnumerator AttackCoroutine()
    {

        var g = GameManager.instance;
        

        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.3f);
        float radius = 30;

        float amount = radius / (3 - 1);
        float z = (radius) / -2f;

        for (int i = 0; i < 3; i++)
        {
            var b = PoolManager.Instance.GetObject("EnemyBullet", transform.position, Quaternion.identity).GetComponent<BulletBase>();
            float _angle = z * Mathf.Deg2Rad; // ������ �������� ��ȯ
            Vector3 direction = new Vector3(Mathf.Cos(_angle), Mathf.Sin(_angle), 0); // ���� ������ ���� ���� ����
            b.dir = -direction;
            z += amount;
        }
        yield return new WaitForSeconds(0.7f);
        //transform.position = new Vector3(g.player.transform.position.x + Random.Range(3, 4.5f), g.player.transform.position.y, 0);
        MovePos = new Vector3(g.player.transform.position.x + Random.Range(8f, 11f), g.player.transform.position.y, 0);
        if(MovePos.x > g.MoveRange.x) MovePos = new Vector3(g.MoveRange.x-1, g.player.transform.position.y, 0);

    }

    protected override void Update()
    {
        base.Update();
        ShieldPoint.Rotate(new Vector3(0,0,rotSpeed * Time.deltaTime)); 
    }

    public override void Damage(int damage, bool isCrit)
    {
        if(ShieldPoint.childCount>0)
        {
            damage = 0;
        }
        base.Damage(damage,isCrit);
    }
    protected override void Dead()
    {
        base.Dead();
        anim.SetBool("Death",IsDeath());
    }
}
