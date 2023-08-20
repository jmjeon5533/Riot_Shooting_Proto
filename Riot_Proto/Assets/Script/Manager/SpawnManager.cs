using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemys;
    float curTime;
    public float spawnTime;
    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsGame)
        {
            Spawn();
        }
    }
    void Spawn()
    {
        if (curTime >= spawnTime)
        {
            GameObject enemy =Instantiate(enemys[Random.Range(0, enemys.Length)], new Vector3(15, Random.Range(-5, 6), 0), Quaternion.identity);
            GameManager.instance.curEnemys.Add(enemy);
            //Instantiate(enemys[Random.Range(0, enemys.Length)], new Vector3(15, Random.Range(-5, 6), 0), Quaternion.identity);
            curTime -= spawnTime;
        }
        else
        {
            curTime += Time.deltaTime;
        }
    }
}
