using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {get; private set;}
    public Transform player;
    public Vector2 MoveRange;
    public List<GameObject> playerPrefab = new List<GameObject>();
    public List<GameObject> StagePrefab = new List<GameObject>();
    
    public bool IsGame = false;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Instantiate(playerPrefab[SceneManager.instance.CharIndex],new Vector3(-12f,0,0),Quaternion.identity);
        Instantiate(StagePrefab[SceneManager.instance.StageIndex],new Vector3(0,0,5f),Quaternion.identity);
    }
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, MoveRange * 2);
    }
}
