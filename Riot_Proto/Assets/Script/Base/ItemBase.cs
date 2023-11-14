using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    [SerializeField] string itemTag;
    void Start()
    {

    }
    protected virtual void GetItem()
    {
        SoundManager.instance.SetAudio("GetItem",SoundManager.SoundState.SFX,false);
    }
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
