using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StaticZone : MonoBehaviour
{
    private float radius;
    private int damage;
    private float livingTime;
    private float curTime = 0;
    private float curDelay = 0;
    private float delay;

    public void Init(int damage, float radius, float livingTime, float delay)
    {
        this.damage = damage;
        this.radius = radius;
        this.livingTime = livingTime;
        this.delay = delay;
    }

    Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player;
        
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one * 0.3f, 0.5f).SetEase(Ease.OutElastic);
        //transform.localScale = Vector3.one * radius;
    }

    // Update is called once per frame
    void Update()
    {
        
        curTime += Time.deltaTime;
        curDelay += Time.deltaTime;
        transform.position = player.transform.position;
        if(curDelay >= delay)
        {
            Debug.Log("D");
            
            curDelay = 0;
            var hit = Physics.OverlapSphere(transform.position, radius);
            foreach (var h in hit)
            {
                if (h.CompareTag("Enemy"))
                {
                    float chance = Random.Range(0, 100f);
                    h.GetComponent<EnemyBase>().Damage((chance <= player.CritRate)
                            ? (int)(damage * player.CritDamage) : damage, (chance <= player.CritRate) ? true : false);
                    
                    
                }
            }
        }
        if (curTime >= livingTime)
        {
            Destroy(gameObject);
        }
    }
}
