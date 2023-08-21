using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public float MoveSpeed;
    public Vector3 dir;
    [SerializeField] protected float radius;
    [HideInInspector] public int Damage;
    [HideInInspector] public int CritRate;
    [HideInInspector] public float CritDamage;
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.Translate(dir * MoveSpeed * Time.deltaTime);
        MapOut();
    }
    protected virtual void MapOut()
    {
        if (Mathf.Abs(transform.position.x) >= GameManager.instance.MoveRange.x + 5
        || Mathf.Abs(transform.position.y) >= GameManager.instance.MoveRange.y + 5)
        {
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
