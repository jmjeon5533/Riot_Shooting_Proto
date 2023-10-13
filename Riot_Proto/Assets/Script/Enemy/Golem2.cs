using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem2 : EnemyBase
{
    [SerializeField] Animator anim1;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    protected override void Item()
    {
        var rand = Random.Range(0, 100);
        if (rand <= 3 || GameManager.instance.itemCoolCount >= 25)
        {
            var itemrand = Random.Range(0, 10);
            var key = itemrand >= 6 ? "HP" : "Power";
            PoolManager.Instance.GetObject(key, transform.position, Quaternion.identity);
            GameManager.instance.itemCoolCount = 0;
        }
        else
        {
            GameManager.instance.itemCoolCount++;
        }
    }
    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        anim1.SetTrigger("Attack");
        yield return new WaitForSeconds(0.75f);
        for (int i = 0; i < 6; i++)
        {
            var b = PoolManager.Instance.GetObject("GravityBullet", transform.position-(Vector3.down*0.3f), Quaternion.identity).GetComponent<GravityBullet>();
            float power = Random.Range(100, 220);
            float gravityScale = Random.Range(0.1f, 0.3f);
            b.dir = Vector3.zero;
            b.SetMoveSpeed(power);
            b.SetGravity(gravityScale);
            b.Bounce();
            
            
           

        }
        yield return new WaitForSeconds(1.5f);
        var g = GameManager.instance;
        MovePos = new Vector3(Random.Range(2,g.MoveRange.x-2), Random.Range(-g.MoveRange.y + 2, g.MoveRange.y -2), 0);
        isAttack = false;
    }

   




protected override void Dead()
    {
        base.Dead();
        anim1.SetBool("Death", IsDeath());
    }

    protected override void Move()
    {
        base.Move();
    }

    protected override void DeadEffect()
    {
        base.DeadEffect();
    }
}
