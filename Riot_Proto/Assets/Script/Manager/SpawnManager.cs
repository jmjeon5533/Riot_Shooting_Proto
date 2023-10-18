using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance { get; private set; }
    public List<WaveList> WavePrefab = new();
    public int BossSpawnWave;
    public int SpawnCount;
    int StageLevel;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Spawn();
    }
    public void Spawn()
    {
        StageLevel = SceneManager.instance.StageIndex;
        StartCoroutine(SpawnWave());
    }
    IEnumerator SpawnWave()
    {
        yield return new WaitUntil(() => GameManager.instance.IsGame);
        yield return new WaitForSeconds(2f);
        while (SpawnCount < BossSpawnWave)
        {
            var Level = SpawnCount <= 10 ? Random.Range(0,5) : Random.Range(5,10);
            var wave = WavePrefab[StageLevel].Wavelist[Level];
            yield return new WaitForSeconds(wave.startDelay);
            for (int i = 0; i < wave.WaveList.Count; i++)
            {
                if(!GameManager.instance.IsGame) yield break;
                GameObject enemy = PoolManager.Instance.GetObject(wave.WaveList[i].Enemy, wave.WaveList[i].SpawnPos, Quaternion.identity);
                GameManager.instance.curEnemys.Add(enemy);
                yield return new WaitForSeconds(wave.WaveList[i].SpawnDelay);
            }
            SpawnCount++;
            GameManager.instance.EnemyPower += 0.1f;
        }
        yield return new WaitUntil(() => GameManager.instance.curEnemys.Count == 0 || GameManager.instance.curEnemys.Equals(null));
        GameObject Boss = PoolManager.Instance.GetObject($"Boss{StageLevel + 1}", new Vector3(15, 0, 0), Quaternion.identity);
        GameManager.instance.curEnemys.Add(Boss);
        yield return new WaitUntil(() => !Boss.activeSelf);
        yield return new WaitForSeconds(1.5f);
        UIManager.instance.InitRate();
        UIManager.instance.UseClearTab();
    }
}
[System.Serializable]
public class WaveList
{
    public List<WaveScriptObj> Wavelist = new();
}
[System.Serializable]
public class SpawnWave
{
    public string Enemy;
    public float SpawnDelay;
    public Vector3 SpawnPos = new Vector3(99,99,99);
}
