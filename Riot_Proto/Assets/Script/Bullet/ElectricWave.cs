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
        transform.localScale = Vector3.one * curRadius;
        var hit = Physics.OverlapSphere(transform.position, radius);
        foreach (var h in hit)
        {
            if (h.CompareTag("Enemy") && !hits.Contains(h.transform))
            {
                BuffBase buff = new Slow(duration, h.gameObject, BuffBase.TargetType.Enemy, BuffList.Slow,slowRate);
                h.GetComponent<EnemyBase>().AddBuff(buff);
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