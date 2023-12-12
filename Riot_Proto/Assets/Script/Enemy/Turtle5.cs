using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Turtle5 : EnemyBase
{
    [SerializeField] Animator anim;
    [SerializeField] BulletBase DeadBullet;
    [SerializeField] Transform rotateTransform;
    public int DeadBulletCount = 10;
    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }
    IEnumerator AttackCoroutine()
    {
        var count = 15;
        for (int i = 0; i < 360; i += 360 / count)
        {
            var b = PoolManager.Instance.GetObject("EnemyBullet", transform.position, Quaternion.identity).GetComponent<BulletBase>();
            float angle = i * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            b.dir = direction;
            b.SetMoveSpeed(7);
        }

        yield return new WaitForSeconds(1f);   
    }
    public override void Init()
    {
        StatMultiplier();
        MovePos = new Vector3(-16, transform.position.y, 0);
        for(int i = 0; i < 8; i++)
        {
            var e = PoolManager.Instance.GetObject("Bat3",transform.position + Vector3.right * 2);
            e.transform.SetParent(rotateTransform);
            e.GetComponent<Bat3>().MoveSpeed = 0;
            rotateTransform.Rotate(new Vector3(0,0,360/8));
        }
    }
    public override void StatMultiplier()
    {
        var p = GameManager.instance.EnemyPower * 1.2f;
        HP = Mathf.Round(p * baseHp);
        XPRate = Mathf.Round(p * baseXPRate);
    }
    
    protected override void Update()
    {
        base.Update();

        anim.transform.Rotate(new Vector3(0, 650f, 0) * Time.deltaTime);
        rotateTransform.Rotate(new Vector3(0,0,Time.deltaTime * 400));
        if (Mathf.Abs(transform.position.x) >= GameManager.instance.MoveRange.x + 5)
        {
            PoolManager.Instance.PoolObject(EnemyTag, gameObject);
            GameManager.instance.curEnemys.Remove(gameObject);
        }

        if (Mathf.Abs(transform.position.y) >= GameManager.instance.MoveRange.y + 5)
        {
            transform.rotation = Quaternion.Euler(0, 0, -transform.rotation.z);
            Attack();
        }
    }


    protected override void Dead()
    {
        base.Dead();
        anim.SetBool("Death", IsDeath());
    }
}
