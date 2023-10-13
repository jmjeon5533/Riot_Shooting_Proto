using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat3 : EnemyBase
{
    Vector3 movedir;
    [SerializeField] Animator anim;
    protected override void Attack()
    {

    }
    protected override void Start()
    {
        ItemAddCount = 0.2f;
        InitStat();
        StatMultiplier();
        var g = GameManager.instance;
        int[] m = {1, -1};
        var y = Random.Range(3f, 6f) * m[Random.Range(0,2)];
        transform.position = new Vector3(15, y, 0);
        movedir = (GameManager.instance.player.transform.position - transform.position).normalized;
    }
    protected override void Move()
    {
        transform.Translate(movedir * Time.deltaTime * MoveSpeed);
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
    }
    protected override void Dead()
    {
        base.Dead();
        anim.SetBool("Death",IsDeath());
    }
}
