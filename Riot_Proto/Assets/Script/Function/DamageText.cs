using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public RectTransform rect;
    [SerializeField] float UpSpeed;
    public Text text;
    public float timeCount = 1;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeCount -= Time.deltaTime;
        text.color = new Color(1,1,1,timeCount);
        rect.localScale = new Vector3(1,1,1) * timeCount;
        rect.anchoredPosition += Vector2.up * Time.deltaTime * UpSpeed;
        if(timeCount <= 0)
        {
            PoolManager.Instance.PoolObject("DamageText",gameObject);
        }
    }
}
