using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    
    public float MoveSpeed;

    public int atkDamage;

    public bool isAttack = true;

    public bool isCool = false;

    [SerializeField] float maxAttackCooltime;
    [SerializeField] float curAttackCooltime = 0;
    [SerializeField] float curAttacktime = 0;

    public GameObject bullet;

    Vector3 MoveRange;
    void Start()
    {
        MoveRange = GameManager.instance.MoveRange;
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }
    void Update()
    {
        Movement();
        Attack();
    }

    void Attack()
    {
        curAttackCooltime += Time.deltaTime;
        curAttacktime += Time.deltaTime;
        if(curAttacktime > maxAttackCooltime)
        {
            isCool = false;
            curAttacktime = 0;
        }
        if(curAttackCooltime > maxAttackCooltime)
        {
            curAttackCooltime = 0;
            isAttack = false;
           
        }
        if(Input.GetButton("Fire1") && !isCool)
        {
            Instantiate(bullet,transform.position, Quaternion.identity);
            curAttackCooltime = 0;
            isAttack = true;
            isCool = true;
        }
    }

    void Movement()
    {
        Vector2 input
       = new Vector2(Input.GetAxisRaw("Horizontal"),
       Input.GetAxisRaw("Vertical"));

        transform.Translate(input * MoveSpeed * Time.deltaTime);

        transform.position
         = new Vector3(Mathf.Clamp(transform.position.x, -MoveRange.x, MoveRange.x),
         Mathf.Clamp(transform.position.y, -MoveRange.y, MoveRange.y));
    }


}
