using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider3 : EnemyBase
{
    [SerializeField] Animator anim;
    public Vector2 sinLine;
    public float axisHorizon;
    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }
    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        var b = PoolManager.Instance.GetObject("Bomb1", transform.position).GetComponent<Bomb1>();
        b.Bomb();
        yield return new WaitForSeconds(0.2f);
        isAttack = false;
    }
    public override void Init()
    {
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

        float moveValue = transform.position.x;
        float moveValueY = axisHorizon + Mathf.Sin(moveValue * sinLine.x) * sinLine.y;
        transform.position = new Vector3(moveValue, moveValueY, 0);
        float deg = Mathf.Rad2Deg * Mathf.Atan2(transform.position.y - Mathf.Sin(moveValue), transform.position.x + 15);
        anim.transform.rotation = Quaternion.Euler(deg, -90, 90);
    }
    protected override void Dead()
    {
        base.Dead();
        anim.SetBool("Death", IsDeath());
    }
}
