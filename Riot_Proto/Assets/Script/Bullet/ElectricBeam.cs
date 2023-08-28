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

                    h.GetComponent<EnemyBase>().Damage((Random.Range(0, 100f) <= player.CritRate)
                        ? (int)(damage * player.CritDamage) : damage);

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
        Attack();
    }
}
