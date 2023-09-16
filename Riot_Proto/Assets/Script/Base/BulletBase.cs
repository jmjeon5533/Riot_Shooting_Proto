using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public float MoveSpeed;
    private float prevSpeed = 99999;
    public Vector3 dir;
    [SerializeField] protected float radius;
    [HideInInspector] public int Damage;
    [HideInInspector] public int CritRate;
    [HideInInspector] public float CritDamage;
    [SerializeField] string BulletTag;
    protected virtual void Start()
    {

    }

    private void OnEnable()
    {
        if (prevSpeed == 99999) return;
        MoveSpeed = prevSpeed;
    }

    public void SetMoveSpeed(float value)
    {
        prevSpeed = MoveSpeed;
        MoveSpeed = value;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position += dir * MoveSpeed * Time.deltaTime;
        MapOut();
    }
    protected virtual void MapOut()
    {
        if (Mathf.Abs(transform.position.x) >= GameManager.instance.MoveRange.x + 5
        || Mathf.Abs(transform.position.y) >= GameManager.instance.MoveRange.y + 5)
        {
            PoolManager.Instance.PoolObject(BulletTag, gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
