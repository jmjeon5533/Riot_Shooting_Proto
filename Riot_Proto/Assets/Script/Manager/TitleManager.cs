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
public class TitleManager : MonoBehaviour
{
    public static TitleManager instance { get; private set; }
    public GameObject[] Panel;
    [SerializeField] Transform[] Selectbg;
    [SerializeField] Camera[] Mcamera;
    [SerializeField] Transform[] titleBtn;
    bool isButton;
    [Space(10)]
    [Header("메인 탭")]
    [SerializeField] Transform[] MainUI;
    [SerializeField] Text MoneyText;
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
    bool canSelect = false;

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

        MoneyText.text = SceneManager.instance.playerData.PlayerMoney.ToString();
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
        titleBtn[0].DOLocalMoveX(600, 1.5f).SetEase(Ease.InOutBack);
        titleBtn[2].DOLocalMoveY(-206, 1.5f).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(0.2f);
        yield return titleBtn[1].DOLocalMoveX(600, 1.5f).SetEase(Ease.InOutBack)
        .OnComplete(() => isButton = false).WaitForCompletion();
    }

    public void StartButton() //타이틀에서 시작 버튼 누를 시 움직임
    {
        StartCoroutine(MainMenuStart());
        SoundManager.instance.SetAudio("UIClick", SoundManager.SoundState.SFX, false);
    }
    
    IEnumerator MainMenuStart()
    {
        yield return StartCoroutine(titleDisappearBtn());
        canSelect = false;
        InitPanel(1);
        Selectbg[0].DOLocalMoveY(1, 0.7f);
        yield return Selectbg[1].DOLocalMoveY(-1, 0.7f).WaitForCompletion();

        MainUI[0].DOLocalMoveX(910, 0.7f);
        MainUI[1].DOLocalMoveX(-910, 0.7f);
        MainUI[2].DOLocalMoveY(520, 0.7f);
        yield return MainUI[3].DOLocalMoveX(-910, 0.7f).WaitForCompletion();
        canSelect = true;
    }
    IEnumerator titleDisappearBtn()
    {
        if (isButton) yield break;
        isButton = true;
        titleBtn[0].DOLocalMoveX(2300, 1.5f).SetEase(Ease.InOutBack);
        titleBtn[2].DOLocalMoveY(-800, 1.5f).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(0.2f);
        yield return titleBtn[1].DOLocalMoveX(2300, 1.5f).SetEase(Ease.InOutBack)
        .OnComplete(() => isButton = false).WaitForCompletion();
    }
    public void ExitButton() //메인 화면에서 타이틀로 이동
    {
        StartCoroutine(TitleMove());
        SoundManager.instance.SetAudio("UIClick", SoundManager.SoundState.SFX, false);
    }
    IEnumerator TitleMove()
    {
        yield return StartCoroutine(MainMenuExit());
        yield return StartCoroutine(titleAppearBtn());
    }
    IEnumerator MainMenuExit()
    {
        canSelect = false;
        MainUI[0].DOLocalMoveX(1500, 0.7f);
        MainUI[1].DOLocalMoveX(-1500, 0.7f);
        MainUI[2].DOLocalMoveY(620, 0.7f);
        yield return MainUI[3].DOLocalMoveX(-1500, 0.7f).WaitForCompletion();
    }
    IEnumerator titleAppearBtn()
    {
        InitPanel(0);
        titleBtn[0].localPosition = new Vector2(1600, -189);
        titleBtn[1].localPosition = new Vector2(1600, -415);
        titleBtn[2].localPosition = new Vector2(-353, -810);

        Selectbg[0].DOLocalMove(new Vector3(0, -540), 1);
        yield return Selectbg[1].DOLocalMove(new Vector3(0, 540), 1).WaitForCompletion();

        if (isButton) yield break;
        isButton = true;
        titleBtn[0].DOLocalMoveX(600, 1.5f).SetEase(Ease.InOutBack);
        titleBtn[2].DOLocalMoveY(-206, 1.5f).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(0.2f);
        yield return titleBtn[1].DOLocalMoveX(600, 1.5f).SetEase(Ease.InOutBack)
        .OnComplete(() => isButton = false).WaitForCompletion();
    }
    public void SelectStart() //메인 화면에서 선택 화면으로 이동
    {
        StartCoroutine(selectStart());
    }
    IEnumerator selectStart() //선택 시작 시 선택 탭 나타남
    {
        canSelect = false;
        MainUI[0].DOLocalMoveX(1500, 0.7f);
        MainUI[1].DOLocalMoveX(-1500, 0.7f);
        MainUI[2].DOLocalMoveY(620, 0.7f);
        yield return MainUI[3].DOLocalMoveX(-1500, 0.7f).WaitForCompletion();

        InitPanel(2);
        //graph.GetComponent<CanvasRenderer>().SetAlpha(1);
        graph.ResetRadar();
        // Selectbg[0].DOLocalMoveY(1, 0.7f);
        // yield return Selectbg[1].DOLocalMoveY(-1, 0.7f).WaitForCompletion();

        yield return new WaitForSeconds(0.1f);
        SelectUI[0].DOLocalMoveX(-850, 1);
        SelectUI[2].DOLocalMoveY(-429, 1);
        SelectUI[3].DOLocalMoveY(350, 1);
        SelectUI[4].DOLocalMoveX(-750, 1);
        var selectPanelRect = SelectUI[1].GetComponent<RectTransform>();
        float size = 70;
        selectPanelRect.sizeDelta = new Vector2(size, 1080);
        yield return DOTween.To(() => selectPanelRect.sizeDelta, x => selectPanelRect.sizeDelta = x, new Vector2(800, 1080), 1).WaitForCompletion();
        SelectSkillImage.transform.position = ASkillList[0].position;
        canSelect = true;
        graph.InitRaderGraph();
    }
    public void StageStart()
    {
        SoundManager.instance.SetAudio("UIClick", SoundManager.SoundState.SFX, false);
        SceneManager.instance.loadingpath = "Main";
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
    }
    public void MainMenu() //선택화면에서 메인화면으로 이동
    {
        if (!canSelect) return;
        SoundManager.instance.SetAudio("UIClick", SoundManager.SoundState.SFX, false);
        StartCoroutine(mainMenu());
    }
    IEnumerator mainMenu()
    {
        SelectUI[0].DOLocalMove(new Vector3(-1658, 530), 1);
        SelectUI[2].DOLocalMove(new Vector3(-960, -570), 1);
        SelectUI[3].DOLocalMove(new Vector3(-118, 740), 1);
        SelectUI[4].DOLocalMove(new Vector3(-1450, 0), 1);
        SelectSkillImage.rectTransform.anchoredPosition = new Vector2(-355, 0);
        var selectPanelRect = SelectUI[1].GetComponent<RectTransform>();
        DOTween.To(() => selectPanelRect.sizeDelta, x => selectPanelRect.sizeDelta = x, new Vector2(0, 1080), 1).WaitForCompletion();
        graph.DisableRadar();
        yield return new WaitForSeconds(1f);
        graph.GetComponent<CanvasRenderer>().SetMesh(null);

        StartCoroutine(MainMenuStart());
    }
    #endregion

    #region ShopPlot

    public void ShopButton()
    {
        StartCoroutine(ShopAppearBtn());
    }
    IEnumerator ShopAppearBtn()
    {
        yield return StartCoroutine(MainMenuExit());

        InitPanel(3);

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
            if (canSelect)
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

        MainUI[0].localPosition = new Vector3(1500, -490);
        MainUI[1].localPosition = new Vector3(-1500, -490);
        MainUI[2].localPosition = new Vector3(-940, 620);
        MainUI[3].localPosition = new Vector3(-1500, -255);

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
