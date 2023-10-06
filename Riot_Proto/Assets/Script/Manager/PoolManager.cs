using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public class ObjectPoolInfo
    {
        public string key;
        public GameObject obj;
        public int maxAmount;
    }

    public class ObjectPoolQueueInfo
    {
        public ObjectPoolQueueInfo(Transform parent, ObjectPoolInfo info)
        {
            this.parent = parent;
            this.info = info;

            queue = new();
            for (int i = 0; i < info.maxAmount; i++)
            {
                var obj = Instantiate(info.obj);
                obj.transform.SetParent(parent);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
        }

        public Transform parent;
        public ObjectPoolInfo info;
        public Queue<GameObject> queue = new();
    }

    public static PoolManager Instance { get; private set; }

    public ObjectPoolInfo[] poolInfo;
    private readonly Dictionary<string, ObjectPoolQueueInfo> pools = new();

    public GameObject GetObject(string key, Vector3 position = default, Quaternion rotation = default)
    {
        if (pools[key].queue.Count == 0)
        {
            var newObj = Instantiate(pools[key].info.obj, position, rotation);
            newObj.SetActive(true);
            return newObj;
        }

        var target = pools[key].queue.Dequeue();
        target.transform.position = position;
        target.transform.rotation = rotation;
        target.transform.SetParent(null);
        target.SetActive(true);
        if (target.CompareTag("Enemy"))
        {
            ResetMaterial(target.gameObject, key);
        }
        return target;
    }
    public GameObject GetObject(string key, Transform parent = null)
    {
        if (pools[key].queue.Count == 0)
        {
            var newObj = Instantiate(pools[key].info.obj,parent);
            newObj.SetActive(true);
            return newObj;
        }

        var target = pools[key].queue.Dequeue();
        target.transform.position = parent.transform.position;
        target.transform.rotation = parent.transform.rotation;
        target.transform.SetParent(parent);
        target.transform.localScale = new Vector3(1,1,1);
        target.SetActive(true);
        if(target.CompareTag("Enemy"))
        {
            ResetMaterial(target.gameObject, key);
        }
        return target;
    }

    public void PoolObject(string key, GameObject obj)
    {
        if (pools[key].queue.Count >= pools[key].info.maxAmount)
        {
            Destroy(obj);
        }
        else
        {
            pools[key].queue.Enqueue(obj);
            obj.SetActive(false);
            obj.transform.SetParent(pools[key].parent);
        }
    }
    

    private void Awake()
    {
        Instance = this;

        foreach (var info in poolInfo)
        {
            var pObj = new GameObject($"{info.key}");
            pObj.transform.SetParent(transform);
            pools.Add(info.key, new ObjectPoolQueueInfo(pObj.transform, info));
        }
    }

    void ResetMaterial(GameObject obj, string key)
    {
        if (obj.GetComponent<EnemyBase>().mesh == null) return;
        var mesh = obj.GetComponent<EnemyBase>().mesh;
        
        mesh.material = pools[key].info.obj.GetComponent<EnemyBase>().mesh.sharedMaterial;
        mesh.material.SetColor("_OutlineColor", mesh.sharedMaterial.GetColor("_OutlineColor"));
        mesh.material.SetFloat("_Outline_Bold", mesh.sharedMaterial.GetFloat("_Outline_Bold"));


    }
}
