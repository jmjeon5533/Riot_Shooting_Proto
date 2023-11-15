using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ElectricCall : AbilityBase
{
    [SerializeField] float curCooltime;
    [SerializeField] float maxCooltime;

    [SerializeField] int defaultDamage;

    [SerializeField] GameObject thunder;
    [SerializeField] ParticleSystem thunderDrain;

    [SerializeField] float radius;

    [SerializeField] int increaseValue;
    [SerializeField] float damageRate;
    [SerializeField] float delay;

    int attackCount = 0;

    public override void Ability()
    {
        
        if(!useSkill)
        {
            List<GameObject> list = GameManager.instance.curEnemys.ToList();
            if (list.Count == 0) return;
            player.Shield(2.5f);
            curCooltime =0;
            useSkill = true;
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        ThunderDrain();
        SoundManager.instance.SetAudio("ElectricCall", SoundManager.SoundState.SFX, false, 1f);
        yield return new WaitForSeconds(1.5f);
        if (attackCount > 7) attackCount = 7; 
        for(int i=0;i<attackCount;i++)
        {
            ThunderDrop();
            SoundManager.instance.SetAudio("ThunderBolt", SoundManager.SoundState.SFX, false, 1f);
            yield return new WaitForSeconds(delay);
        }
        attackCount = 0;
        
    }

    void ThunderDrop()
    {
        Collider[] list = Physics.OverlapBox(Vector3.zero, new Vector3(GameManager.instance.MoveRange.x, GameManager.instance.MoveRange.y, 1), Quaternion.identity, LayerMask.GetMask("Enemy"));
        //List<GameObject> list = GameManager.instance.curEnemys.ToList();
        if (list.Length == 0) return;
        int damage = defaultDamage + (int)(player.damage * damageRate);
        
            Transform target = list[Random.Range(0, list.Length)].transform;
            while(target == null || !target.gameObject.activeSelf)
            {
            
                list = Physics.OverlapBox(Vector3.zero, new Vector3(GameManager.instance.MoveRange.x, GameManager.instance.MoveRange.y, 1), Quaternion.identity, LayerMask.GetMask("Enemy"));
                target = list[Random.Range(0, list.Length)].transform;
            }
            Instantiate(thunder, new Vector3(target.position.x, 0, target.position.z), Quaternion.identity).GetComponent<Thunder>()
               .SetDamage(damage);
            
    }

    void ThunderDrain()
    {

        Collider[] hits = Physics.OverlapBox(Vector3.zero, new Vector3(GameManager.instance.MoveRange.x, GameManager.instance.MoveRange.y, 1),Quaternion.identity, LayerMask.GetMask("Enemy"));
        //List<GameObject> list = new List<GameObject>(GameManager.instance.curEnemys);
        if (hits.Length == 0) return;
        int damage = defaultDamage/2 + (int)(player.damage * damageRate);
        foreach (var enemy in hits)
        {
            if (enemy != null)
            {
                float chance = Random.Range(0, 100f);
                if (!enemy.gameObject.activeSelf) continue;
                if (enemy.GetComponent<Alert>() != null || enemy.GetComponent<EnemyBase>() == null) continue;
                var t = Instantiate(thunderDrain, enemy.transform);
                t.transform.localPosition = Vector3.zero;
                
                enemy.GetComponent<EnemyBase>().Damage((chance <= player.CritRate)
                        ? (int)(damage * player.CritDamage) : damage, (chance <= player.CritRate) ? true : false);
                
                attackCount++;
            }
        }
    }

    public override string GetStatText()
    {
        return "스킬 데미지 " + defaultDamage + " → " + (defaultDamage + (int)(increaseValue * Mathf.Pow((1 + 0.2f), level+1)));
    }

    // Start is called before the first frame update
    public override void Start()
    {
        player = GameManager.instance.player;
    }

    public override void LevelUp()
    {
        base.LevelUp();
        defaultDamage += (int)(increaseValue * Mathf.Pow((1 + 0.2f), level));
    }

    // Update is called once per frame
    void Update()
    {
        if (useSkill)
        {

            curCooltime += Time.deltaTime;
            minCool = curCooltime;
            maxCool = maxCooltime;
            if (curCooltime >= maxCooltime)
            {
                curCooltime = 0;
                useSkill = false;
            }
        }
    }
}
