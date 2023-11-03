using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : AbilityBase
{
    [SerializeField] GameObject bullet;

    [SerializeField] float maxCooltime;
    [SerializeField] float curCooltime = 0;

    //[SerializeField] float distance;

    public override void Ability()
    {
        curCooltime += Time.deltaTime;
        minCool = curCooltime;
        if(curCooltime >= maxCooltime)
        {
            curCooltime = 0;
            ResetTimerUI(1);
            //List<GameObject> list = GetNearbyEnemies();
            for (int i = 0; i < 4 + ((level -1) * 2); i++)
            {
                Instantiate(bullet, player.transform.position, Quaternion.identity);
            }
        }
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

    public override void Initalize()
    {
        player = GameManager.instance.player;
    }

    public override string GetStatText()
    {
        return "";
    }

    // Start is called before the first frame update
   public override void Start()
    {
        Initalize();
        originCooltime = maxCooltime;
    }

    // Update is called once per frame
    void Update()
    {
        Ability();
    }
}
