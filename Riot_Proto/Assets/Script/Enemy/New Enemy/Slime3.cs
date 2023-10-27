using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime3 : EnemyBase
{
    [SerializeField] Animator anim;
    [SerializeField] float rotateSpeed;
    [SerializeField] Transform rotateAxis;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {

        var g = GameManager.instance;


        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.25f);
        var b = PoolManager.Instance.GetObject("GravityBullet", transform.position).GetComponent<GravityBullet>();
        b.SetMoveSpeed(700);
        b.SetGravity(6);
        yield return new WaitForSeconds(0.7f);
        MovePos = new Vector3(Random.Range(2, 7), Random.Range(-6.5f, 3.5f));

    }
    public override void Init()
    {
        HP = baseHp;
        StatMultiplier();
    }
    protected override void Update()
    {
        base.Update();
        transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
        rotateAxis.Rotate(new Vector3(0, 0, -rotateSpeed * Time.deltaTime));
    }

    protected override void Dead()
    {
        base.Dead();
        anim.SetBool("Death", IsDeath());
    }
}
