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

    [SerializeField] Camera[] Mcamera;
    [SerializeField] Transform CharButtonParant;
    [SerializeField] Transform StageButtonParant;
    [SerializeField] Button OptionButton;
    [Space(10)]
    [SerializeField] RawImage CharImage;
    [SerializeField] Transform explainPanel;
    [Space(10)]
    public Text moraText;
    [Space(10)]
    [Header("ActiveSkill")]
    public Sprite[] ASkillSprite;
    public string[] ASkillName;
    public string[] ASkillExplain;
    public int[] ASkillCoolTime;
    [Space(10)]
    [SerializeField] Transform ASkillParent;
    [SerializeField] Image ASkillImage;
    [SerializeField] Image ASkillBorder;
    [SerializeField] Text ASkillNameText;
    [SerializeField] Text ASkillExplainText;
    [SerializeField] Text ASkillCoolTimeText;
    [SerializeField] GameObject ActivePrefab;

    RawImage SelectRawImage;
    Vector3 returnPos;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SceneManager.instance.SetResolution(Mcamera);
        explainPanel.gameObject.SetActive(false);
        OptionButton.onClick.AddListener(() => SceneManager.instance.Option(0));
        CharButtonInit();
        StageButtonInit();
        ASkillButtonAdd();
        InitPanel(0);
    }
    public void ASkillButtonAdd()
    {
        var p = SceneManager.instance.playerData;
        for (int i = 0; i < p.abilitiy.Count; i++)
        {
            var b = Instantiate(ActivePrefab, ASkillParent).GetComponent<Button>();
            b.image.sprite = ASkillSprite[p.abilitiy[i].index];
            Color[] colors = { Color.yellow, new Color(1,0.5f,0,1), Color.red};
            b.transform.GetChild(0).GetComponent<Image>().color = colors[p.abilitiy[i].level - 1];
            var index = p.abilitiy[i].index;
            var num = i;
            b.onClick.AddListener(() =>
            {
                SceneManager.instance.ActiveIndex = index;
                ASkillImage.sprite = ASkillSprite[index];
                ASkillBorder.color = colors[p.abilitiy[num].level - 1];
                ASkillNameText.text = ASkillName[index];
                ASkillCoolTimeText.text = ASkillCoolTime[index].ToString() + "s";
                ASkillExplainText.text = ASkillExplain[index];
            });
        }
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
