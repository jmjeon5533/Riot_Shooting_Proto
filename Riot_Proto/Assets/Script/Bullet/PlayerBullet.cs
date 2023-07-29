using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BulletBase
{
    // Start is called before the first frame update
    void Start()
    {
        SetDmaage(Player.Instance.atkDamage);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    
}
