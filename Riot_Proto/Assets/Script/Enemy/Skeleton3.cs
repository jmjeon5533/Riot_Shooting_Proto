using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton3 : EnemyBase
{
    [SerializeField] Animator anim;
    [SerializeField] float spawnDelay;
    [SerializeField] float spawnTime;

    Vector3 originPos;
    Vector3 nextPos;

    protected override void Attack()
    {
        
    }
    
    IEnumerator AttackCoroutine()
    {
        var g = GameManager.instance;
        isAttack = true;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.7f);
        for (int i = 0; i < 6; i++)
        {

            var b = PoolManager.Instance.GetObject("EnemyBullet", transform.position + (Vector3.up * 0.7f), Quaternion.identity).GetComponent<EnemyBullet>();
            b.dir = Vector3.left;
            b.SetMoveSpeed(i + 2f);
        }
        yield return new WaitForSeconds(1f);
        MovePos = new Vector3(Random.Range(0, g.MoveRange.x / 2), Random.Range((-g.MoveRange.y + 1), (g.MoveRange.y - 1)), 0);
        isAttack = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        
        HP = baseHp;
        StatMultiplier();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Move()
    {
        base.Move();
    }
}
