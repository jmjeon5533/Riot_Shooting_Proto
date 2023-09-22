using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillBullet : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] float delay;
    [SerializeField] float attackTime;

    [SerializeField] string poolTag;

    float time = 0;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time+=Time.deltaTime;
        if(time >= duration) {
            PoolManager.Instance.PoolObject(poolTag,this.gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var g = GameManager.instance;
        if (time > delay && time <= attackTime)
        {
            if(other.gameObject == g.player.gameObject)
            {
                g.player.Damage();
            }
        }
    }
}
