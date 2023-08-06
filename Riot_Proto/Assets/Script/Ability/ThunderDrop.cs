using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderDrop : AbilityBase
{
    [SerializeField] float maxCooltime;
    [SerializeField] float curCooltime;

    [SerializeField] int defaultDamage;

    [SerializeField] GameObject thunder;
    Player player;
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

    public override void Ability()
    {
        curCooltime += Time.deltaTime;
        if (GameManager.instance.curEnemys != null && curCooltime >= maxCooltime && GameManager.instance.curEnemys.Count > 0 )
        {
            StartCoroutine(Drop());
            curCooltime = 0;
        }
    }

    IEnumerator Drop()
    {
        for(int i = 0; i < level; i++)
        {
            if (GameManager.instance.curEnemys != null && GameManager.instance.curEnemys.Count == 0) continue; 
            Transform target = GameManager.instance.curEnemys[Random.Range(0, GameManager.instance.curEnemys.Count)].transform;
            if (target == null) continue; 
            Instantiate(thunder, new Vector3(target.position.x, 0, target.position.z), Quaternion.identity).GetComponent<Thunder>()
                .SetDamage(defaultDamage * level);
            yield return new WaitForSeconds(0.3f);
        } 
    }
}
