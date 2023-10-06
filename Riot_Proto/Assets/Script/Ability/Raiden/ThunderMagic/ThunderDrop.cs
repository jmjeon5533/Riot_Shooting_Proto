using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderDrop : AbilityBase
{
    [SerializeField] float maxCooltime;
    [SerializeField] float curCooltime;

    [SerializeField] int defaultDamage;

    [SerializeField] GameObject thunder;

    [SerializeField] float distance;
    // Start is called before the first frame update
    public override void Start()
    {
        Initalize();
    }

    // Update is called once per frame
    void Update()
    {
        Ability();
    }

    public override void Ability()
    {
        curCooltime += Time.deltaTime;
        if (GameManager.instance.curEnemys != null && curCooltime >= maxCooltime && GameManager.instance.curEnemys.Count > 0 )
        {
            List<GameObject> nearbyEnemies = GetNearbyEnemies();
            if (nearbyEnemies != null && nearbyEnemies.Count == 0) return;
            Transform target = nearbyEnemies[Random.Range(0, nearbyEnemies.Count)].transform;
            if (target == null) return;
            Instantiate(thunder, new Vector3(target.position.x, 0, target.position.z), Quaternion.identity).GetComponent<Thunder>()
                .SetDamage(defaultDamage);

            curCooltime = 0;
        }
    }

    
    private List<GameObject> GetNearbyEnemies()
    {
        List<GameObject> nearbyEnemies = new List<GameObject>();

        GameObject player = GameManager.instance.player.gameObject;
        foreach (GameObject enemy in GameManager.instance.curEnemys)
        {
            float distance = Vector3.Distance(new Vector3(0,0,0), enemy.transform.position);
            if (distance <= this.distance)
            {
                nearbyEnemies.Add(enemy);
            }
        }

        return nearbyEnemies;
    }

    public override void LevelUp()
    {
        base.LevelUp();
        defaultDamage += (int)(3 * Mathf.Pow((1 + 0.15f), level));
        maxCooltime -= (Mathf.Round((0.3f * Mathf.Pow((1 + 0.2f), level)) * 100) / 100);

    }

    public override string GetStatText()
    {
        return "스킬 데미지 " + defaultDamage + " → " + (defaultDamage + (int)(3 * Mathf.Pow((1 + 0.15f), level))) + 
            "\n스킬 쿨타임 " + maxCooltime + " → " + (maxCooltime - Mathf.Round((0.3f * Mathf.Pow((1 + 0.2f), level)) * 100) / 100);
    }
}
