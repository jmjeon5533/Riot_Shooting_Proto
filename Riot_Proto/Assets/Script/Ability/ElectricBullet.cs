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

    [SerializeField] float angle;

    [SerializeField] float livingDuration;

    public override void Ability()
    {
        curCooltime += Time.deltaTime;
        if (curCooltime > maxCooltime)
        {
            curCooltime = 0;
            float radius = angle;
            int count = level;
            float amount = radius / (level - 1);
            float z = (level == 1) ? 0 : radius / -2f;

            for (int i = 0; i < level; i++)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, z);
                BulletBase b = Instantiate(bullet, GameManager.instance.player.transform.position, Quaternion.identity).GetComponent<BulletBase>();
                b.MoveSpeed = speed;
                b.Damage = defaultDamage;
                b.transform.rotation = rotation;
                z += amount;
            }
        }
    }

    public override string GetStatText()
    {
        return "스킬 데미지 " + defaultDamage + " → " + (defaultDamage + (int)(5 * Mathf.Pow((1 + 0.2f), level))) + "\n탄환 개수 " + level + " → " + (level+1);
    }

    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ability();
    }
}
