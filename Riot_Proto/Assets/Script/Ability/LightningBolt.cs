using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : AbilityBase
{
    [SerializeField] GameObject bullet;

    [SerializeField] float maxCooltime;
    [SerializeField] float curCooltime = 0;

    [SerializeField] float distance;

    Player player;

    public override void Ability()
    {
        curCooltime += Time.deltaTime;
        if(curCooltime >= maxCooltime)
        {
            curCooltime = 0;
            List<GameObject> list = GetNearbyEnemies();
            for(int i = 0; i < 4 + ((level -1) * 2); i++)
            {
                Instantiate(bullet, player.transform.position, Quaternion.identity);
            }
        }
    }

    

    private List<GameObject> GetNearbyEnemies()
    {
        List<GameObject> nearbyEnemies = new List<GameObject>();

        GameObject player = GameManager.instance.player.gameObject;
        foreach (GameObject enemy in GameManager.instance.curEnemys)
        {
            float distance = Vector3.Distance(new Vector3(0, 0, 0), enemy.transform.position);
            if (distance <= this.distance)
            {
                nearbyEnemies.Add(enemy);
            }
        }

        return nearbyEnemies;
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
    }

    // Update is called once per frame
    void Update()
    {
        Ability();
    }
}
