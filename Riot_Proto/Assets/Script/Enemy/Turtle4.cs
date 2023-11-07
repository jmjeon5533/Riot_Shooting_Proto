using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Turtle4 : EnemyBase
{
    [SerializeField] Animator anim;
    [SerializeField] BulletBase DeadBullet;
    [SerializeField] Transform rotateTransform;
    public int DeadBulletCount = 10;
    protected override void Attack()
    {
        
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
        rotateTransform.Rotate(0,0,300 * Time.deltaTime);

        if (Mathf.Abs(transform.position.x) >= GameManager.instance.MoveRange.x + 5)
        {
            PoolManager.Instance.PoolObject(EnemyTag, gameObject);
            GameManager.instance.curEnemys.Remove(gameObject);
        }
    }


    protected override void Dead()
    {
        base.Dead();
        anim.SetBool("Death", IsDeath());
    }
}
