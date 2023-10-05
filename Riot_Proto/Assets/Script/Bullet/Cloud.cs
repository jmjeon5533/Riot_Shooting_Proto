using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    float speed;

    [SerializeField] float curAttackTime = 0;
    [SerializeField] float maxAttackTime;
    [SerializeField] float radius;

    [SerializeField] GameObject line;

    [SerializeField] int damage;

    List<Transform> lines = new List<Transform>();
    
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
                    var player = GameManager.instance.player;
                    //StartCoroutine(Electric(collider.transform, maxAttackTime/2));
                    float chance = Random.Range(0, 100f);
                    collider.GetComponent<EnemyBase>().Damage((chance <= player.CritRate)
                            ? (int)(damage * player.CritDamage) : damage, (chance <= player.CritRate) ? true : false);
                    collider.GetComponent<EnemyBase>().AddBuff(new Slow(2, collider.gameObject, BuffBase.TargetType.Enemy, BuffList.Slow, 0.7f));
                    Debug.Log(collider.name);
                }
            }
        }
    }

    //IEnumerator Electric(Transform target, float duration) 
    //{
    //    GameObject obj = Instantiate(line, transform);
    //    lines.Add(obj.transform);
    //    obj.GetComponent<LineRenderer>().SetPosition(0, transform.position);
    //    obj.GetComponent<LineRenderer>().SetPosition(1, target.position);
    //    target.GetComponent<EnemyBase>().Damage(damage);
    //    float prevSpeed = target.GetComponent<EnemyBase>().MoveSpeed;
    //    target.GetComponent<EnemyBase>().MoveSpeed = prevSpeed/2;
    //    yield return new WaitForSeconds(duration);
    //    if(target != null)
    //    {
    //        target.GetComponent<EnemyBase>().MoveSpeed = prevSpeed;
    //    }
    //    lines.Remove(obj.transform);
    //    Destroy(obj);
    //}
         
    public void Duration(float value, float speed, int damage)
    {
        this.speed = speed;
        this.damage = damage;
        Destroy(gameObject, value);
    }

    void Movement()
    {
        transform.Translate(transform.right * Time.deltaTime * speed);
        for(int i = 0; i < lines.Count; i++)
        {
            lines[i].transform.position = transform.position;
            
        }
    }
} 
