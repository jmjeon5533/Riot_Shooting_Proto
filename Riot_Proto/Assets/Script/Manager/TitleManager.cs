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

    [SerializeField] Camera[] Mcamera;
    [Space(10)]
    [SerializeField] RawImage CharImage;
    [SerializeField] Transform[] titleBtn;
    bool isButton;
    [SerializeField] Transform[] SelectUI;
    [SerializeField] Transform[] Selectbg;
    [Space(10)]
    [Header("ActiveSkill")]
    public ASkillInfo[] aSkillInfos;
    [Space(10)]
    [SerializeField] Transform[] ASkillList = new Transform[3];
    [SerializeField] Transform ASkillParent;
    [SerializeField] GameObject ASkillPrefab;
    [SerializeField] Image[] ASkillStatus;
    [SerializeField] RawImage SelectSkillImage;
    bool canSelect = false; //

    [SerializeField] Image startPanel;
    [SerializeField] Image logo;
    [SerializeField] MeshFilter meshFilter;

    [SerializeField] RadarGraph graph;
    Mesh statusPentagon;

    public Transform OptionTab;
    public bool isOption = false;
    Tween OptionTween;
    public Slider BGMS, SFXS;

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

        SceneManager.instance.ActiveIndex = 0;
        SceneManager.instance.ActiveLevel = 3;
        statusPentagon = new Mesh();
        meshFilter.mesh = statusPentagon;
        //pentagonInit();
        ASkillStatus[0].fillAmount = Mathf.InverseLerp(0, 10, aSkillInfos[0].dmg);
        ASkillStatus[1].fillAmount = Mathf.InverseLerp(0, 10, aSkillInfos[0].range);
        ASkillStatus[2].fillAmount = Mathf.InverseLerp(0, 130, aSkillInfos[0].coolTime);

        BGMS.value = SoundManager.instance.BGMVolume;
        SFXS.value = SoundManager.instance.SFXVolume;

        SoundManager.instance.SetAudio("Title1",SoundManager.SoundState.BGM,true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Option();
        }
        SoundManager.instance.BGMVolume = BGMS.value;
        SoundManager.instance.SFXVolume = SFXS.value;
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

    IEnumerator StartMotion()
    {
        startPanel.DOFade(1, 0);
        yield return new WaitForSeconds(1);
        yield return logo.DOFade(1, 1f).WaitForCompletion();
        startPanel.DOFade(0, 1.5f);
        yield return new WaitForSeconds(1);
        logo.DOFade(0, 1f);


        if (isButton) yield break;
        isButton = true;
        titleBtn[0].DOLocalMoveX(1600, 1.5f).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(0.2f);
        yield return titleBtn[1].DOLocalMoveX(1500, 1.5f).SetEase(Ease.InOutBack)
        .OnComplete(() => isButton = false).WaitForCompletion();
    }

    public void StartButton()
    {
        StartCoroutine(Startbtn());
    }
    IEnumerator Startbtn()
    {
        if (isButton) yield break;
        isButton = true;
        titleBtn[0].DOLocalMoveX(2300, 1.5f).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(0.2f);
        yield return titleBtn[1].DOLocalMoveX(2300, 1.5f).SetEase(Ease.InOutBack)
        .OnComplete(() => isButton = false).WaitForCompletion();
        SelectStart();
    }
    public void SelectStart()
    {
        StartCoroutine(selectStart());
    }
    IEnumerator selectStart()
    {
        canSelect = false;
        InitPanel(1);
        graph.ResetRadar();
        Selectbg[0].DOLocalMoveY(0, 0.7f);
        yield return Selectbg[1].DOLocalMoveY(0, 0.7f).WaitForCompletion();

        yield return new WaitForSeconds(0.1f);
        SelectUI[0].DOLocalMoveX(-930, 1);
        SelectUI[2].DOLocalMoveY(355, 1);
        SelectUI[3].DOLocalMoveY(-520, 1);
        SelectUI[4].DOLocalMoveX(-500, 1);
        var selectPanelRect = SelectUI[1].GetComponent<RectTransform>();
        float size = 70;
        selectPanelRect.sizeDelta = new Vector2(size, 0);
        yield return DOTween.To(() => selectPanelRect.sizeDelta, x => selectPanelRect.sizeDelta = x, new Vector2(1000, 0), 1).WaitForCompletion();
        SelectSkillImage.transform.position = ASkillList[0].position;
        canSelect = true;
        graph.InitRaderGraph();
    }
    public void StageStart()
    {
        SceneManager.instance.loadingpath = "Main";
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
    }
    public void MainMenu()
    {
        StartCoroutine(mainMenu());
    }
    IEnumerator mainMenu()
    {
        SelectUI[0].DOLocalMove(new Vector3(-1658, 520), 1);
        SelectUI[2].DOLocalMove(new Vector3(-930, -760), 1);
        SelectUI[4].DOLocalMove(new Vector3(-1450, -383), 1);
        SelectSkillImage.rectTransform.anchoredPosition = new Vector2(-355, 0);
        var selectPanelRect = SelectUI[1].GetComponent<RectTransform>();
        DOTween.To(() => selectPanelRect.sizeDelta, x => selectPanelRect.sizeDelta = x, new Vector2(0, 0), 1);
        graph.DisableRadar();
        yield return SelectUI[3].DOLocalMove(new Vector3(930, -760), 1).WaitForCompletion();

        yield return new WaitForSeconds(0.1f);

        Selectbg[0].DOLocalMove(new Vector3(0, -540), 1);
        Selectbg[1].DOLocalMove(new Vector3(0, 540), 1);

        InitPanel(0);
        titleBtn[0].localPosition = new Vector2(2300, -189);
        titleBtn[1].localPosition = new Vector2(2300, -415);

        if (isButton) yield break;
        isButton = true;
        titleBtn[0].DOLocalMoveX(1600, 1.5f).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(0.2f);
        yield return titleBtn[1].DOLocalMoveX(1500, 1.5f).SetEase(Ease.InOutBack)
        .OnComplete(() => isButton = false).WaitForCompletion();
    }
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
                SceneManager.instance.ActiveIndex = num;
                SceneManager.instance.ActiveLevel = 3;
                ASkillStatus[0].fillAmount = Mathf.InverseLerp(0, 10, aSkillInfos[num].dmg);
                ASkillStatus[1].fillAmount = Mathf.InverseLerp(0, 10, aSkillInfos[num].range);
                ASkillStatus[2].fillAmount = Mathf.InverseLerp(0, 100, aSkillInfos[num].coolTime);
                SelectSkillImage.transform.position = b.transform.position;
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
        titleBtn[0].localPosition = new Vector3(1600, -189);
        titleBtn[1].localPosition = new Vector3(1500, -415);

        SelectUI[0].localPosition = new Vector3(-1658, 520);
        SelectUI[1].GetComponent<RectTransform>().sizeDelta = new Vector3(0, 0);
        SelectUI[2].localPosition = new Vector3(-930, 570);
        SelectUI[3].localPosition = new Vector3(930, -800);
        SelectUI[4].localPosition = new Vector3(-1450, -383);

        // Selectbg[0].localPosition = new Vector3(0,-540);
        // Selectbg[1].localPosition = new Vector3(0,540);

        CharImage.color = Color.clear;
        CharImage.transform.localScale = new Vector3(1, 1, 1);
    }
    public void GetMora()
    {
        SceneManager.instance.playerData.PlayerMora += 1000;
        InitPanel(2);
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
