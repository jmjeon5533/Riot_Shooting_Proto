using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage6 : EnemyBase
{
    [SerializeField] Animator anim;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    public void batSpawn() => StartCoroutine(BatSpawn());

    IEnumerator BatSpawn()
    {
        int count = 0;
        while (gameObject.activeSelf)
        {
            count++;
            var enemy = PoolManager.Instance.GetObject("Bat3", new Vector3(13, 0, 0)).GetComponent<Bat3>();
            enemy.movedir = new Vector3(-1, Mathf.Cos(Time.time));
            enemy.XPRate = 0;
            enemy.ItemAddCount = 0;
            yield return new WaitForSeconds(0.01f);
        }

    }

    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        //float spawnSlimeY = 3.5f;
        //int spawnPosNum = Random.Range(0, 4);

        for (int i = 0; i < 5; i++)
        {
        //    if (spawnPosNum != i)
        //    {
                var e = PoolManager.Instance.GetObject("Bat3", new Vector3(13, 0, 0), Quaternion.identity).GetComponent<Bat3>();
                e.HP = 100;
        //    }
        //    spawnSlimeY -= 2.5f;
        }
        //yield return new WaitForSeconds(2.5f);
        //MovePos = new Vector3(Random.Range(4, 9), Random.Range(-6.5f, 3.5f));
        isAttack = false;
    }
    public override void Init()
    {
        StatMultiplier();
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
}
