using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IEnemyBase
{
    public int hp;
    public int maxhp;

    public int atkDamage;

    public float atkSpeed;

    public float moveSpeed;

    bool isDamaged = false;

    protected Rigidbody2D rigid;

    protected Material material;


    public abstract void Attack();

    public virtual void Damaged(int damage)
    {
        if (isDamaged) return;
        StartCoroutine(Damage());
        hp -= damage;
        if (hp < 0)
        {
            hp = 0;
            Death();
        }
    }

    public abstract void Death();
   

    public virtual void Initalize()
    { 
        maxhp = hp;
    }

    public abstract void Movement();


    public IEnumerator Damage()
    {
        //material.color = Color.red;
        isDamaged = true;
        yield return new WaitForSeconds(0.1f);
        //material.color = Color.white;
        isDamaged = false;
    }


}
