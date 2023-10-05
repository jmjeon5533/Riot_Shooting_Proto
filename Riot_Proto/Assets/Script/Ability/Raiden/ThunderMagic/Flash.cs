using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : AbilityBase
{
    [SerializeField] float curCooltime;
    [SerializeField] float maxCooltime;

    [SerializeField] int defaultDamage;

    [SerializeField] GameObject explosion;
    [SerializeField] float radius;

    [SerializeField] int increaseValue;
    [SerializeField] float damageRate;
    
    public override void Ability()
    {
        curCooltime+=Time.deltaTime;
        if(curCooltime >= maxCooltime && GameManager.instance.curEnemys.Count > 0)
        {
            curCooltime=0;
            Vector3 targetPos = GetEnemyPos();
            Destroy(Instantiate(explosion, targetPos, Quaternion.identity),0.7f);
            var hit = Physics.OverlapSphere(targetPos, radius);
            player = GameManager.instance.player;
            int damage = defaultDamage + (int)(player.damage * damageRate); 
            foreach (var h in hit)
            {
                if (h.CompareTag("Enemy"))
                {
                    float chance = Random.Range(0, 100f);
                    h.GetComponent<EnemyBase>().Damage((chance <= player.CritRate)
                            ? (int)(damage * player.CritDamage) : damage, (chance <= player.CritRate) ? true : false);
                   

                }
            }

        }
    }

    public override string GetStatText()
    {
        return "스킬 데미지 " + defaultDamage + " → " + (defaultDamage + (int)(increaseValue * Mathf.Pow((1 + 0.2f), level)));  
    }

    Vector3 GetEnemyPos()
    {
        var pos = GetNearbyEnemies().transform.position;
        return pos;
    }

    private GameObject GetNearbyEnemies()
    {

        GameObject player = GameManager.instance.player.gameObject;
        GameObject nearbyEnemy = player;
        float distance = Mathf.Infinity;
        foreach (GameObject enemy in GameManager.instance.curEnemys)
        {
            float newDist = Vector3.Distance(player.transform.position, enemy.transform.position);
            if (newDist <= distance)
            {
                nearbyEnemy = enemy;
                distance = newDist;
            }
        }
        return nearbyEnemy;
    }

    public override void LevelUp()
    {
        base.LevelUp();
        defaultDamage += (int)(increaseValue * Mathf.Pow((1 + 0.2f), level));
    }

    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ability();
    }
}
