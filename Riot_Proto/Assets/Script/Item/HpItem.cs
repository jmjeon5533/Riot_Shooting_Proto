using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpItem : ItemBase
{
    [SerializeField] float MoveSpeed;
    void Update()
    {
        transform.position += new Vector3(-1,0) * MoveSpeed * Time.deltaTime;
    }
    protected override void GetItem()
    {
        if(GameManager.instance.player.HP <= 5) GameManager.instance.player.HP++;
        GameManager.instance.GetMoney += 150;
        UIManager.instance.InitHeart();
    }
}
