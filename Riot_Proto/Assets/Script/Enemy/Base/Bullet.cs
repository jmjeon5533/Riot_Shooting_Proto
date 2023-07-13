using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask targetMask;
    private int damage;
    public float speed;
    Vector3 dir;

    public void SetDamage(int value)
    {
        damage = value;
    }

    public void SetDir(Vector3 dir)
    {
        this.dir = dir;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(targetMask == LayerMask.GetMask("Player") && other.gameObject.layer.Equals(LayerMask.GetMask("Player")))
        {
            var player = other.GetComponent<Player>();
            //플레이어 데미지 입히기

        } else if(targetMask == LayerMask.GetMask("Enemy") && other.gameObject.layer.Equals(LayerMask.GetMask("Enemy")))
        {
            var enemy = other.GetComponent<EnemyBase>();
            enemy.Damaged(damage);
        }
    }
}
