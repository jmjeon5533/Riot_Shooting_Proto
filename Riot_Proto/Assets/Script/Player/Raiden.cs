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
                InitBullet(Instantiate(bulletPrefab[0],transform.position + Vector3.up,Quaternion.identity));
                InitBullet(Instantiate(bulletPrefab[0],transform.position + Vector3.down,Quaternion.identity));
                break;
            }
            case 3:
            {
                InitBullet(Instantiate(bulletPrefab[0],transform.position,Quaternion.identity));
                InitBullet(Instantiate(bulletPrefab[0],transform.position + Vector3.up,Quaternion.identity));
                InitBullet(Instantiate(bulletPrefab[0],transform.position + Vector3.down,Quaternion.identity));
                break;
            }
            case 4:
            {
                InitBullet(Instantiate(bulletPrefab[0],transform.position,Quaternion.identity));
                InitBullet(Instantiate(bulletPrefab[0],transform.position,Quaternion.identity));
                InitBullet(Instantiate(bulletPrefab[0],transform.position,Quaternion.identity));
                break;
            }
            case 5:
            {
                InitBullet(Instantiate(bulletPrefab[0],transform.position,Quaternion.identity));
                InitBullet(Instantiate(bulletPrefab[0],transform.position,Quaternion.identity));
                InitBullet(Instantiate(bulletPrefab[0],transform.position,Quaternion.identity));
                break;
            }
        }
    }
}
