using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricShock : AbilityBase, IListener
{
    [SerializeField] int stack = 0;
    [SerializeField] int maxStack;

    [SerializeField] int defaultDamage;

    [SerializeField] GameObject bullet;  

    [SerializeField] int increaseValue;
    [SerializeField] float damageRate;


    public override void Ability()
    {
        if (stack >= maxStack)
        {
            stack = 0;
            var b = Instantiate(bullet, player.transform.position, Quaternion.identity);
            b.GetComponent<BulletBase>().Damage = defaultDamage + (int)(player.damage * damageRate);

        }
    }

    public override string GetStatText()
    {
        return "��ų ������ " + defaultDamage + " �� " + (defaultDamage + (int)(increaseValue * Mathf.Pow((1 + 0.2f), level))) +
            " �ʿ� ���� Ƚ�� " + maxStack + " �� " + (maxStack - 1);
    }

    public void OnEvent(Event_Type type, Component sender, object param = null)
    {
        if (type == Event_Type.PlayerAttack)
        {
            stack++;
        }
    }

    public override void LevelUp()
    {
        base.LevelUp();
        defaultDamage += (int)(increaseValue * Mathf.Pow((1 + 0.2f), level));
        maxStack--;
    }

    // Start is called before the first frame update
    public override void Start()
    {
        EventManager.Instance.AddListener(Event_Type.PlayerAttack, this);
        Initalize();
    }

    // Update is called once per frame
    void Update()
    {
        
        Ability();
    }

}
