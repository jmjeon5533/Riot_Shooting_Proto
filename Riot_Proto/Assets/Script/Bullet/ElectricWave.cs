using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricWave : MonoBehaviour
{
    private float radius;
    private float speed;
    private float duration;
    private float slowRate;

    Player player;

    private float curRadius = 0;

    List<Transform> hits = new List<Transform>();

    public void Init(float radius, float speed, float duration, float slowRate)
    {
        this.radius = radius;
        this.speed = speed;
        this.duration = duration;
        this.slowRate = slowRate;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player;
        
    }

    // Update is called once per frame
    void Update()
    {
        curRadius += Time.deltaTime * speed;
        //Debug.Log(curRadius);
        //transform.localScale = Vector3.one * curRadius;
        var bulletHit = Physics.OverlapSphere(transform.position, curRadius/2);
        foreach (var h in bulletHit)
        {
            var bullet = h.GetComponent<EnemyBullet>();
            if(bullet != null && !hits.Contains(h.transform))
            {
                var b = bullet;
                hits.Add(h.transform);
                var g = GameManager.instance;

                b.dir = -((g.player.transform.position - b.transform.position).normalized);
                b.SetMoveSpeed(b.MoveSpeed * 0.75f);
                //b.SetMoveSpeed(b.MoveSpeed * slowRate);
            }
        }
        var hit = Physics.OverlapSphere(transform.position, curRadius);
        foreach (var h in hit)
        {
            var bullet = h.GetComponent<EnemyBullet>();
            if (h.CompareTag("Enemy") && !hits.Contains(h.transform))
            {
                BuffBase buff = new Slow(duration, h.gameObject, BuffBase.TargetType.Enemy, BuffList.Slow, slowRate);
                h.GetComponent<EnemyBase>().AddBuff(buff);

                //Debug.Log(h.name);
                hits.Add(h.transform);
            }
        }
        if (curRadius > radius)
        {
            curRadius = radius;
            Destroy(gameObject);
        }
    } 


}
