using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryPower : ItemBase
{
    [SerializeField] float MoveSpeed;
    protected override void Update()
    {
        base.Update();
        transform.position += new Vector3(-1, 0) * MoveSpeed * Time.deltaTime;
    }
    protected override void GetItem()
    {
        base.GetItem();
        UIManager.instance.InitRate();
    }
}
