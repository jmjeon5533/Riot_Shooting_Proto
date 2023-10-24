using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floated_Script : MonoBehaviour
{
    Vector3 curPos;
    float curTime;
    public float floating_Value;
    void Start()
    {
        curPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        var Y = curPos.y + (Mathf.Sin(curTime) * floating_Value);
        transform.position = new Vector3(curPos.x,Y,curPos.z);
    }
}
