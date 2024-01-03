using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using System.IO;
using System.Linq;

[System.Serializable]
public class ASkillInfo
{
    public Sprite sprite;
    public string name;
    public string explain;
    [Space(10)]
    public int dmg;
    [Range(0, 10)] public float range;
    public int coolTime;
}
[System.Serializable]
public class UpgradeInfo
{
    public string name;
    public Sprite Icon;
    public float UpgradeValue; //레벨당 추가값
    public int Cost;
}
public class TitleManager : MonoBehaviour
{
    public static TitleManager instance { get; private set; }
    public GameObject[] Panel;
    [SerializeField] Transform[] Selectbg;
    [SerializeField] Camera[] Mcamera;
    [SerializeField] Transform[] titleBtn;
    bool isButton;
    [Space(10)]
    [Header("상점 탭")]
    [SerializeField] Transform[] ShopUI;
    [SerializeField] Button[] ShopButton;
    [SerializeField] Button[] StatusButton = new Button[8];
    [SerializeField] Text[] StatusLevel = new Text[8];
    [SerializeField] Text MoneyText;
    [SerializeField] Text Name;
    [SerializeField] Text CurValue;
    [SerializeField] Image Icon;
    [SerializeField] Text Cost;
    int SelectStatus;
    [Space(10)]
    [Header("선택 탭")]
    [SerializeField] RawImage CharImage;
    [SerializeField] RadarGraph graph;
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] Transform[] SelectUI;
    Mesh statusPentagon;
    [Space(10)]
    [Header("스킬 정보")]
    public ASkillInfo[] aSkillInfos;
    [SerializeField] Transform[] ASkillList = new Transform[3];
    [SerializeField] Image[] ASkillStatus;
    [SerializeField] Transform ASkillParent;
    [SerializeField] GameObject ASkillPrefab;
    [SerializeField] RawImage SelectSkillImage;
    [SerializeField] Text ASkillExplain;

    [Header("로고 & 첫 타이틀")]
    [SerializeField] Image startPanel;
    [SerializeField] Image logo;

    [Header("옵션 탭")]
    public Transform OptionTab;
    public bool isOption = false;
    Tween OptionTween;
    public Slider BGMS, SFXS;
    public Toggle DetailCtrlToggle;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SceneManager.instance.SetResolution(Mcamera);
        for (int i = 0; i < 3; i++)
        {
            ASkillButtonAdd(i);
        }
        for (int i = 0; i < StatusButton.Length; i++)
        {
            StatusLevel[i].text = SceneManager.instance.playerData.StatusLevel[i].ToString();
        }
        for (int i = 0; i < ShopUI.Length; i++)
        {
            var num = i;
            ShopButton[num].onClick.AddListener(() =>
            {
                InitShopPanel(num);
            });
        }
        InitShopBtn();
        InitShopPanel(0);
        InitPanel(0);
        titleBtn[0].localPosition = new Vector2(2300, -189);
        titleBtn[1].localPosition = new Vector2(2300, -415);
        titleBtn[2].localPosition = new Vector2(-353, -800);

        SceneManager.instance.ActiveIndex = 0;
        SceneManager.instance.ActiveLevel = 3;
        statusPentagon = new Mesh();
        meshFilter.mesh = statusPentagon;
        //pentagonInit();
        ASkillStatus[0].fillAmount = Mathf.InverseLerp(0, 10, aSkillInfos[0].dmg);
        ASkillStatus[1].fillAmount = Mathf.InverseLerp(0, 10, aSkillInfos[0].range);
        ASkillStatus[2].fillAmount = Mathf.InverseLerp(0, 130, aSkillInfos[0].coolTime);
        ASkillExplain.text = aSkillInfos[0].explain;


        BGMS.value = SoundManager.instance.BGMVolume;
        SFXS.value = SoundManager.instance.SFXVolume;
        DetailCtrlToggle.isOn = SceneManager.instance.DetailCtrl;

        SoundManager.instance.SetAudio("Title1", SoundManager.SoundState.BGM, true);
        SceneManager.instance.JsonSave();

        InitMoney();
    }
    void InitShopPanel(int index)
    {
        for (int j = 0; j < ShopUI.Length; j++)
        {
            ShopUI[j].gameObject.SetActive(false);
        }
        ShopUI[index].gameObject.SetActive(true);
        InitShopStatus(0);
    }
    public void InitMoney()
    {
        var money = SceneManager.instance.playerData.PlayerMoney.ToString();
        MoneyText.text = money;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Option();
        }
        SoundManager.instance.BGMVolume = BGMS.value;
        SoundManager.instance.SFXVolume = SFXS.value;
        SceneManager.instance.DetailCtrl = DetailCtrlToggle.isOn;

        //BACKDOOR!
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SoundManager.instance.BGMVolume = 0;
            SoundManager.instance.SFXVolume = 0;
            StageStart();
        }
    }

    public void Option()
    {
        isOption = !isOption;
        if (OptionTween != null) OptionTween.Kill();
        OptionTween = OptionTab.DOLocalMoveY(isOption ? 0 : 900, 0.5f).SetUpdate(true);
    }

    [RuntimeInitializeOnLoadMethod]
    static void OnAppStart()
    {
        instance.StartCoroutine(instance.StartMotion());
    }
    #region MainPlot

    IEnumerator StartMotion() //처음 시작 타이틀 움직임
    {
        startPanel.DOFade(1, 0);
        yield return new WaitForSeconds(1);
        yield return logo.DOFade(1, 1f).WaitForCompletion();
        startPanel.DOFade(0, 1.5f);
        yield return new WaitForSeconds(1);
        logo.DOFade(0, 1f);


        if (isButton) yield break;
        isButton = true;
        titleBtn[0].DOLocalMoveX(600, 1.25f).SetEase(Ease.InOutBack);
        titleBtn[2].DOLocalMoveY(-206, 1.25f).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(0.2f);
        yield return titleBtn[1].DOLocalMoveX(600, 1.25f).SetEase(Ease.InOutBack)
        .OnComplete(() => isButton = false).WaitForCompletion();
    }

    public void StartButton() //타이틀에서 시작 버튼 누를 시 움직임
    {
        if (isButton) return;
        StartCoroutine(MainMenuStart());
        SoundManager.instance.SetAudio("UIClick", SoundManager.SoundState.SFX, false);
    }

    IEnumerator MainMenuStart()
    {
        yield return StartCoroutine(titleDisappearBtn());

        isButton = true;
        InitPanel(1);
        Selectbg[0].DOLocalMoveY(1, 0.7f);
        yield return Selectbg[1].DOLocalMoveY(-1, 0.7f).WaitForCompletion();

        yield return StartCoroutine(selectStart());
    }
    IEnumerator titleDisappearBtn()
    {
        if (isButton) yield break;
        isButton = true;
        titleBtn[0].DOLocalMoveX(2300, 1f).SetEase(Ease.InOutBack);
        titleBtn[2].DOLocalMoveY(-800, 1f).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(0.2f);
        yield return titleBtn[1].DOLocalMoveX(2300, 1f).SetEase(Ease.InOutBack)
        .OnComplete(() => isButton = false).WaitForCompletion();
    }
    public void ExitButton() //메인 화면에서 타이틀로 이동
    {
        if(isButton) return;
        StartCoroutine(exitButton());
        SoundManager.instance.SetAudio("UIClick", SoundManager.SoundState.SFX, false);
    }
    IEnumerator exitButton()
    {
        yield return StartCoroutine(mainMenu());
        StartCoroutine(titleAppearBtn());
    }
    IEnumerator titleAppearBtn()
    {
        InitPanel(0);
        titleBtn[0].localPosition = new Vector2(1600, -189);
        titleBtn[1].localPosition = new Vector2(1600, -415);
        titleBtn[2].localPosition = new Vector2(-353, -810);

        Selectbg[0].DOLocalMove(new Vector3(0, -540), 0.7f);
        yield return Selectbg[1].DOLocalMove(new Vector3(0, 540), 0.7f).WaitForCompletion();

        if (isButton) yield break;
        isButton = true;
        titleBtn[0].DOLocalMoveX(600, 1f).SetEase(Ease.InOutBack);
        titleBtn[2].DOLocalMoveY(-206, 1f).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(0.2f);
        yield return titleBtn[1].DOLocalMoveX(600, 1f).SetEase(Ease.InOutBack)
        .OnComplete(() => isButton = false).WaitForCompletion();
    }
    IEnumerator selectStart() //선택 시작 시 선택 탭 나타남
    {
        isButton = true;
        InitPanel(1);
        //graph.GetComponent<CanvasRenderer>().SetAlpha(1);
        graph.ResetRadar();
        // Selectbg[0].DOLocalMoveY(1, 0.7f);
        // yield return Selectbg[1].DOLocalMoveY(-1, 0.7f).WaitForCompletion();

        yield return new WaitForSeconds(0.1f);
        SelectUI[0].DOLocalMoveX(-850, 0.75f);
        SelectUI[2].DOLocalMoveY(-429, 0.75f);
        SelectUI[3].DOLocalMoveY(350, 0.75f);
        SelectUI[4].DOLocalMoveX(-750, 0.75f);
        var selectPanelRect = SelectUI[1].GetComponent<RectTransform>();
        float size = 70;
        selectPanelRect.sizeDelta = new Vector2(size, 1080);
        yield return DOTween.To(() => selectPanelRect.sizeDelta, x => selectPanelRect.sizeDelta = x, new Vector2(800, 1080), 0.75f).WaitForCompletion();
        SelectSkillImage.transform.position = ASkillList[0].position;
        isButton = false;
        graph.InitRaderGraph();
    }
    public void StageStart()
    {
        SoundManager.instance.SetAudio("UIClick", SoundManager.SoundState.SFX, false);
        SceneManager.instance.loadingpath = "Main";
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
    }
    IEnumerator mainMenu()
    {
        SelectUI[0].DOLocalMove(new Vector3(-1658, 530), 0.75f);
        SelectUI[2].DOLocalMove(new Vector3(-960, -570), 0.75f);
        SelectUI[3].DOLocalMove(new Vector3(-118, 740), 0.75f);
        SelectUI[4].DOLocalMove(new Vector3(-1450, 0), 0.75f);
        SelectSkillImage.rectTransform.anchoredPosition = new Vector2(-355, 0);
        var selectPanelRect = SelectUI[1].GetComponent<RectTransform>();
        DOTween.To(() => selectPanelRect.sizeDelta, x => selectPanelRect.sizeDelta = x, new Vector2(0, 1080), 0.75f).WaitForCompletion();
        graph.DisableRadar();
        yield return new WaitForSeconds(0.75f);
        graph.GetComponent<CanvasRenderer>().SetMesh(null);
    }
    #endregion

    #region ShopPlot

    public void ShopStart()
    {
        if(isButton) return;
        StartCoroutine(ShopAppearBtn());
    }
    IEnumerator ShopAppearBtn()
    {
        yield return StartCoroutine(mainMenu());
        InitPanel(2);
    }
    public void ShopExit()
    {
        StartCoroutine(ShopDisappearBtn());
    }
    IEnumerator ShopDisappearBtn()
    {
        InitPanel(1);

        yield return StartCoroutine(selectStart());
    }

    #endregion

    public void ASkillButtonAdd(int i)
    {
        var p = SceneManager.instance.playerData;
        var b = Instantiate(ASkillPrefab, ASkillParent).GetComponent<Button>();
        ASkillList[i] = b.transform;
        b.image.sprite = aSkillInfos[i].sprite;
        var num = i;
        b.onClick.AddListener(() =>
        {
            if (!isButton)
            {
                SoundManager.instance.SetAudio("XP", SoundManager.SoundState.SFX, false);

                SceneManager.instance.ActiveIndex = num;
                SceneManager.instance.ActiveLevel = 3;
                ASkillStatus[0].fillAmount = Mathf.InverseLerp(0, 10, aSkillInfos[num].dmg);
                ASkillStatus[1].fillAmount = Mathf.InverseLerp(0, 10, aSkillInfos[num].range);
                ASkillStatus[2].fillAmount = Mathf.InverseLerp(80, 10, aSkillInfos[num].coolTime);
                SelectSkillImage.transform.position = b.transform.position;
                ASkillExplain.text = aSkillInfos[num].explain;
            }
        });
    }
    void InitShopBtn()
    {
        for (int i = 0; i < StatusButton.Length; i++)
        {
            var num = i;
            StatusButton[num].onClick.AddListener(() =>
            {
                InitShopStatus(num);
            });
        }
    }
    void InitShopStatus(int index)
    {
        var s = SceneManager.instance;
        Name.text = $"{s.upgradeInfos[index].name} + {s.upgradeInfos[index].UpgradeValue}";
        Icon.sprite = s.upgradeInfos[index].Icon;
        Cost.text = $"{s.upgradeInfos[index].Cost * (s.playerData.StatusLevel[index] + 1)}";
        CurValue.text = $"현재 + {s.upgradeInfos[index].UpgradeValue * s.playerData.StatusLevel[index]}";
        SelectStatus = index;
    }
    public void UpgradeStat()
    {
        var s = SceneManager.instance;
        var money = s.playerData.PlayerMoney;
        var cost = s.upgradeInfos[SelectStatus].Cost * (s.playerData.StatusLevel[SelectStatus] + 1);
        if (money >= cost)
        {
            s.playerData.StatusLevel[SelectStatus]++;
            s.playerData.PlayerMoney -= cost;
            InitShopStatus(SelectStatus);
        }
        StatusLevel[SelectStatus].text = s.playerData.StatusLevel[SelectStatus].ToString();
        InitMoney();
    }
    public void ResetStat()
    {
        var s = SceneManager.instance;
        for(int i = 0; i < s.playerData.StatusLevel.Length; i++)
        {
            var returnMoney = 0;
            for(int j = 0; j < s.playerData.StatusLevel[i]; j++)
            {
                returnMoney += s.upgradeInfos[i].Cost * (j + 1);
            }
            //s.playerData.PlayerMoney += s.playerData.StatusLevel[i] * s.upgradeInfos[i].Cost;
            s.playerData.PlayerMoney += returnMoney;
            s.playerData.StatusLevel[i] = 0;
            StatusLevel[i].text = s.playerData.StatusLevel[i].ToString();
        }
        InitMoney();
        InitShopStatus(SelectStatus);
    }
    public void InitPanel(int index) //타이틀 패널 바꾸기
    {
        for (int i = 0; i < Panel.Length; i++)
        {
            Panel[i].SetActive(false);
        }
        Panel[index].SetActive(true);
        titleBtn[0].localPosition = new Vector3(600, -189);
        titleBtn[1].localPosition = new Vector3(600, -415);
        titleBtn[2].localPosition = new Vector3(-353, -206);

        SelectUI[0].localPosition = new Vector3(-1658, 470);
        SelectUI[1].GetComponent<RectTransform>().sizeDelta = new Vector3(0, 1080);
        SelectUI[2].localPosition = new Vector3(-960, -570);
        SelectUI[3].localPosition = new Vector3(-118, 740);
        SelectUI[4].localPosition = new Vector3(-1550, 0);

        CharImage.color = Color.clear;
        CharImage.transform.localScale = new Vector3(1, 1, 1);
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
