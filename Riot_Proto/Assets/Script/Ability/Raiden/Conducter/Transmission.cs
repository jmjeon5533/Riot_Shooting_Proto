using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmission : AbilityBase
{
    [SerializeField] float maxCooltime;
    [SerializeField] float curCooltime;

    [SerializeField] int defaultDamage;
    [SerializeField] float damageRate;

    [SerializeField] float increaseValue;

    [SerializeField] GameObject bullet;

    [SerializeField] float radius;
    [SerializeField] int maxAttack;


    Player player;

    public override void Ability()
    {
        curCooltime += Time.deltaTime;
        if(curCooltime >= maxCooltime)
        {
            curCooltime = 0;
            var b = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<TransmissionBullet>();
            b.transRadius = radius;
            b.damage = (defaultDamage + (int)(player.damage * damageRate));
            b.maxAttack = maxAttack;

        }
    }

    public override string GetStatText()
    {
        return "스킬 데미지 " + defaultDamage + " → " + (defaultDamage + (int)(increaseValue * Mathf.Pow((1 + 0.2f), level)))
            + "스킬 범위" + radius + " → " + (radius + (increaseValue/6 * Mathf.Pow((1 + 0.2f), level)));
    }

    public override void LevelUp()
    {
        base.LevelUp();
        defaultDamage += (int)(increaseValue * Mathf.Pow((1 + 0.2f), level));
    }

    // Start is called before the first frame update
    public override void Start()
    {
        Initalize();
    }

    public override void Initalize()
    {
        player = GameManager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        Ability();
    }
}
