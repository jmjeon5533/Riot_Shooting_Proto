using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raiden : Player
{
    protected override void Attack()
    {
        switch(bulletLevel)
        {
            case 1:
            {
                InitBullet(Instantiate(bulletPrefab[0],transform.position,Quaternion.identity));
                break;
            }
        }
    }
}
