using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Wave",menuName = "WaveData")]
public class WaveScriptObj : ScriptableObject
{
    public float startDelay;
    public List<SpawnWave> WaveList = new();
}
