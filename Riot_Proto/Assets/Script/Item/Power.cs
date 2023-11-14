using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : ItemBase
{
    [SerializeField] float MoveSpeed;
    protected override void Update()
    {
        base.Update();
        transform.position += new Vector3(-1,0) * MoveSpeed * Time.deltaTime;
    }
    protected override void GetItem()
    {
        base.GetItem();
        var b = GameManager.instance.player.bulletLevel;
        if(b <= 4) GameManager.instance.player.bulletLevel++; 
        GameManager.instance.GetMoney += 150;
        UIManager.instance.InitRate();
    }
}
