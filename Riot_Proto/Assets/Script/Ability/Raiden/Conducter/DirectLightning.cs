using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectLightning : AbilityBase, IListener
{
    [SerializeField] int stack = 0;
    [SerializeField] int maxStack;

    [SerializeField] int defaultDamage;

    [SerializeField] GameObject bullet;

    [SerializeField] int increaseValue;
    [SerializeField] float damageRate;
    

    public override void Ability()
    {
        if(stack >= maxStack)
        {
            useSkill = false;
            stack = 0;
            minCool = stack;
            ResetTimerUI(1);
            useSkill = true;
            SoundManager.instance.SetAudio("Kaisa", SoundManager.SoundState.SFX, false, 1f);
            var b = Instantiate(bullet, player.transform.position + (Vector3.right * 1.5f), Quaternion.identity);
            b.GetComponent<BulletBase>().Damage = defaultDamage + (int)(player.damage * damageRate);
           
        }
    }

    public override string GetStatText()
    {
        return "스킬 데미지 " + defaultDamage + " → " + (defaultDamage + (int)(increaseValue * Mathf.Pow((1 + 0.2f), level+1))) +
            " 필요 공격 횟수 " + maxStack + " → " + (maxStack-1);
    }

    public void OnEvent(Event_Type type, Component sender, object param = null)
    {
        if(type == Event_Type.PlayerAttack)
        {
            stack++;
            minCool = stack;
        }
    }

    public override void LevelUp()
    {
        base.LevelUp();
        defaultDamage += (int)(increaseValue * Mathf.Pow((1 + 0.2f), level));
        maxStack--;
        maxCool = maxStack;
    }

    // Start is called before the first frame update
    public override void Start()
    {
        EventManager.Instance.AddListener(Event_Type.PlayerAttack, this);
        stack = maxStack-1;
        minCool = stack;
        Initalize();
        maxCool = maxStack;
        useSkill = true;
    }


    // Update is called once per frame
    void Update()
    {
        Ability();
    }
}
