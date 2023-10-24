using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Skeleton3 : EnemyBase
{
    [SerializeField] Animator anim;
    [SerializeField] float spawnDelay;
    [SerializeField] float spawnTime;

    Vector3 originPos;
    Vector3 nextPos;

    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }
    
    IEnumerator AttackCoroutine()
    {
        var g = GameManager.instance;
        isAttack = true;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.7f);
        var b = PoolManager.Instance.GetObject("EnemyBullet", transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
        b.dir = Vector3.zero;
        b.transform.localScale = Vector3.one * 0.5f;
        Vector3 originPos = b.transform.position;
        float dist = 12.5f;
        //if(originPos.x - dist <= -10) dist = (originPos.x - dist) + 10;
        b.transform.DOMove(originPos + (Vector3.left * Mathf.Abs(dist)), 0.3f * Mathf.Abs(dist)).OnComplete(() => { b.transform.DOScale(Vector3.zero, 0.1f).OnComplete(() => { PoolManager.Instance.PoolObject("EnemyBullet", b.gameObject); }); });
        //yield return b.transform.DOScale(Vector3.zero, 0.1f).WaitForCompletion();
        for (int i = 0; i < 5; i++)
        {
            int startAngle = Random.Range(0, 360);
            for (int j = 0; j < 4; j++)
            {
                var tempB = PoolManager.Instance.GetObject("EnemyBullet", b.transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
                tempB.SetMoveSpeed(4);
                tempB.dir = Vector3.left;
                float _angle = (startAngle + (j * 90)) * Mathf.Deg2Rad; // ?????? ???????? ???
                Vector3 direction = new Vector3(Mathf.Cos(_angle), Mathf.Sin(_angle), 0); // ???? ?????? ???? ???? ????
                tempB.dir = -direction;
            }
            yield return new WaitForSeconds(0.6f);
            //tempB.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
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
