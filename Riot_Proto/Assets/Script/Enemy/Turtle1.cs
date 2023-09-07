using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Turtle1 : EnemyBase
{
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
        StartCoroutine(TMove());
    }
    protected override void Update()
    {
        base.Update();
        var range = MovePos - transform.position;
        if (range.magnitude <= 0.1f)
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
    }
    IEnumerator TMove()
    {
        yield return transform.DOMoveY(1, 1.5f).SetEase(Ease.InOutSine).WaitForCompletion();
        yield return transform.DOMoveY(-1, 1.5f).SetEase(Ease.InOutSine).WaitForCompletion();
        StartCoroutine(TMove());
    }

}
