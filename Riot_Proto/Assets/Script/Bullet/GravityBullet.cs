using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBullet : BulletBase
{
    bool isBounce = false;

    float gravityScale;

    Rigidbody rb;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        MapOut();
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void SetMoveSpeed(float value)
    {
        base.SetMoveSpeed(value);

    }

    public void SetGravity(float value)
    {
        gravityScale = value;
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        Vector3 gravity = 9.81f * gravityScale * Vector3.down;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    public void Bounce()
    {
        rb.AddForce(Vector3.up * MoveSpeed / 2);
        rb.AddForce(Vector3.left * MoveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player p = GameManager.instance.player;
        if (other.gameObject.Equals(p.gameObject))
        {
            p.Damage();
            PoolManager.Instance.PoolObject(BulletTag, this.gameObject);    
            
        }
    }
}
