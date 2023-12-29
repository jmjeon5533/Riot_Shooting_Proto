using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem4 : EnemyBase
{
    [SerializeField] Animator anim1;
    [SerializeField] Animator anim2;
    [SerializeField] SkinnedMeshRenderer ShieldMaterial;

    //[SerializeField] GameObject Shield;
    [SerializeField] Transform ShieldPoint; //따로 Shields란 Empty Object 생성후 Shield를 Child화
    public bool IsShield = true;
    //bool isSpawned = false;
    //Inspired by Golem1
    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        float o = 0;
        for (int c = 0; c < 3; c++)
        {
            anim1.SetTrigger("Attack");
            anim2.SetTrigger("Attack");
            anim1.speed = 1.8f;
            anim2.speed = 1.8f;
            yield return new WaitForSeconds(0.65f);
            for (int i = 0; i < 20; i++)
            {
                var b = PoolManager.Instance.GetObject("EnemyBullet", transform.position, Quaternion.identity).GetComponent<BulletBase>();
                float angle = ((i + o) * 18) * Mathf.Deg2Rad;
                Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
                b.dir = direction;
                b.SetMoveSpeed(5f);
            }
            o += 0.5f;
        }
        anim1.speed = anim2.speed = 1f;
        isAttack = false;
    }

    public void batSpawn() => StartCoroutine(BatSpawn());

    IEnumerator BatSpawn()
    {
        int count = 0;
        while (gameObject.activeSelf)
        {
            count++;
            var rand = count % 2 == 0 ? -1 : 1;
            var rand2 = count % 2 == 0 ? -1 : 1;
            var Y = Random.Range(3f, 3.6f) * rand;
            var enemy = PoolManager.Instance.GetObject("Bat3", new Vector3(13, Y + (Random.Range(0.5f, 4f) * rand2), 0)).GetComponent<Bat3>();
            enemy.movedir = Vector3.left;
            enemy.XPRate = 0;
            enemy.ItemAddCount = 0;
            yield return new WaitForSeconds(0.1f);
        }

    }

    protected override void Update()
    {
        base.Update();
        var a = ShieldMaterial.material.color.a;
        var alpha = Mathf.MoveTowards(a, 0, Time.deltaTime);
        ShieldMaterial.material.SetColor("_Color", new Color(0.5f, 0.5f, 1, alpha));

        /*
        if (!isAttack)
        {
            
            ShieldPoint.position = Vector3.MoveTowards();
        }*/
    }
    public override void Damage(int damage, bool isCrit, string hitTag = null)
    {
        /*if (ShieldPoint.childCount > 0) //????s
        {
            damage = 0;
        }*/
        if (IsShield)
        {
            if (!isAttack)
            {
                ShieldMaterial.material.SetColor("_Color", new Color(0.5f, 0.5f, 1, 0.5f));
            }
            else
            {
                base.Damage(damage, isCrit, hitTag);
            }
        }
        else
        {
            base.Damage(damage, isCrit, hitTag);
        }
    }

    protected override void Dead()
    {
        base.Dead();
        anim1.SetBool("Death", IsDeath());
        anim2.SetBool("Death", IsDeath());
    }

    public override void Init()
    {
        HP = baseHp;
        StatMultiplier();
    }

    protected override void Move()
    {
        base.Move();
    }
    
    /*
    private void OnEnable()
    {
        if (!isSpawned)
        {
            isSpawned = true;
            return;
        }
        //????
        var shield1 = PoolManager.Instance.GetObject("Shield", ShieldPoint);
        shield1.transform.position = ShieldPoint.position + (Vector3.up * 3f);
        var shield2 = PoolManager.Instance.GetObject("Shield", ShieldPoint);
        shield2.transform.position = ShieldPoint.position + (Vector3.down * 3f);
        var shield3 = PoolManager.Instance.GetObject("Shield", ShieldPoint);
        shield3.transform.position = ShieldPoint.position + (Vector3.right * 3f);
        var shield4 = PoolManager.Instance.GetObject("Shield", ShieldPoint);
        shield4.transform.position = ShieldPoint.position + (Vector3.left * 3f);
    }*/

    /*
    protected override void Update()
    {
        base.Update();
        ShieldPoint.Rotate(new Vector3(0, 0, rotSpeed * Time.deltaTime));
    }
    */
}
