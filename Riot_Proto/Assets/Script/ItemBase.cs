using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    public Transform target;

    void Start()
    {
        target = GameManager.instance.player;
    }
}
