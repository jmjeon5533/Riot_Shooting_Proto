using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {get; private set;}

    void Awake()
    {
        instance = this;
    }
    public Vector2 MoveRange;
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, MoveRange * 2);
    }
}
