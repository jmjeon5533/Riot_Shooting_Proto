using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemy : EnemyBase
{
    public bool isTarget = false;
    
    public override void Attack()
    {
        Player.Instance.Damaged(atkDamage);
    }

    public override void Death()
    {
       Destroy(gameObject);
    }

    public override void Movement()
    {
        transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
        if(isTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x,Player.Instance.transform.position.y), moveSpeed/2 * Time.deltaTime);
        }
        if(transform.position.x < -19)
        {
            Destroy(gameObject);
        }
    }

    public override void Damaged(int damage)
    {
        base.Damaged(damage);
    }

    // Start is called before the first frame update

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Attack();
            Death();
        }
    }

    void Start()
    {
        material = gameObject.GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
}
