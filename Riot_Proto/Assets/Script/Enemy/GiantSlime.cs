using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantSlime : EnemyBase
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
        for (int i = 0; i < 8; i++)
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
    }

    public void batSpawn() => StartCoroutine(BatSpawn());

    IEnumerator BatSpawn()
    {
        int count = 0;
        while (gameObject.activeSelf)
        {
            count++;
            var rand = count % 2 == 0 ? -1 : 1;
            var rand2 = count % 2 == 0 ? -1 : 1;
            var Y = Random.Range(3f, 3.6f) * rand;
            var enemy = PoolManager.Instance.GetObject("Bat3", new Vector3(13, Y + (Random.Range(0.5f, 4f) * rand2), 0)).GetComponent<Bat3>();
            enemy.movedir = Vector3.left;
            enemy.XPRate = 0;
            enemy.ItemAddCount = 0;
            yield return new WaitForSeconds(0.1f);
        }

    }

    protected override void Update()
    {
        base.Update();
        
    }

    protected override void Dead()
    {
        for (int i = 0; i < 4; i++)
        {
            float dir = i * 90 * Mathf.Deg2Rad;

            var slime = PoolManager.Instance.GetObject("SmallSlime", transform.position);
            slime.GetComponent<SmallSlime>().MovePos = new Vector3(Mathf.Cos(dir), Mathf.Sin(dir), 0);
            GameManager.instance.curEnemys.Add(slime);
        }

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
