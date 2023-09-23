using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage1 : EnemyBase
{
    [SerializeField] Animator anim;
    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }
    protected override void Start()
    {
        base.Start();
        var g = GameManager.instance;
    }
    protected override void Move()
    {
        
    }
    protected override void Update()
    {
        base.Update();
        float Speed = UIManager.instance.BGList[SceneManager.instance.StageIndex].bgs[0].speed;
        transform.Translate(Vector3.left * Time.deltaTime * Speed);

        if (Mathf.Abs(transform.position.x) >= GameManager.instance.MoveRange.x + 5)
        {
            PoolManager.Instance.PoolObject(EnemyTag, gameObject);
            GameManager.instance.curEnemys.Remove(gameObject);
        }
    }
    IEnumerator AttackCoroutine()
    {

        var g = GameManager.instance;
        anim.transform.rotation = GetRotation(g.player.transform);

        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.3f);

        var b = PoolManager.Instance.GetObject("EnemyBullet",transform.position,Quaternion.identity).GetComponent<BulletBase>();
        b.dir = (g.player.transform.position - transform.position).normalized;
    }

    Quaternion GetRotation(Transform target)
    {
        if(transform.position.x > target.transform.position.x)
        {
            return Quaternion.Euler(0, 270, 0);
        } else
        {
            return Quaternion.Euler(0,90,0);
        }
    }
}
