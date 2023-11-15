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

    bool isUp = false;
    public bool isSound = false;

    private void OnEnable()
    {
        time = 0;
        isUp = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time+=Time.deltaTime;
        if (time > delay && time <= attackTime && !isUp && isSound)
        {
            isUp = true;
            SoundManager.instance.SetAudio("FireUp", SoundManager.SoundState.SFX, false);
        }
        if (time >= duration) {
            PoolManager.Instance.PoolObject(poolTag,this.gameObject);
        }
        
    }


    private void OnTriggerStay(Collider other)
    {
        var g = GameManager.instance;
        if (time > delay && time <= attackTime)
        {
            if (other.gameObject == g.player.gameObject)
            {
                g.player.Damage();
            }
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
