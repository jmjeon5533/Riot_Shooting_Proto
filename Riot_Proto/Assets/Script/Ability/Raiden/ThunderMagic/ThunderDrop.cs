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

    float totalMinusCooltime = 0;
    // Start is called before the first frame update
    public override void Start()
    {
        Initalize();
        curCooltime = maxCooltime-1;
        minCool = curCooltime;
        useSkill = true;
        maxCool = maxCooltime;
        originCooltime = maxCooltime;
    }

    public override void ResizingCooldown()
    {
        maxCooltime = originCooltime - (originCooltime * SubtractCool);
        maxCooltime -= totalMinusCooltime;
        maxCool = maxCooltime;
    }

    // Update is called once per frame
    void Update()
    {
        Ability();
    }

    public override void Ability()
    {
        curCooltime += Time.deltaTime;
        minCool = curCooltime;
        if (GameManager.instance.curEnemys != null && curCooltime >= maxCooltime && GameManager.instance.curEnemys.Count > 0 )
        {
            
            List<GameObject> nearbyEnemies = GetNearbyEnemies();
            if (nearbyEnemies != null && nearbyEnemies.Count == 0) return;
            Transform target = nearbyEnemies[Random.Range(0, nearbyEnemies.Count)].transform;
            if (target == null) return;
            ResetTimerUI(1);
            SoundManager.instance.SetAudio("ThunderBolt", SoundManager.SoundState.SFX, false);
            Instantiate(thunder, new Vector3(target.position.x, 0, target.position.z), Quaternion.identity).GetComponent<Thunder>()
                .SetDamage(defaultDamage);

            curCooltime = 0;
            useSkill = true;
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
        totalMinusCooltime += (Mathf.Round((0.3f * Mathf.Pow((1 + 0.2f), level)) * 100) / 100);
        maxCooltime -= (Mathf.Round((0.3f * Mathf.Pow((1 + 0.2f), level)) * 100) / 100);
        maxCool = maxCooltime;

    }

    public override string GetStatText()
    {
        return "스킬 데미지 " + defaultDamage + " → " + (defaultDamage + (int)(3 * Mathf.Pow((1 + 0.15f), level+1))) + 
            "\n스킬 쿨타임 " + maxCooltime + " → " + (maxCooltime - Mathf.Round((0.3f * Mathf.Pow((1 + 0.2f), level+1)) * 100) / 100);
    }
}
