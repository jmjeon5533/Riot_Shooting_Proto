using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongThunder : AbilityBase
{
    [SerializeField] float curCooltime;
    [SerializeField] float maxCooltime;

    [SerializeField] int defaultDamage;

    [SerializeField] List<GameObject> thunders;
    

    [SerializeField] float radius;

    [SerializeField] int increaseValue;
    [SerializeField] float damageRate;

    public override void Ability()
    {
        
            curCooltime=0;
             useSkill = true;
            List<GameObject> list = new List<GameObject>(GameManager.instance.curEnemys);
            int damage = defaultDamage + (int)(player.damage * damageRate);

            Transform target = list[Random.Range(0, list.Count)].transform;
            while (target == null)
            {
                list = new List<GameObject>(GameManager.instance.curEnemys);
                target = list[Random.Range(0, list.Count)].transform;
            }
            Thunder t = Instantiate(thunders[level-1], new Vector3(target.position.x, 0, target.position.z), Quaternion.identity).GetComponent<Thunder>();
             t.SetDamage(damage);
            t.radius = radius;
            t.transform.localScale = new Vector3(t.transform.localScale.x, t.transform.localScale.y, t.transform.localScale.z);

        
    }

    public override string GetStatText()
    {
        if((level+1) ==3 || (level + 1) == 5)
        {
            return "스킬 데미지 " + defaultDamage + " → " + defaultDamage + (int)(increaseValue * Mathf.Pow((1 + 0.2f), level)) +
            " 스킬 범위 " + radius + " → " + (radius + 0.5);
        } else
        {
            return "스킬 데미지 " + defaultDamage + " → " + defaultDamage + (int)(increaseValue * Mathf.Pow((1 + 0.2f), level));
          
        }
        
    }

    // Start is called before the first frame update
    public override void Start()
    {
        Initalize();
    }

    public override void LevelUp()
    {
        base.LevelUp();
        defaultDamage += (int)(increaseValue * Mathf.Pow((1 + 0.2f), level));
        radius += 0.5f;

    }

    // Update is called once per frame
    void Update()
    {
        if(useSkill)
        {
            
            curCooltime += Time.deltaTime;
            minCool = curCooltime;
            maxCool = maxCooltime;
            if(curCooltime >= maxCooltime)
            {
                curCooltime = 0;
                useSkill = false;
            }
        }
    }
}
