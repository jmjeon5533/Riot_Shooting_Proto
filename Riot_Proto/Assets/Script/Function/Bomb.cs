using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject,3);
    }
    void Update()
    {
        transform.localScale += new Vector3(10,10,10) * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnemyBullet"))
        {
            var b = other.GetComponent<EnemyBullet>();
            PoolManager.Instance.PoolObject(b.BulletTag,other.gameObject);
        }
    }
}
