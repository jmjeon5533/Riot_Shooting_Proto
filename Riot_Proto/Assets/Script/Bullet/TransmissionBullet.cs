using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TransmissionBullet : BulletBase
{
    Player player;
    public float transRadius;

    [SerializeField] LineRenderer line;

    public int damage;

    public int maxAttack;

    bool isAttack = false;

    [SerializeField] List<Transform> test = new List<Transform>();
    
    // Start is called before the first frame update
    protected override void Start()
    {
        dir = Vector2.right;
        player = GameManager.instance.player;
        Destroy(gameObject,2f);
    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
        //if(!isAttack) line.SetPosition(1, transform.position);
        var hit = Physics.OverlapSphere(transform.position, radius);
        
        if (isAttack) return;
        foreach (var h in hit)
        {
            if (h.CompareTag("Enemy"))
            {
                float chance = Random.Range(0, 100f);
                h.GetComponent<EnemyBase>().Damage((chance <= player.CritRate)
                        ? (int)(damage * player.CritDamage) : damage, (chance <= player.CritRate) ? true : false);
                TransferAttack();
                break;
            }
        }
    }

    void TransferAttack()
    {
        Vector3 pos = transform.position;
        dir = Vector3.zero;
        isAttack = true;
        var hit = Physics.OverlapSphere(transform.position, transRadius);
        int count = 0;
        var list = hit.OrderBy(enemy => Vector3.Distance(enemy.transform.position, pos)).ToList();
        List<Transform> targets = new List<Transform>();
        
        for(int i = 0; i < list.Count; i++)
        {
            if(list[i].CompareTag("Enemy"))
            {
                targets.Add(list[i].transform);
                count++;
                if (count >= maxAttack) break;
            }
        }
        line.positionCount = targets.Count+1;
        targets = targets.OrderBy(enemy => Vector3.Distance(enemy.transform.position, pos)).ToList();
        test = targets;
        line.SetPosition(0, transform.position);
        for (int i = 0; i < targets.Count; i++)
        {
            line.SetPosition(i+1, targets[i].transform.position);
            float chance = Random.Range(0, 100f);
            targets[i].GetComponent<EnemyBase>().Damage((chance <= player.CritRate)
                    ? (int)(damage * player.CritDamage) : damage, (chance <= player.CritRate) ? true : false);
            
        }

        StartCoroutine(Delay(targets));
        

    }

    IEnumerator Delay(List<Transform> list)
    {
        float time = 0;
        
        if(list != null && list.Count > 0)
        {
            while (time < 0.3f)
            {
                int count = 0;
                line.SetPosition(0, transform.position);
                for (int i = 0; i < list.Count; i++)
                {

                    line.SetPosition(i+1, list[i].transform.position);


                }
                yield return null;
                time += Time.deltaTime;
            }
        }
        Destroy(gameObject);
    }


    
}
