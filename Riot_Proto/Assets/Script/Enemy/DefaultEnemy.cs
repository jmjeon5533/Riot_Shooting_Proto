using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemy : EnemyBase
{

    [SerializeField] float posY = 0;

    public float nextPosTime;
    public float curPosTime;

    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        Movement();
    }

    public override void Attack()
    {
        Vector3 dir = Player.Instance.transform.position - transform.position;
        dir = dir.normalized;
       

        var b = Instantiate(bullet, transform.position, Quaternion.Euler(0,0,0 )).GetComponent<Bullet>();
        b.SetDir(dir);
        b.SetDamage(atkDamage);


    }

    public override void Death()
    {
        Destroy(gameObject);
    }

    public override void Movement()
    {
        curPosTime += Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, posY, 0), Time.deltaTime * speed);
        
        if(curPosTime > nextPosTime)
        {
            curPosTime = 0;
            RandomY();
            Attack();
        }
    }

    void RandomY()
    {
        posY = Random.Range(-5, 5);
    }

    public override void Damaged(int value)
    {
        base.Damaged(value);
    }

    public override void Initialize()
    {
        base.Initialize();
    }
}
