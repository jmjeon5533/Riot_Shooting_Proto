using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IEnemyBase
{
    [SerializeField] protected int hp;
    [SerializeField] protected int maxhp;

    [SerializeField] protected int atkDamage;

    [SerializeField] protected float speed;
    [SerializeField] protected float attackCooltime = 0;
    [SerializeField] protected float maxAttackCooltime;

    [SerializeField] protected float spawnX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(transform.position.x > spawnX) SpawnDelay();
    }

    public abstract void Attack();
    

    public virtual void Damaged(int value)
    {
        hp -= value;
        if(hp <= 0)
        {
            hp = 0;
            Death();
        }
    }

    public abstract void Death();
    

    public virtual int GetHP()
    {
        return hp;
    }

    public virtual void SpawnDelay()
    {
        transform.position += -transform.right * Time.deltaTime * speed;

    }

    public virtual void Initialize()
    {
        maxhp = hp;
    }

    public abstract void Movement();
    

    public virtual void SetHP(int value)
    {
        hp = value;
    }
}
