using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IListener
{
    public float HP;
    [HideInInspector] public float baseHp;
    [Space(10)]
    public float XPRate;
    [HideInInspector] public float baseXPRate;
    [Space(10)]
    public float defence;
    [HideInInspector] public float baseDefence;
    [Space()]
    public float MoveSpeed;
    public float AttackCooltime;
    private float AttackCurtime;

    public Vector3 MovePos;
    public string EnemyTag;

    protected virtual void Start()
    {
        var g = GameManager.instance;
        var x = Random.Range(0, g.MoveRange.x + g.MovePivot.x);
        var y = Random.Range(-g.MoveRange.y + g.MovePivot.y, g.MoveRange.y + g.MovePivot.y);
        MovePos = new Vector3(x, y, 0);
        InitStat();
        StatMultiplier();
    }
    protected void InitStat()
    {
        baseHp = HP;
        baseXPRate = XPRate;
        baseDefence = defence;
    }
    public virtual void StatMultiplier()
    {
        var p = GameManager.instance.EnemyPower;
        HP = p * baseHp;
        XPRate = p * baseXPRate;
    }

    protected virtual void Update()
    {
        if (AttackCurtime >= AttackCooltime)
        {
            AttackCurtime -= AttackCooltime;
            Attack();
        }
        else
        {
            AttackCurtime += Time.deltaTime;
        }
        Move();
    }
    protected virtual void Move()
    {
        if (Vector3.Distance(transform.position, MovePos) >= 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, MovePos, MoveSpeed * Time.deltaTime);
        }
    }
    public void Damage(int damage)
    {
        var dmg = 50f / (50 + defence);
        print(dmg);
        HP -= dmg * damage;
        if (HP <= 0)
        {
            GameManager.instance.curEnemys.Remove(this.gameObject);
            for (int i = 0; i < XPRate; i++)
            {
                PoolManager.Instance.GetObject("XP", transform.position, Quaternion.identity);
            }

            PoolManager.Instance.PoolObject(EnemyTag, gameObject);
        }
        PoolManager.Instance.GetObject("Hit", transform.position, Quaternion.identity);
    }
    protected abstract void Attack();

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Damage();
        }
    }

    public void OnEvent(Event_Type type, Component sender, object param = null)
    {
        if (type == Event_Type.ApplyBuff)
        {

        }
    }
}
