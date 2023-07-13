using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawn : MonoBehaviour
{
    public GameObject[] enemys;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        Instantiate(enemys[Random.Range(0, enemys.Length)], new Vector3(15, Random.Range(-5, 6), 0), Quaternion.identity);
        Instantiate(enemys[Random.Range(0, enemys.Length)], new Vector3(15, Random.Range(-5, 6), 0), Quaternion.identity);

        yield return new WaitForSeconds(3f);
        StartCoroutine(Spawn());

    }
}
