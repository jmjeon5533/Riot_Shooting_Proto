using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    [SerializeField] string itemTag;
    void Start()
    {

    }
    protected abstract void GetItem();
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetItem();
            PoolManager.Instance.PoolObject(itemTag,gameObject);
        }
    }
    protected virtual void Update()
    {
        if(transform.position.x <= -15)
        PoolManager.Instance.PoolObject(itemTag,gameObject);
    }
}
