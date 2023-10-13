using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raiden : Player
{
    [SerializeField] float maxCooltime;
    [SerializeField] float curCooltime = 0;

    [SerializeField] GameObject autoTargetBullet;

    public int numClosestEnemies = 4;

    protected override void Update()
    {
        base.Update();
        curCooltime += Time.deltaTime;
        if(curCooltime >= maxCooltime && bulletLevel >= 3 && GameManager.instance.curEnemys.Count > 0)
        {
            curCooltime = 0;
            List<Transform> list = FindClosestEnemies();

            for(int i = 0; i < 1 * (bulletLevel-2) + ((bulletLevel >= 3) ? 1 : 0); i++)
            {
                var bullet = Instantiate(autoTargetBullet,transform.position,Quaternion.identity);
                bullet.GetComponent<ThunderBolt>().target = list[Random.Range(0,list.Count)];
            }
        }
    }


    public List<Transform> FindClosestEnemies()
    {
        List<Transform> closestEnemies = new List<Transform>();

        while (closestEnemies.Count < numClosestEnemies)
        {
            Transform closestEnemy = null;
            float closestDistanceSqr = Mathf.Infinity;
            
            foreach (var enemyTransform in GameManager.instance.curEnemys)
            {
                if (!closestEnemies.Contains(enemyTransform.transform))
                {
                    Vector3 directionToEnemy = enemyTransform.transform.position - transform.position;
                    float distanceSqrToEnemy = directionToEnemy.sqrMagnitude;

                    if (distanceSqrToEnemy < closestDistanceSqr)
                    {
                        closestDistanceSqr = distanceSqrToEnemy;
                        closestEnemy = enemyTransform.transform;
                    }
                }
            }
            if (closestEnemy != null)
            {
                closestEnemies.Add(closestEnemy);
            }
            else
            {
                // ���� �� �̻� ���� ��� ����
                break;
            }
            if (closestEnemies.Count >= GameManager.instance.curEnemys.Count) break;
        }

        return closestEnemies;
    }

    protected override void Attack()
    {
        switch(bulletLevel)
        {
            case 1:
            {
                InitBullet(PoolManager.Instance.GetObject("PlayerBullet",transform.position + (Vector3.right * 3),Quaternion.identity));
                break;
            }
            case 2:
            {
                InitBullet(PoolManager.Instance.GetObject("PlayerBullet",transform.position + (Vector3.right * 3) + (Vector3.up * 0.2f), Quaternion.identity));
                InitBullet(PoolManager.Instance.GetObject("PlayerBullet",transform.position + (Vector3.right * 3) + (Vector3.down * 0.2f), Quaternion.identity));
                break;
            }
            case 3:
            {
                InitBullet(PoolManager.Instance.GetObject("PlayerBullet",transform.position + (Vector3.right * 3),Quaternion.identity));
                InitBullet(PoolManager.Instance.GetObject("PlayerBullet",transform.position + (Vector3.right * 1.5f) + (Vector3.up * 0.3f),Quaternion.identity));
                InitBullet(PoolManager.Instance.GetObject("PlayerBullet",transform.position + (Vector3.right * 1.5f) + (Vector3.down * 0.3f),Quaternion.identity));
                break;
            }
            case 4:
            {
                InitBullet(PoolManager.Instance.GetObject("PlayerBullet",transform.position + (Vector3.right * 3),Quaternion.identity));
                InitBullet(PoolManager.Instance.GetObject("PlayerBullet",transform.position + (Vector3.right * 1.5f) + (Vector3.up * 0.3f),Quaternion.identity));
                InitBullet(PoolManager.Instance.GetObject("PlayerBullet",transform.position + (Vector3.right * 1.5f) + (Vector3.down * 0.3f),Quaternion.identity));
                break;
            }
            case 5:
            {
                InitBullet(PoolManager.Instance.GetObject("PlayerBullet",transform.position + (Vector3.right * 3),Quaternion.identity));
                InitBullet(PoolManager.Instance.GetObject("PlayerBullet",transform.position + (Vector3.right * 1.5f) + (Vector3.up * 0.3f),Quaternion.identity));
                InitBullet(PoolManager.Instance.GetObject("PlayerBullet",transform.position + (Vector3.right * 1.5f) + (Vector3.down * 0.3f),Quaternion.identity));
                InitBullet(PoolManager.Instance.GetObject("PlayerBullet",transform.position + (Vector3.right * 1) + (Vector3.up * 0.6f),Quaternion.identity));
                InitBullet(PoolManager.Instance.GetObject("PlayerBullet",transform.position + (Vector3.right * 1) + (Vector3.down * 0.6f),Quaternion.identity));
                break;
            }
        }
    }
}
