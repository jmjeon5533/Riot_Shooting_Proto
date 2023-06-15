using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public float speed;

    [SerializeField] private int hp;
    [SerializeField] private int maxhp;

    [SerializeField] private int atkDamage;

    [SerializeField] private GameObject bullet;


    //[SerializeField]
    //private float jumpSpeed;
    //[SerializeField] int jumpCount = 0;

    [SerializeField] private float shootCoolTime = 0.5f;
    private float curShootTime = 0;


    bool isShoot = false;
    bool isDamaged = false;

    Rigidbody2D rigid;

    Material material;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        if (Instance != null) Destroy(this.gameObject);
        else Instance = this;
        maxhp = hp;
        material = GetComponent<Material>();
        

    }


    public int GetHP()
    {
        return hp;
    }

    public int GetMaxHP()
    {
        return maxhp;
    }

    public int GetAttackDamage()
    {
        return atkDamage;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        //Jump();
        Shoot();
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        //spriteRenderer.flipX = x >= 0 ? true : false;
        float moveX = (x * speed * Time.deltaTime);
        float moveY = (y * speed * Time.deltaTime);
        float limitX = x * ((transform.position.x + moveX < -17f || transform.position.x + moveX > 17f) ? 0 : speed) * Time.deltaTime;
        float limitY = y * ((transform.position.y + moveY < -6.4f || transform.position.y + moveY > 8.3f) ? 0 : speed / 1.5f) * Time.deltaTime;

        transform.position += new Vector3(limitX, limitY, 0);
    }

    void Shoot()
    {
        curShootTime += isShoot ? Time.deltaTime : 0;
        isShoot = (curShootTime >= shootCoolTime) ? false : isShoot;
        if (Input.GetButton("Fire1") && !isShoot)
        {
            isShoot = true;
            curShootTime = 0;
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
    }

    //void Jump()
    //{
    //    RaycastHit2D hit;


    //    if (Physics2D.Raycast(transform.position, Vector2.down, 2f, LayerMask.GetMask("Floor")) && jumpCount > 0 && rigid.velocity.y < 0)
    //    {
    //        jumpCount = 0;
    //    }

    //    if (Input.GetButtonDown("Jump"))
    //    {


    //        if (jumpCount > 1)
    //            return;
    //        jumpCount++;
    //        rigid.velocity = new Vector2(rigid.velocity.x, jumpSpeed);
    //    }
    //}

    IEnumerator Damage()
    {
        //material.color = Color.red;
        isDamaged = true;
        yield return new WaitForSeconds(0.1f);
        //material.color = Color.white;
        isDamaged = false;
    }

    public void Damaged(int damage)
    {
        if (isDamaged) return;
        StartCoroutine(Damage());
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            Death();
        }
    }

    void Death()
    {
        Time.timeScale = 0f;
        
        gameObject.SetActive(false);

    }
}

