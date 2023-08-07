using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : ItemBase
{
    Transform player;
    public int AddValue;
    float curtime;
    public float moveRate;
    public float dirPower;
    Vector3 startPos;
    Vector3 middlePos;
    private void Start()
    {
        player = GameManager.instance.player.transform;
        startPos = transform.position;
        middlePos = transform.position + ((Vector3)Random.insideUnitCircle * dirPower);
    }
    private void Update()
    {
        curtime += Time.deltaTime * moveRate;
        transform.position = GameManager.CalculateBezier(startPos, 
            middlePos, player.position, curtime);
    }
    protected override void GetItem()
    {
        Destroy(gameObject);
        GameManager.instance.AddXP(AddValue);
    }
}
