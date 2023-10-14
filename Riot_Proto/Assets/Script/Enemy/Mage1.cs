using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mage1 : EnemyBase
{
    [SerializeField] Animator anim;
    [SerializeField] ParticleSystem magicCircle;
    [SerializeField] float spawnDelay;
    [SerializeField] float spawnTime;
    [SerializeField] Vector3 offset;

    bool isSpawn = false;

    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }
    protected override void Start()
    {
        base.Start();
        transform.position = new Vector3(20, 0, 0);
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        var g = GameManager.instance;
        var circle = Instantiate(magicCircle, new Vector3(Random.Range(0, g.MoveRange.x / 2), Random.Range((-g.MoveRange.y + 1), (g.MoveRange.y - 1)), 0), Quaternion.identity);
        yield return new WaitForSeconds(spawnDelay);
        transform.position = circle.transform.position + offset;
        transform.localScale = Vector3.zero;
        yield return transform.DOScale(Vector3.one, spawnTime).SetEase(Ease.OutBack);
        Attack();
        isSpawn = true;

    }
    protected override void Update()
    {
        if (!isSpawn) return; 
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
        MovePos = new Vector3(Random.Range(0, g.MoveRange.x / 2), Random.Range((-g.MoveRange.y + 1), (g.MoveRange.y - 1)), 0);
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
    protected override void Dead()
    {
        base.Dead();
        anim.SetBool("Death",IsDeath());
    }
}
