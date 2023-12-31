using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage5 : EnemyBase
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

    List<EnemyBase> bats = new();

    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        float firstspawnY = 3.5f;
        int spawnPosNum = Random.Range(0, 4);

        for (int i = 0; i < 5; i++)
        {
            if (spawnPosNum != i)
            {
                var e = PoolManager.Instance.GetObject("Bat5", new Vector3(13, firstspawnY, 0), Quaternion.identity).GetComponent<Bat5>();
                e.HP = 100;
            }
            firstspawnY -= 2.5f;
        }
        yield return new WaitForSeconds(2.5f);
        MovePos = new Vector3(Random.Range(4, 9), Random.Range(-6.5f, 3.5f));
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
        foreach(EnemyBase e in bats)
        {
            PoolManager.Instance.PoolObject(e.EnemyTag, e.gameObject);
        }
        anim.SetBool("Death", IsDeath());
    }
}
