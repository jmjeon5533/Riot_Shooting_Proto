using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using System.IO;

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
    public Text[] moraText;
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
    [SerializeField] GameObject ASkillPrefab;
    [Space(10)]
    [Header("Buy")]
    public Ability buyAbility;
    BuyButton selectSkill;
    [SerializeField] Image BuySkillImage;
    [SerializeField] Image BuySkillBorder;
    [SerializeField] Text BuySkillNameText;
    [SerializeField] Text BuySkillExplainText;
    [SerializeField] Text BuyMoney;
    RawImage SelectRawImage;
    Vector3 returnPos;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        var p = SceneManager.instance.playerData;
        SceneManager.instance.SetResolution(Mcamera);
        explainPanel.gameObject.SetActive(false);
        OptionButton.onClick.AddListener(() => SceneManager.instance.Option(0));
        CharButtonInit();
        StageButtonInit();
        for (int i = 0; i < p.abilitiy.Count; i++)
        {
            ASkillButtonAdd(i);
        }
        InitPanel(0);
    }
    public void SelectBuySkill(BuyButton btn)
    {
        selectSkill = btn;
        Color[] colors = { Color.yellow, new Color(1, 0.5f, 0, 1), Color.red };
        BuySkillImage.sprite = ASkillSprite[btn.ability.index];
        BuySkillBorder.color = colors[btn.ability.level - 1];
        BuySkillNameText.text = ASkillName[btn.ability.index];
        BuySkillExplainText.text = ASkillExplain[btn.ability.index];
        BuyMoney.text = (btn.ability.level * 2000).ToString();
    }
    public void BuyButtonSelect()
    {
        if (SceneManager.instance.playerData.PlayerMora >= selectSkill.ability.level * 6000 && !selectSkill.isSale)
        {
            SceneManager.instance.playerData.abilitiy.Add(selectSkill.ability);
            ASkillButtonAdd(SceneManager.instance.playerData.abilitiy.Count - 1);
            SceneManager.instance.playerData.PlayerMora -= selectSkill.ability.level * 6000;
            selectSkill.saleImage.color = new Color(0, 0, 0, 0.8f);
            selectSkill.isSale = true;
            InitPanel(2);
        }
    }
    public void ASkillButtonAdd(int i)
    {
        var p = SceneManager.instance.playerData;
        var b = Instantiate(ASkillPrefab, ASkillParent).GetComponent<Button>();
        b.image.sprite = ASkillSprite[p.abilitiy[i].index];
        Color[] colors = { Color.yellow, new Color(1, 0.5f, 0, 1), Color.red };
        b.transform.GetChild(0).GetComponent<Image>().color = colors[p.abilitiy[i].level - 1];
        var ab = p.abilitiy[i];
        var num = i;
        b.onClick.AddListener(() =>
        {
            SceneManager.instance.ActiveIndex = ab.index;
            SceneManager.instance.ActiveLevel = ab.level;
            ASkillImage.sprite = ASkillSprite[ab.index];
            ASkillBorder.color = colors[p.abilitiy[num].level - 1];
            ASkillNameText.text = ASkillName[ab.index];
            ASkillCoolTimeText.text = ASkillCoolTime[ab.index].ToString() + "s";
            ASkillExplainText.text = ASkillExplain[ab.index];
        });
    }
    public void InitPanel(int index) //타이틀 패널 바꾸기
    {
        for (int i = 0; i < Panel.Count; i++)
        {
            Panel[i].SetActive(false);
        }
        Panel[index].SetActive(true);
        if (index.Equals(4))
        {
            StageButtonInit();
            Debug.Log("Test");
        }
        Panel[3].transform.GetChild(0).gameObject.SetActive(true);
        explainPanel.gameObject.SetActive(false);
        CharImage.color = Color.clear;
        CharImage.transform.localScale = new Vector3(1, 1, 1);
        moraText[0].text = SceneManager.instance.playerData.PlayerMora.ToString();
        moraText[1].text = SceneManager.instance.playerData.PlayerMora.ToString();

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
    public void GetMora()
    {
        SceneManager.instance.playerData.PlayerMora += 1000;
        InitPanel(2);
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
        SceneManager.instance.JsonSave();
        Application.Quit();
    }
    void OnApplicationQuit()
    {

    }
}
