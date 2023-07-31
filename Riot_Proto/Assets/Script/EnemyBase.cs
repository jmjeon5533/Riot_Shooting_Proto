using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public int HP;

    public float MoveSpeed;
    public float AttackCooltime;
    private float AttackCurtime;
    public Vector3 MovePos;

    void Start()
    {
        var x = Random.Range(0f,10f);
        var y = Random.Range(-5f,5f);
        MovePos = new Vector3(x,y,0);
    }

    void Update()
    {
        if(AttackCurtime >= AttackCooltime)
        {
            AttackCurtime -= AttackCooltime;
            Attack();
        }
        else
        {
            AttackCurtime += Time.deltaTime;
        }
        if(Vector3.Distance(transform.position,MovePos) >= 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, MovePos,MoveSpeed * Time.deltaTime);
        }
    }
    public void Damage(int damage)
    {
        HP -= damage;
        if(HP <= 0) Destroy(gameObject);
    }
    protected abstract void Attack();
}
