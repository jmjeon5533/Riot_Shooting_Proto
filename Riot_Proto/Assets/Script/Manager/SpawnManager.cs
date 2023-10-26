using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance { get; private set; }
    public int BossSpawnWave;
    public int SpawnCount;
    int StageLevel;

    int rand;
    int curWaveIndex;

    public float coolMobSpawn = 10;
    float curMobSpawn;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Spawn();
    }
    private void Update()
    {
        curMobSpawn += Time.deltaTime;
        if(curMobSpawn >= coolMobSpawn)
        {
            curMobSpawn = 0;
            coolMobSpawn = Random.Range(3f,8f);
            int index = 0;
            switch(index)
            {
                case 0:
                {
                    var enemy1 = PoolManager.Instance.GetObject("Bat3",new Vector3(14,-6,0),Quaternion.identity).GetComponent<Bat3>();
                    var enemy2 = PoolManager.Instance.GetObject("Bat3",new Vector3(14,6,0),Quaternion.identity).GetComponent<Bat3>();
                    var dir1 = (GameManager.instance.player.transform.position - enemy1.transform.position).normalized;
                    var dir2 = (GameManager.instance.player.transform.position - enemy2.transform.position).normalized;
                    enemy1.movedir = dir1;
                    enemy2.movedir = dir2;

                    enemy1.MoveSpeed = 8;
                    enemy2.MoveSpeed = 8;
                    break;
                }
            }
        }
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
            while(rand == curWaveIndex)
            {
                rand = Random.Range(0,wave.Waves.Count);
            }
            curWaveIndex = rand;
            
            print(rand);

            yield return StartCoroutine(wave.Waves[rand]());

            SpawnCount++;
            GameManager.instance.EnemyPower += 0.06f;
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

