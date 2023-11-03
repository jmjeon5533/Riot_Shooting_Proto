using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpItem : ItemBase
{
    [SerializeField] float MoveSpeed;
    [SerializeField] ParticleSystem getEffect;
    protected override void Update()
    {
        base.Update();
        transform.position += new Vector3(-1,0) * MoveSpeed * Time.deltaTime;
    }
    protected override void GetItem()
    {
        if(GameManager.instance.player.HP < GameManager.instance.player.MaxHP) GameManager.instance.player.HP++;
        GameManager.instance.GetMoney += 150;
        Instantiate(getEffect,GameManager.instance.player.transform);
        UIManager.instance.InitHeart();
        UIManager.instance.InitRate();
    }
}
