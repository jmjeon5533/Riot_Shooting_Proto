using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance { get; private set; }
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
            var wave = WaveExcuter.instance.waveScripts[StageLevel];
            var rand = Random.Range(0,wave.Waves.Count);
            print(rand);

            yield return StartCoroutine(wave.Waves[rand]());

            SpawnCount++;
            GameManager.instance.EnemyPower += 0.1f;
            yield return new WaitUntil(() => GameManager.instance.curEnemys.Count == 0 || GameManager.instance.curEnemys.Equals(null));
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

