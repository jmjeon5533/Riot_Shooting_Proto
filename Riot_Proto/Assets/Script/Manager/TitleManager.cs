using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using System.IO;
using System.Linq;

public class TitleManager : MonoBehaviour
{
    public static TitleManager instance { get; private set; }
    public List<GameObject> Panel = new();

    [SerializeField] Camera[] Mcamera;
    [Space(10)]
    [SerializeField] RawImage CharImage;
    [SerializeField] Image loadingbar;
    [SerializeField] Transform[] titleBtn;
    bool isButton;
    [SerializeField] Transform[] SelectUI;
    [SerializeField] Transform[] Selectbg;
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
    }
    public void StartButton()
    {
        StartCoroutine(Startbtn());
    }
    IEnumerator Startbtn()
    {
        if(isButton) yield break;
        isButton = true;
        titleBtn[0].DOLocalMoveX(2300,1.5f).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(0.2f);
        yield return titleBtn[1].DOLocalMoveX(2300,1.5f).SetEase(Ease.InOutBack)
        .OnComplete(()=> isButton = false).WaitForCompletion();
        SelectStart();
    }
    public void SelectStart()
    {
        StartCoroutine(selectStart());
    }
    IEnumerator selectStart()
    {
        InitPanel(1);
        Selectbg[0].DOLocalMoveY(0,0.7f);
        yield return Selectbg[1].DOLocalMoveY(0,0.7f).WaitForCompletion();

        yield return new WaitForSeconds(0.1f);
        SelectUI[0].DOLocalMoveX(-930,1);
        SelectUI[1].DOLocalMoveX(930,1);
        SelectUI[2].DOLocalMoveY(-520,1);
        SelectUI[3].DOLocalMoveY(-520,1);
        yield return null;
    }
    public void StageStart()
    {
        StartCoroutine(stageStart());
    }
    IEnumerator stageStart()
    {
        InitPanel(2);
        var time = 1.5f;
        var curtime = 0f;
        while (curtime <= time)
        {
            curtime += Time.deltaTime;
            loadingbar.fillAmount = Mathf.Lerp(curtime, time, 0.01f);
            yield return null;
        }
        SceneManager.instance.StageStart();
    }
    public void MainMenu()
    {
        StartCoroutine(mainMenu());
    }
    IEnumerator mainMenu()
    {
        SelectUI[0].DOLocalMove(new Vector3(-1658,520),1);
        SelectUI[1].DOLocalMove(new Vector3(2200,510),1);
        SelectUI[2].DOLocalMove(new Vector3(-930,-760),1);
        yield return SelectUI[3].DOLocalMove(new Vector3(930,-760),1).WaitForCompletion();
        yield return new WaitForSeconds(0.1f);

        Selectbg[0].DOLocalMove(new Vector3(0,-540),1);
        Selectbg[1].DOLocalMove(new Vector3(0,540),1);
        
        InitPanel(0);
        titleBtn[0].localPosition = new Vector2(2300,-189);
        titleBtn[1].localPosition = new Vector2(2300,-415);

        if(isButton) yield break;
        isButton = true;
        titleBtn[0].DOLocalMoveX(1600,1.5f).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(0.2f);
        yield return titleBtn[1].DOLocalMoveX(1500,1.5f).SetEase(Ease.InOutBack)
        .OnComplete(()=> isButton = false).WaitForCompletion();
    }
    public void ASkillButtonAdd(int i)
    {
        var p = SceneManager.instance.playerData;
        var b = Instantiate(ASkillPrefab, ASkillParent).GetComponent<Button>();
        b.image.sprite = ASkillSprite[i];
        Color[] colors = { Color.yellow, new Color(1, 0.5f, 0, 1), Color.red };
        b.transform.GetChild(0).GetComponent<Image>().color = colors[2];
        var num = i;
        b.onClick.AddListener(() =>
        {
            SceneManager.instance.ActiveIndex = num;
            SceneManager.instance.ActiveLevel = 3;
            ASkillImage.sprite = ASkillSprite[num];
            ASkillBorder.color = colors[2];
            ASkillNameText.text = ASkillName[num];
            ASkillCoolTimeText.text = ASkillCoolTime[num].ToString() + "s";
            ASkillExplainText.text = ASkillExplain[num];
        });
    }
    public void InitPanel(int index) //타이틀 패널 바꾸기
    {
        for (int i = 0; i < Panel.Count; i++)
        {
            Panel[i].SetActive(false);
        }
        Panel[index].SetActive(true);
        titleBtn[0].localPosition = new Vector3(1600,-189);
        titleBtn[1].localPosition = new Vector3(1500,-415);

        SelectUI[0].localPosition = new Vector3(-1658,520);
        SelectUI[1].localPosition = new Vector3(2200,510);
        SelectUI[2].localPosition = new Vector3(-930,-760);
        SelectUI[3].localPosition = new Vector3(930,-760);

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
