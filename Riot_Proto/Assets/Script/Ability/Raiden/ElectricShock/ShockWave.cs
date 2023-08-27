using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : AbilityBase
{
    [SerializeField] float curTime = 0;
    [SerializeField] float maxCooltime;

    [SerializeField] int defaultDamage;

    [SerializeField] float delay;
    [SerializeField] float range;

    [SerializeField] float duration;
    [SerializeField] float slowRate;

    [SerializeField] GameObject wave;

    Player player;

    [SerializeField] int increaseValue;
    [SerializeField] float damageRate;

    public override void Ability()
    {
        curTime += Time.deltaTime;
        if (curTime >= maxCooltime)
        {
            curTime = 0;
            var b = Instantiate(wave, player.transform.position, Quaternion.identity).GetComponent<StaticZone>();
            
        }
    }

    public override string GetStatText()
    {
        return "스킬 데미지 " + defaultDamage + " → " + (defaultDamage + (int)(increaseValue * Mathf.Pow((1 + 0.2f), level))) +
            " 스킬 범위 " + range + " → " + (range + (increaseValue/2 * Mathf.Pow((1 + 0.1f), level)));
    }

    public override void LevelUp()
    {
        base.LevelUp();
        defaultDamage += (int)(increaseValue * Mathf.Pow((1 + 0.2f), level));
        range += (increaseValue / 2 * Mathf.Pow((1 + 0.1f), level));
    }

    // Start is called before the first frame update
    public override void Start()
    {
        player = GameManager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        Ability();
    }
}
