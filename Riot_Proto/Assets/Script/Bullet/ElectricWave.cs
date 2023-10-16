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
        var hit = Physics.OverlapSphere(transform.position, curRadius);
        foreach (var h in hit)
        {
            var bullet = h.GetComponent<EnemyBullet>();
            if (h.CompareTag("Enemy") && !hits.Contains(h.transform))
            {
                BuffBase buff = new Slow(duration, h.gameObject, BuffBase.TargetType.Enemy, BuffList.Slow,slowRate);
                h.GetComponent<EnemyBase>().AddBuff(buff);

                //Debug.Log(h.name);
                hits.Add(h.transform);
            } else if(bullet != null && !hits.Contains(h.transform))
            {
                var b = bullet;
                hits.Add(h.transform);
                b.SetMoveSpeed(b.MoveSpeed * slowRate);
            }
        }
        if (curRadius > radius)
        {
            curRadius = radius;
            Destroy(gameObject);
        }
    } 
}
