using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Turtle2 : EnemyBase
{
    [SerializeField] Animator anim;
    [SerializeField] BulletBase DeadBullet;
    public int DeadBulletCount = 10;
    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }
    IEnumerator AttackCoroutine()
    {
        for (int i = 0; i < 720; i += 720 / 50)
        {
            var b1 = PoolManager.Instance.GetObject("EnemyBullet", transform.position, Quaternion.identity).GetComponent<BulletBase>();
            var b2 = PoolManager.Instance.GetObject("EnemyBullet2", transform.position, Quaternion.identity).GetComponent<BulletBase>();
            float angle = i * Mathf.Deg2Rad; // 각도를 라디안으로 변환
            Vector3 direction1 = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0); // 라디안 각도로 방향 벡터 생성
            Vector3 direction2 = new Vector3(Mathf.Cos(angle), -Mathf.Sin(angle), 0);
            b1.dir = direction1; // 방향을 총알에 할당
            b2.dir = direction2; // 방향을 총알에 할당
            b1.SetMoveSpeed(6f);
            b2.SetMoveSpeed(6f);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        var count = 30;
        for (int j = 0; j < 3; j++)
        {
            string bulletTag = "";
            if(j % 2 == 0) bulletTag = "EnemyBullet";
            else bulletTag = "EnemyBullet2";

            for (int i = 0; i < 360; i += 360 / count)
            {
                var b = PoolManager.Instance.GetObject(bulletTag, transform.position, Quaternion.identity).GetComponent<BulletBase>();
                float angle = i * Mathf.Deg2Rad; // 각도를 라디안으로 변환
                Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0); // 라디안 각도로 방향 벡터 생성
                b.dir = direction; // 방향을 총알에 할당
            }
            yield return new WaitForSeconds(0.3f);
            count += 5;
        }
    }
    public override void Init()
    {
        StatMultiplier();
        var y = Random.Range(3f, 3f);
        transform.position = new Vector3(15, y, 0);
        MovePos = new Vector3(5, y, 0);
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
        anim.SetBool("Death", IsDeath());
    }
}
