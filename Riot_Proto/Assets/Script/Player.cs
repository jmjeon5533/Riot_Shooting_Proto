using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    
    public float MoveSpeed;
    Vector3 MoveRange;
    void Start()
    {
        MoveRange = GameManager.instance.MoveRange;
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }
    void Update()
    {
        Vector2 input 
        = new Vector2(Input.GetAxisRaw("Horizontal"),
        Input.GetAxisRaw("Vertical"));

        transform.Translate(input * MoveSpeed * Time.deltaTime);

        transform.position
         = new Vector3(Mathf.Clamp(transform.position.x,-MoveRange.x,MoveRange.x),
         Mathf.Clamp(transform.position.y,-MoveRange.y,MoveRange.y));
    }
}
