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
            case 2:
            {

                break;
            }
            case 3:
            {

                break;
            }
            case 4:
            {

                break;
            }
            case 5:
            {
                
                break;
            }
        }
    }
}
