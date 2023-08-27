using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ElectricCall : AbilityBase
{
    [SerializeField] float curCooltime;
    [SerializeField] float maxCooltime;

    [SerializeField] int defaultDamage;

    [SerializeField] GameObject thunder;
    [SerializeField] ParticleSystem thunderDrain;

    [SerializeField] float radius;

    [SerializeField] int increaseValue;
    [SerializeField] float damageRate;
    [SerializeField] float delay;


    Player player;

    int attackCount = 0;

    public override void Ability()
    {
        curCooltime+=Time.deltaTime;
        if(curCooltime >= maxCooltime)
        {
            curCooltime=0;
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        ThunderDrain();
        yield return new WaitForSeconds(1.5f);
        for(int i=0;i<attackCount;i++)
        {
            ThunderDrop();
            yield return new WaitForSeconds(delay);
        }
        attackCount = 0;
        
    }

    void ThunderDrop()
    {
        List<GameObject> list = GameManager.instance.curEnemys.ToList();
        int damage = defaultDamage + (int)(player.damage * damageRate);
        
            Transform target = list[Random.Range(0, list.Count)].transform;
            while(target == null)
            {
                list = GameManager.instance.curEnemys;
                target = list[Random.Range(0, list.Count)].transform;
            }
            Instantiate(thunder, new Vector3(target.position.x, 0, target.position.z), Quaternion.identity).GetComponent<Thunder>()
               .SetDamage(damage);
            
    }

    void ThunderDrain()
    {
        List<GameObject> list = new List<GameObject>(GameManager.instance.curEnemys);
        int damage = defaultDamage/2 + (int)(player.damage * damageRate);
        foreach (var enemy in list)
        {
            if (enemy != null)
            {
                Instantiate(thunderDrain, enemy.transform.position, Quaternion.identity);
                enemy.GetComponent<EnemyBase>().Damage((Random.Range(0, 100f) <= player.CritRate)
                        ? (int)(damage * player.CritDamage) : damage);
                attackCount++;
            }
        }
    }

    public override string GetStatText()
    {
        return "스킬 데미지 " + defaultDamage + " → " + (defaultDamage + (int)(increaseValue * Mathf.Pow((1 + 0.2f), level)));
    }

    // Start is called before the first frame update
    public override void Start()
    {
        player = GameManager.instance.player;
    }

    public override void LevelUp()
    {
        base.LevelUp();
        defaultDamage+= defaultDamage += (int)(increaseValue * Mathf.Pow((1 + 0.2f), level));
    }

    // Update is called once per frame
    void Update()
    {
        Ability();
    }
}
