using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : BulletBase
{
    private const float originSpeed = 10;
    private const float originSize = 0.3f;
    protected Vector2 bulletDir;

    protected void OnEnable()
    {
        MoveSpeed = originSpeed;
        transform.localScale = Vector3.one * originSize;
    }

    protected override void Update()
    {
        base.Update();
        transform.Translate(bulletDir * MoveSpeed * Time.deltaTime);

        var hit = Physics.OverlapSphere(transform.position, radius);
        foreach (var h in hit)
        {
            if (h.CompareTag("Player"))
            {
                h.GetComponent<Player>().Damage();
                PoolManager.Instance.PoolObject(BulletTag, gameObject);
            }
        }
    }
}
