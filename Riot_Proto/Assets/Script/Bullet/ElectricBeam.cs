using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBeam : MonoBehaviour
{
    public float duration;
    public float livingTime;
    public float delay;
    [SerializeField] private float curAttackTime = 0;
    [SerializeField] private float maxAttackTime;
    
    private float curTime = 0;
    Player player;
    public int damage;

    bool isSound = false;

    [SerializeField] Vector3 size;
    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 pos;


    void UpdateLivingTime()
    {
        curTime += Time.deltaTime;
        if (curTime >= livingTime)
        {
            Destroy(gameObject);
        }
    }

    void Attack()
    {
        curAttackTime += Time.deltaTime;
        if(curAttackTime >= maxAttackTime)
        {
            curAttackTime = 0;
            var hit = Physics.OverlapBox(transform.position + offset, size, Quaternion.identity);
           

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
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position+offset, size);
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLivingTime();
        transform.position = player.transform.position + pos;
        if(curTime > delay)
        {
            Attack();
            if (!isSound)
            {
                SoundManager.instance.SetAudio("ElectricLine_Shoot", SoundManager.SoundState.SFX, false);
                isSound = true;
            }
        }
    }
}
