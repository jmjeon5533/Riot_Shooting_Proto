using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawn : MonoBehaviour
{
   [SerializeField] GameObject testEnemy;
    [SerializeField] float spawnTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        Instantiate(testEnemy, new Vector3(18, Random.Range(-6.3f, 8.4f), 5), Quaternion.identity);
        Invoke("SpawnEnemy", spawnTime);
    }
}
