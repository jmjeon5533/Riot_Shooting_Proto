using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance{ get; private set;}
    public List<WaveScriptObj> WavePrefab = new();
    public int SpawnCount;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
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
        if(SpawnCount <= 20)
        {

        var wave = WavePrefab[Random.Range(0, WavePrefab.Count)];
        yield return new WaitForSeconds(wave.startDelay);
        for (int i = 0; i < wave.WaveList.Count; i++)
        {
            GameObject enemy = Instantiate(wave.WaveList[i].Enemy, new Vector3(15, Random.Range(-5, 6), 0), Quaternion.identity);
            GameManager.instance.curEnemys.Add(enemy);
            yield return new WaitForSeconds(wave.WaveList[i].SpawnDelay);
        }
        SpawnCount++;
        StartCoroutine(Spawn());
        }
        else
        {
            print("Boss");
        }
    }
}
[System.Serializable]
public class SpawnWave
{
    public GameObject Enemy;
    public float SpawnDelay;
}
