using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBullet : AbilityBase
{
    [SerializeField] float speed;

    [SerializeField] GameObject bullet;

    [SerializeField] int defaultDamage;

    [SerializeField] float curCooltime = 0;
    [SerializeField] float maxCooltime;

    [SerializeField] float livingDuration;

    public override void Ability()
    {
        curCooltime+=Time.deltaTime;
        if (curCooltime > maxCooltime)
        {
            float radius = 60;
            float amount = radius / (5 - 1);
            float z = radius / -2f;

            for (int i = 0; i < 5; i++)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, z);
                BulletBase b = Instantiate(bullet, GameManager.instance.player.transform.position, Quaternion.identity).GetComponent<BulletBase>();
                b.MoveSpeed = speed;
                
               
                b.Damage = defaultDamage;
                //newObj.isEnemyBullet = true;
               // newObj.transform.position = transform.position;
                //newObj.transform.rotation = rotation;


                z += amount;
            }
        }
    }

    public override string GetStatText()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
