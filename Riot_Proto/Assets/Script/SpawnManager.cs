using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemys;
    float Curtime;
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
        if (Curtime >= 3)
        {
            GameObject enemy =Instantiate(enemys[Random.Range(0, enemys.Length)], new Vector3(15, Random.Range(-5, 6), 0), Quaternion.identity);
            GameManager.instance.curEnemys.Add(enemy);
            //Instantiate(enemys[Random.Range(0, enemys.Length)], new Vector3(15, Random.Range(-5, 6), 0), Quaternion.identity);
            Curtime -= 3;
        }
        else
        {
            Curtime += Time.deltaTime;
        }
    }
}
