using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class TitleManager : MonoBehaviour
{
    public static TitleManager instance { get; private set; }
    public List<GameObject> Panel = new();
    [SerializeField] Transform CharButtonParant;
    [SerializeField] Transform StageButtonParant;
    [SerializeField] Button OptionButton;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        OptionButton.onClick.AddListener(()=>SceneManager.instance.Option(0));
        CharButtonInit();
        StageButtonInit();
        InitPanel(0);
    }
    public void InitPanel(int index) //타이틀 패널 바꾸기
    {
        for (int i = 0; i < Panel.Count; i++)
        {
            Panel[i].SetActive(false);
        }
        Panel[index].SetActive(true);
    }

    public void CharButtonInit() //캐릭터 버튼 초기화
    {
        for (int i = 0; i < CharButtonParant.childCount; i++)
        {
            int j = i;
            var button = CharButtonParant.GetChild(i).GetChild(0).GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                SceneManager.instance.CharIndex = j;
                InitPanel(2);
            });
        }
    }
    public void StageButtonInit()
    {
        for (int i = 0; i < StageButtonParant.childCount; i++)
        {
            int j = i;
            var button = StageButtonParant.GetChild(i).GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                SceneManager.instance.StageIndex = j;
                SceneManager.instance.StageStart();
            });
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
}
