using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Turtle3 : EnemyBase
{
    [SerializeField] Animator anim;
    [SerializeField] BulletBase DeadBullet;
    public int DeadBulletCount = 10;
    protected override void Awake()
    {
        base.Awake();
        AttackCurtime = 2.5f;
    }
    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }
    IEnumerator AttackCoroutine()
    {
        Vector3 movePos;
        var dir = 0;
        for (int j = 0; j < 3; j++)
        {

            do
            {
                movePos = transform.position + (Vector3)Random.insideUnitCircle.normalized * 5;
            }
            while (Mathf.Abs(movePos.x) >= GameManager.instance.MoveRange.x 
                || Mathf.Abs(movePos.y) >= GameManager.instance.MoveRange.y || movePos.x < -5);

            MovePos = movePos;
            yield return new WaitUntil(() => Vector3.Distance(movePos, transform.position) <= 0.1f);

            var count = 4;
            for (int i = 0; i < 360; i += 360 / count)
            {
                var b = PoolManager.Instance.GetObject("EnemyBullet", transform.position, Quaternion.identity).GetComponent<BulletBase>();
                float angle = (i + dir) * Mathf.Deg2Rad; // 각도를 라디안으로 변환
                Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0); // 라디안 각도로 방향 벡터 생성
                b.dir = direction; // 방향을 총알에 할당
                b.SetMoveSpeed(7);
            }
            dir += 30;
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
