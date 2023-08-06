using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Thunder : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] float radius;
    int damage;

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScaleX(radius, time).SetEase(Ease.OutExpo);
        transform.DOScaleZ(radius, time).SetEase(Ease.OutExpo);
        Destroy(gameObject, time);
        player = GameManager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBase>().Damage((Random.Range(0, 100f) <= player.CritRate)
                    ? (int)(damage * player.CritDamage) : damage);
        }
    }

    public void SetDamage(int value)
    {
        damage = value;
    }
}
