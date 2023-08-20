using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    void Start()
    {

    }
    protected abstract void GetItem();
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetItem();
            Destroy(gameObject);
        }
    }
}
