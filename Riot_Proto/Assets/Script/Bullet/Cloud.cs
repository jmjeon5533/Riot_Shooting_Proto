using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    float speed;

    [SerializeField] float curAttackTime = 0;
    [SerializeField] float maxAttackTime;
    [SerializeField] float radius;

    [SerializeField] int damage;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Attack();
    }

    void Attack()
    {
        curAttackTime += Time.deltaTime;
        if(curAttackTime > maxAttackTime)
        {
            curAttackTime = 0;
            Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale, Quaternion.identity, LayerMask.GetMask("Enemy"));
            foreach(Collider collider in colliders)
            {
                if(collider != null )
                {
                    collider.GetComponent<EnemyBase>().Damage(damage);
                    Debug.Log(collider.name);
                }
            }
        }
    }
         
    public void Duration(float value, float speed, int damage)
    {
        this.speed = speed;
        this.damage = damage;
        Destroy(gameObject, value);
    }

    void Movement()
    {
        transform.Translate(transform.right * Time.deltaTime * speed);
    }
} 
