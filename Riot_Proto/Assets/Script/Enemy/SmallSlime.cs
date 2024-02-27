using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSlime : EnemyBase
{
    [SerializeField] Animator anim;

    //GiantSlime -> Death (Instiante(SmallSlime) x 4 -> AddGameObject(SmallSlime))
    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 3; i++)
        {
            var b = PoolManager.Instance.GetObject("GravityBullet", transform.position + Vector3.up * 1.5f, Quaternion.identity).GetComponent<GravityBullet>();
            float power = Random.Range(100, 420);
            float gravityScale = Random.Range(0.1f, 0.7f);
            b.dir = Vector3.zero;
            b.SetMoveSpeed(power);
            b.SetGravity(gravityScale);
            b.Bounce();
        }
        isAttack = false;
        MovePos = new Vector3(Random.Range(0, GameManager.instance.MoveRange.x / 2), Random.Range((-GameManager.instance.MoveRange.y + 1), (GameManager.instance.MoveRange.y - 1)), 0);
    }

    protected override void Update()
    {
        base.Update();

    }

    protected override void Dead()
    {
        base.Dead();
        anim.SetBool("Death", IsDeath());
    }

    public override void Init()
    {
        HP = baseHp;
        StatMultiplier();
    }

    protected override void Move()
    {
        base.Move();
    }
}
