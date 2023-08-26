using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public class PoolData
    {
        public string ObjName;
        public GameObject OriginObj;
        public int size;
    }
    public static PoolManager instance { get; private set; }

    public List<PoolData> pools = new();
    public Dictionary<int, Queue<GameObject>> PoolDictionary;

    private void Start()
    {
        PoolDictionary = new Dictionary<int, Queue<GameObject>>();

        foreach(PoolData pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();


        }
    }
}
