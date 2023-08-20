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
    public int XPRate;

    void Start()
    {
        var g = GameManager.instance;
        var x = Random.Range(0,g.MoveRange.x + g.MovePivot.x);
        var y = Random.Range(-g.MoveRange.y + g.MovePivot.y, g.MoveRange.y + g.MovePivot.y);
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
        if (HP <= 0)
        {
            GameManager.instance.curEnemys.Remove(this.gameObject);
            Destroy(gameObject);
            for (int i = 0; i < XPRate; i++)
            {
                Instantiate(GameManager.instance.XPPrefab,transform.position,Quaternion.identity);
            }
        }
    }
    protected abstract void Attack();
}
