using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider2 : EnemyBase
{
    [SerializeField] Animator anim;
    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }
    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        var b = PoolManager.Instance.GetObject("Bomb1", transform.position).GetComponent<Bomb1>();
        b.Bomb();
        yield return new WaitForSeconds(0.5f);
        isAttack = false;
    }
    public override void Init()
    {
        HP = baseHp;
        StatMultiplier();
    }
    protected override void Move()
    {
        transform.Translate(Vector2.left * Time.deltaTime * MoveSpeed);
    }

    protected override void Update()
    {
        base.Update();
        if (Mathf.Abs(transform.position.x) >= GameManager.instance.MoveRange.x + 5
        || Mathf.Abs(transform.position.y) >= GameManager.instance.MoveRange.y + 5)
        {
            PoolManager.Instance.PoolObject(EnemyTag, gameObject);
            GameManager.instance.curEnemys.Remove(gameObject);
        }

        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-45f, 45f));
    }
    protected override void Dead()
    {
        base.Dead();
        anim.SetBool("Death", IsDeath());
    }
}
