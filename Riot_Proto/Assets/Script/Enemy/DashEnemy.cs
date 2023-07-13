
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : EnemyBase
{

    [SerializeField] float posY = 0;

    public float nextPosTime;
    public float curPosTime;

    //public GameObject bullet;

    public ParticleSystem alert;
    [SerializeField] Player player;

    public float radius;

    bool isAttack = false;

    float moveTime = 0;

    Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        targetPos = player.transform.position;
        if(Player.Instance != null) player = Player.Instance;
        transform.position += new Vector3(0, 0, 6);
        Instantiate(alert, player.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        Movement();
    }

    public override void Attack()
    {
        

    }

    public override void Death()
    {
        Destroy(gameObject);
    }

    public override void Movement()
    {
        curPosTime += Time.deltaTime;
        moveTime += Time.deltaTime / nextPosTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
        if(Vector3.Distance(targetPos, transform.position) < 0.2f && !isAttack)
        {
            isAttack = true;
            Collider[] hits = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Player"));
            foreach(var hit in hits)
            {
                Player player = hit.GetComponent<Player>();
                //데미지 넣기
            }
        }

        if (curPosTime >= nextPosTime)
        {
            curPosTime = 0;
            moveTime = 0;
            isAttack = false;
            targetPos = player.transform.position;
            Instantiate(alert, player.transform.position, Quaternion.identity);
            //Attack();
        }
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
