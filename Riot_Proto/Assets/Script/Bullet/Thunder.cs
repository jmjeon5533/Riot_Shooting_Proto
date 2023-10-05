using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Thunder : MonoBehaviour
{
    [SerializeField] float time;
    public float radius;
    int damage;

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        //transform.DOScaleX(radius, time).SetEase(Ease.OutExpo);
        //transform.DOScaleZ(radius, time).SetEase(Ease.OutExpo);
        var hit = Physics.OverlapBox(transform.position, new Vector3(radius,10, 2), Quaternion.identity);
        player = GameManager.instance.player;
        foreach (var h in hit)
        {
            if (h.CompareTag("Enemy"))
            {
                float chance = Random.Range(0, 100f);
                h.GetComponent<EnemyBase>().Damage((chance <= player.CritRate)
                        ? (int)(damage * player.CritDamage) : damage, (chance <= player.CritRate) ? true : false);
                
                
            }
        }
        Destroy(gameObject, time);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Enemy"))
        //{
        //    other.GetComponent<EnemyBase>().Damage((Random.Range(0, 100f) <= player.CritRate)
        //            ? (int)(damage * player.CritDamage) : damage);
        //}
    }

    public void SetDamage(int value)
    {
        damage = value;
    }
}
