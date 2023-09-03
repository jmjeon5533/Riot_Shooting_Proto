using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public float HP;
    [HideInInspector] public float baseHp;
    [Space(10)]
    public float XPRate;
    [HideInInspector] public float baseXPRate;
    [Space()]
    public float MoveSpeed;
    public float AttackCooltime;
    private float AttackCurtime;

    public Vector3 MovePos;
    public string EnemyTag;

    [SerializeField] List<BuffBase> EnemyBuffList = new List<BuffBase>();

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
    }
    public virtual void StatMultiplier()
    {
        var p = GameManager.instance.EnemyPower;
        HP = p * baseHp;
        XPRate = p * baseXPRate;
    }

    public void AddBuff(BuffBase buff) 
    {
        
        BuffBase _buff = buff;
        _buff.Start();
        EnemyBuffList.Add(_buff);
    }

    void CheckBuff(BuffBase buff)
    {
        List<BuffBase> list = new List<BuffBase>(EnemyBuffList);
        foreach (BuffBase _buff in list)
        {
            
        }
    }

    protected void BuffTimer()
    {
        List<BuffBase> list = null; 
        if(EnemyBuffList.Count > 0)
        {
            list = new List<BuffBase>();    
            for(int i = 0; i < EnemyBuffList.Count; i++)
            {
                EnemyBuffList[i].Run();
                if(EnemyBuffList[i].IsOnTimer())
                {
                    EnemyBuffList[i].End();
                    list.Add(EnemyBuffList[i]);
                }
            }
        }
        if(list != null && list.Count > 0)
        {
            for(int i = 0; i < list.Count;i++)
            {
                EnemyBuffList.Remove(list[i]);
            }
        }
    } 

   
    protected virtual void Update()
    {
        BuffTimer();
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
        HP -= damage;
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
}
