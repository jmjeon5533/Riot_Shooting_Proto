using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle4 : EnemyBase
{
    [SerializeField] Animator anim;

    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }
    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        for (int j = 0; j < 50; j++)
        {
            for (int i = 0; i < 3; i ++)
            {
                var b = PoolManager.Instance.GetObject("EnemyBullet", transform.position, Quaternion.identity).GetComponent<BulletBase>();
                float angle = ((i + j * 1.05f) * 120f) * Mathf.Deg2Rad;
                Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
                b.dir = direction;
                b.SetMoveSpeed(4);
            }

            yield return new WaitForSeconds(0.07f);
        }
        isAttack = false;
        MovePos = new Vector3(Random.Range(0, GameManager.instance.MoveRange.x / 2), Random.Range((-GameManager.instance.MoveRange.y + 1), (GameManager.instance.MoveRange.y - 1)), 0);
    }
    public override void Init()
    {
        StatMultiplier();
    }

    protected override void Update()
    {
        base.Update();

        anim.transform.Rotate(new Vector3(0, 650f, 0) * Time.deltaTime);

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
