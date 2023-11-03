using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowLazer : MonoBehaviour
{
    [SerializeField] float startDelay;
    [SerializeField] float duration;
    [SerializeField] int Damage;
    bool isAttack = false;

    float curCooltime = 0;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player;
    }

    private void OnEnable()
    {
        curCooltime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    public void Init(int Damage)
    {
        this.Damage = Damage;
    }

    void Attack()
    {
        curCooltime += Time.deltaTime;
        if(curCooltime >= startDelay && !isAttack)
        {
            Collider[] hits = Physics.OverlapBox(new Vector3(transform.position.x + 50, transform.position.y, transform.position.z), new Vector3(100, 0.7f,1), Quaternion.identity);
            foreach(Collider hit in hits)
            {
                if(hit.CompareTag("Enemy"))
                {
                    float chance = Random.Range(0, 100f);

                    var e = hit.GetComponent<EnemyBase>();
                    Debug.Log(e.name);
                    e.Damage((chance <= player.CritRate)
                    ? (int)(Damage * player.CritDamage) : Damage, (chance <= player.CritRate) ? true : false);
                }
            }
            isAttack = true;
        }
        if(curCooltime >= duration)
        {
            PoolManager.Instance.PoolObject("Lazer", this.gameObject);
        }
    }
}
