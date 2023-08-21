using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<WaveScriptObj> WavePrefab = new();
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
        var wave = WavePrefab[Random.Range(0, WavePrefab.Count)];
        yield return new WaitForSeconds(wave.startDelay);
        for (int i = 0; i < wave.WaveList.Count; i++)
        {
            GameObject enemy = Instantiate(wave.WaveList[i].Enemy, new Vector3(15, Random.Range(-5, 6), 0), Quaternion.identity);
            GameManager.instance.curEnemys.Add(enemy);
            yield return new WaitForSeconds(wave.WaveList[i].SpawnDelay);
        }
        StartCoroutine(Spawn());
    }
}
[System.Serializable]
public class SpawnWave
{
    public GameObject Enemy;
    public float SpawnDelay;
}
