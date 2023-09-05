using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricChain : AbilityBase
{
    [SerializeField] float curCooltime = 0;
    [SerializeField] int maxCooltime;

    [SerializeField] int defaultDamage;
    [SerializeField] float duration;

    [SerializeField] GameObject electric;

    [SerializeField] int increaseValue;
    [SerializeField] float damageRate;


    public override void Ability()
    {
        throw new System.NotImplementedException();
    }

    public override string GetStatText()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    public override void Start()
    {
        Initalize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
