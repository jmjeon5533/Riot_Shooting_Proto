using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : ItemBase
{
    [SerializeField] float MoveSpeed;
    void Update()
    {
        transform.position += new Vector3(-1,0) * MoveSpeed * Time.deltaTime;
    }
    protected override void GetItem()
    {
        var b = GameManager.instance.player.bulletLevel;
        if(b <= 4) GameManager.instance.player.bulletLevel++; 
        GameManager.instance.GetMoney += 150;
    }
}
