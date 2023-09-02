using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : AbilityBase
{
    [SerializeField] float curTime = 0;
    [SerializeField] float maxCooltime;

    [SerializeField] float delay;
    [SerializeField] float range;

    [SerializeField] float duration;
    [SerializeField] float slowRate;
    [SerializeField] float speed;

    [SerializeField] GameObject wave;

 
    
    [SerializeField] int increaseValue;
    [SerializeField] float damageRate;

    public override void Ability()
    {
        curTime += Time.deltaTime;
        if (curTime >= maxCooltime)
        {
            curTime = 0;
            var b = Instantiate(wave, player.transform.position, Quaternion.identity).GetComponent<ElectricWave>();
            b.Init(range, speed, duration, 1 - slowRate);
        }
    }

    public override string GetStatText()
    {
        return "디버프 지속 시간 " + duration + " → " + (duration + (increaseValue/2 * Mathf.Pow((1 + 0.08f), level))) +
            " 이동 속도 감소 " + (slowRate * 100) + "% →" + ((slowRate+0.04f) * 100) + "% " +
            " 스킬 범위 " + range + " → " + (range + (increaseValue/2 * Mathf.Pow((1 + 0.1f), level)));
    }

    public override void LevelUp()
    {
        base.LevelUp();
        duration += (increaseValue / 2 * Mathf.Pow((1 + 0.08f), level));
        slowRate += 0.04f;
        range += (increaseValue / 2 * Mathf.Pow((1 + 0.1f), level));
    }

    // Start is called before the first frame update
    public override void Start()
    {
        Initalize();
    }

    // Update is called once per frame
    void Update()
    {
        Ability();
    }
}
