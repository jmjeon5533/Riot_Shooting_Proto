using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Turtle1 : EnemyBase
{
    [SerializeField] Animator anim;
    [SerializeField] BulletBase DeadBullet;
    public int DeadBulletCount = 10;
    protected override void Attack()
    {

    }
    protected override void Start()
    {
        InitStat();
        StatMultiplier();
        var g = GameManager.instance;
        var y = Random.Range(3f, 3f);
        transform.position = new Vector3(15, y, 0);
        MovePos = new Vector3(-15, y, 0);
    }
    protected override void Item()
    {
        var rand = Random.Range(0, 100);
        if (rand <= 3 || GameManager.instance.itemCoolCount >= 25)
        {
            var itemrand = Random.Range(0, 10);
            var key = itemrand >= 6 ? "HP" : "Power";
            PoolManager.Instance.GetObject(key, transform.position, Quaternion.identity);
            GameManager.instance.itemCoolCount = 0;
        }
        else
        {
            GameManager.instance.itemCoolCount++;
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

        Vector3 pos = transform.position;
        float sin = Mathf.Sin(pos.x);
        pos.y = sin;

        transform.position = pos;

        if (Mathf.Abs(transform.position.x) >= GameManager.instance.MoveRange.x + 5)
        {
            PoolManager.Instance.PoolObject(EnemyTag, gameObject);
            GameManager.instance.curEnemys.Remove(gameObject);
        }
    }

    
    protected override void Dead()
    {
        base.Dead();
        for (int i = 0; i < 360; i += 360 / DeadBulletCount)
        {
            var b = Instantiate(DeadBullet, transform.position, Quaternion.identity).GetComponent<BulletBase>();
            float angle = i * Mathf.Deg2Rad; // 각도를 라디안으로 변환
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0); // 라디안 각도로 방향 벡터 생성
            b.dir = direction; // 방향을 총알에 할당
        }
        anim.SetBool("Death",IsDeath());
    }
}
