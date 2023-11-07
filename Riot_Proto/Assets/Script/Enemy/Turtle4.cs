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

    }
    
    protected override void Update()
    {
        base.Update();

        anim.transform.Rotate(new Vector3(0, 650f, 0) * Time.deltaTime);
        //Vector3 pos = transform.position;
        //float sin = Mathf.Sin(pos.x);
        //pos.y = sin;

        //transform.position = pos;

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
