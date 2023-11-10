using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinTurtle : EnemyBase
{
    [SerializeField] Animator anim;
    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }
    IEnumerator AttackCoroutine()
    {
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 360; i++)
            {
                var b = PoolManager.Instance.GetObject("EnemyBullet", transform.position).GetComponent<BulletBase>();
                float rad = i * Mathf.Deg2Rad;
                Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
                b.dir = direction.normalized;
                b.SetMoveSpeed(7);
                yield return new WaitForSeconds(1f);
            }
            //yield return new WaitForSeconds(3f);
            //for (int i = 0; i < 10; i++)
            //{
            //    var blt = PoolManager.Instance.GetObject("EnemyBullet", transform.position).GetComponent<BulletBase>();
            //    Vector2 direction = new Vector2(Mathf.Cos(i * 36), Mathf.Sin(i * 36));
            //    blt.dir = direction.normalized;
            //    blt.SetMoveSpeed(8);
            //}
        }
    }

    protected override void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, MovePos, MoveSpeed * Time.deltaTime);
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
