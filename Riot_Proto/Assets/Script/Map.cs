using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private float MoveSpeed;
    public int Number = 0;
    private void Start()
    {
        MoveSpeed = GameManager.instance.BGSpeed;
    }
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed);
        if(transform.localPosition.x <= -1920)
        {
            transform.localPosition += new Vector3(3840,0);
        }
    }
}
