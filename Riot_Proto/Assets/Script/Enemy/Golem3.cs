using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem3 : EnemyBase
{
    [SerializeField] Transform rotateTransform;
    [SerializeField] Animator anim1;

    public override void Init()
    {
        base.Init();
        for(int i = 0; i < 8; i++)
        {
            var e = PoolManager.Instance.GetObject("Bat3",transform.position + Vector3.right * 2);
            e.transform.SetParent(rotateTransform);
            e.GetComponent<Bat3>().MoveSpeed = 0;
            rotateTransform.Rotate(new Vector3(0,0,360/8));
        }
    }
    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        anim1.SetTrigger("Attack");
        yield return new WaitForSeconds(0.75f);
        for (int i = 0; i < 6; i++)
        {
            var b = PoolManager.Instance.GetObject("GravityBullet", transform.position-(Vector3.down*0.3f), Quaternion.identity).GetComponent<GravityBullet>();
            float power = Random.Range(100, 220);
            float gravityScale = Random.Range(0.1f, 0.3f);
            b.dir = Vector3.zero;
            b.SetMoveSpeed(power);
            b.SetGravity(gravityScale);
            b.Bounce();
        }
        yield return new WaitForSeconds(1.5f);
        var g = GameManager.instance;
        MovePos = new Vector3(Random.Range(2,g.MoveRange.x-2), Random.Range(-g.MoveRange.y + 2, g.MoveRange.y -2), 0);
        isAttack = false;
    }

   




    protected override void Dead()
    {
        base.Dead();
        anim1.SetBool("Death", IsDeath());
    }
}
