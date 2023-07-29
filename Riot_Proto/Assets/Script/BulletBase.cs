using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public int atkDamage;

    public float moveSpeed;

    public bool isEnemyBullet = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();   
    }

    public virtual void Movement()
    {
        transform.Translate(transform.right * moveSpeed * ((isEnemyBullet) ? -1 : 1) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Attack(other.gameObject);
    }

    public virtual void Attack(GameObject target)
    {
        if (!isEnemyBullet)
        {
            if (target.CompareTag("Enemy"))
            {
                EnemyBase enemy = target.GetComponent<EnemyBase>();
                enemy.Damaged(atkDamage);
            }
        }
        else
        {
            if (target == Player.Instance.gameObject)
            {
                //데미지 받는곳
            }
        }
    }

    public void SetDmaage(int value)
    {
        atkDamage = value;
    }
}
