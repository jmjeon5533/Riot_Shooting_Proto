using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] int spawnCount;

    [SerializeField] float spawnDelay;

    WaitForSeconds wait;

    [SerializeField] float widthOffset;
    

    [SerializeField] Transform spawnPos; 
    [SerializeField] Transform endPos;

    bool isSpawn = false;

    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        //wait = new WaitForSeconds(spawnDelay);
    }

    private void OnEnable()
    {
        time = 0;
        isSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > delay && !isSpawn)
        {
            isSpawn = true;
            StartCoroutine(SpawnBat());
        }
    }

    IEnumerator SpawnBat()
    {
        for(int i = 0; i < spawnCount; i++)
        {
            for(int j = -1; j<2; j++)
            {
                var bat = PoolManager.Instance.GetObject("Bat7", spawnPos.position + (Vector3.up * j * Random.Range(widthOffset*0.7f,widthOffset*1.4f)), Quaternion.identity).GetComponent<Bat3>();
                Vector3 moveDir = (endPos.position + (Vector3.up * j * widthOffset)) - (spawnPos.position + (Vector3.up * j * widthOffset));
                moveDir = moveDir.normalized;
                bat.MoveSpeed = Random.Range(26,34);
                bat.movedir = moveDir;
                float y = Mathf.Atan2(moveDir.z, moveDir.x) * Mathf.Rad2Deg;
                bat.transform.rotation = Quaternion.Euler(0f, 0, 0f);
            }
            yield return new WaitForSeconds(Random.Range(spawnDelay*0.8f,spawnDelay*1.2f));
        }
        yield return null;
        PoolManager.Instance.PoolObject("Alert", this.gameObject);
    }
}
