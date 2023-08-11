using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private float MoveSpeed;
    public RectTransform rect;
    private void Start()
    {
        rect = GetComponent<RectTransform>();
        MoveSpeed = GameManager.instance.BGSpeed;
    }
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed);
        if(transform.localPosition.x <= -rect.rect.width)
        {
            transform.localPosition += new Vector3(rect.rect.width * 2,0);
        }
    }
}
