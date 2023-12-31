    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricRush : AbilityBase
{
    [SerializeField] GameObject bullet;
    [SerializeField] int defaultDamage;
    [SerializeField] float damageRate;

    [SerializeField] float angle;

    [SerializeField] int count;

    [SerializeField] float maxCooltime;
    [SerializeField] float curCooltime;

    [SerializeField] float defRate;

    [SerializeField] float increaseValue;

    [SerializeField] float duration;
    [SerializeField] float multiplier;

    [SerializeField] float speed;



    public override void ResizingCooldown()
    {
        maxCooltime = originCooltime - (originCooltime * SubtractCool);
        
        maxCool = maxCooltime;
    }


    public override void Ability()
    {
        curCooltime+=Time.deltaTime;
        minCool = curCooltime;
        if(curCooltime >= maxCooltime)
        {
            curCooltime = 0;
            useSkill = false;
            ResetTimerUI(1);
            float radius = angle;
            
            float amount = radius / (count - 1);
            float z = (radius) / -2f;

            SoundManager.instance.SetAudio("Kaisa", SoundManager.SoundState.SFX, false, 0.9f);
            for (int i = 0; i < count; i++)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, z);
                RushBullet b = PoolManager.Instance.GetObject("RushBullet", player.transform.position+(Vector3.right * 1.5f), rotation).GetComponent<RushBullet>();
                b.Init(duration, multiplier, defaultDamage + (int)(player.damage * damageRate),speed);
                b.transform.rotation = rotation;
                z += amount;
            }
            useSkill = true;
        }
    }

    public override string GetStatText()
    {
        return "스킬 데미지 " + defaultDamage + " → " + (defaultDamage + (int)(increaseValue * Mathf.Pow((1 + 0.2f), level+1)))
            + "\n디버프 지속 시간 " + duration + " → " + (Mathf.Round((duration + (increaseValue / 4 * Mathf.Pow((1 + 0.05f), level+1))) * 100) / 100) +
            "\n받는 피해 증가 " + (multiplier * 100) + "% →" + ((multiplier + 0.03f) * 100) + "% ";
    }

    public override void LevelUp()
    {
        base.LevelUp();
        defaultDamage += (int)(increaseValue * Mathf.Pow((1 + 0.2f), level));
        duration += (increaseValue / 4 * Mathf.Pow((1 + 0.05f), level));
        multiplier += 0.03f;    
    }

    // Start is called before the first frame update
    public override void Start()
    {
        Initalize();
        curCooltime = maxCooltime-1;
        minCool = maxCooltime;
        originCooltime = maxCooltime;

        useSkill = true;
        maxCool = maxCooltime;
    }

    // Update is called once per frame
    void Update()
    {
        Ability();
    }
}
