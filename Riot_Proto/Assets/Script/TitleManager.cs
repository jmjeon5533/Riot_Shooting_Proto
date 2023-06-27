using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField] Transform canvas;
    [HideInInspector] public List<GameObject> Panel = new List<GameObject>();

    private void Start()
    {
        for(int i = 0; i < canvas.childCount; i++)
        {
            Panel.Add(canvas.GetChild(i).gameObject);
        }
        InitPanel(0);
    }
    public void InitPanel(int index)
    {
        for(int i= 0; i < Panel.Count; i++)
        {
            Panel[i].SetActive(false);
        }
        Panel[index].SetActive(true);
    }
    
    public void Exit()
    {
        Application.Quit();
    }
}
