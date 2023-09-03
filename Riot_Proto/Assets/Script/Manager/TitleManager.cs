using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class TitleManager : MonoBehaviour
{
    public static TitleManager instance { get; private set; }
    [SerializeField] Camera MainCamera, UICamera;
    public List<GameObject> Panel = new();
    [SerializeField] Transform CharButtonParant;
    [SerializeField] Transform StageButtonParant;
    [SerializeField] Button OptionButton;
    [Space(10)]
    [SerializeField] RawImage CharImage;
    [SerializeField] Transform explainPanel;
    [Space(10)]
    public Text moraText;

    [SerializeField] RawImage SelectRawImage;
    Vector3 returnPos;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Camera[] camera = { MainCamera, UICamera };
        SceneManager.instance.SetResolution(camera);
        explainPanel.gameObject.SetActive(false);
        OptionButton.onClick.AddListener(() => SceneManager.instance.Option(0));
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
        Panel[3].transform.GetChild(0).gameObject.SetActive(true);
        explainPanel.gameObject.SetActive(false);
        CharImage.color = Color.clear;
        CharImage.transform.localScale = new Vector3(1, 1, 1);
        moraText.text = SceneManager.instance.playerData.PlayerMora.ToString();
    }
    public void CharButtonInit() //캐릭터 버튼 초기화
    {
        for (int i = 0; i < CharButtonParant.childCount; i++)
        {
            int j = i;
            var buttonPos = CharButtonParant.GetChild(i);
            var button = buttonPos.GetChild(0).GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                SelectRawImage = button.transform.GetChild(0).GetComponent<RawImage>();
                returnPos = buttonPos.transform.position;
                Panel[3].transform.GetChild(0).gameObject.SetActive(false);
                CharImage.texture = SelectRawImage.texture;
                CharImage.transform.position = SelectRawImage.transform.position;
                CharImage.color = Color.white;

                SceneManager.instance.CharIndex = j;

                SelectMove(true);
            });
        }
    }
    public void SelectMove(bool OnOff)
    {
        StartCoroutine(SelectCharMove(OnOff));
    }
    IEnumerator SelectCharMove(bool OnOff)
    {
        if (!OnOff)
        {
            SelectRawImage = null;
            explainPanel.gameObject.SetActive(false);
            CharImage.transform.DOMove(returnPos, 1);
            yield return CharImage.transform.DOScale(new Vector3(1, 1, 1), 1).WaitForCompletion();
            CharImage.color = Color.clear;
        }
        else
        {
            CharImage.transform.DOLocalMove(new Vector3(-700, 100), 1);
            yield return CharImage.transform.DOScale(new Vector3(1, 1, 1) * 1.5f, 1).WaitForCompletion();
            CharImage.color = Color.white;
        }
        explainPanel.gameObject.SetActive(OnOff);
        Panel[3].transform.GetChild(0).gameObject.SetActive(!OnOff);
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
    void OnApplicationQuit()
    {
        SceneManager.instance.JsonSave();
    }
}
