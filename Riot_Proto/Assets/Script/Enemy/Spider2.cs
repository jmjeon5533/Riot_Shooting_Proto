using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider2 : EnemyBase
{
    [SerializeField] Animator anim;
    private Vector3 runPos;
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
        StatMultiplier();
    }
    protected override void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, runPos, MoveSpeed * Time.deltaTime);
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

        runPos = new Vector3(-15, GameManager.instance.player.transform.position.y);
        float deg = Mathf.Rad2Deg * Mathf.Atan2(transform.position.y - runPos.y, transform.position.x - runPos.x);
        anim.transform.rotation = Quaternion.Euler(deg, -90, 90);
    }
    protected override void Dead()
    {
        base.Dead();
        anim.SetBool("Death", IsDeath());
    }
}
