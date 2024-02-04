using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;
using GoogleMobileAds.Api;
using Random = UnityEngine.Random;

[System.Serializable]
public class BG
{
    [System.Serializable]
    public class BGGroup
    {
        public Sprite sprite;
        public float speed;
    }
    public List<BGGroup> bgs = new();
    public List<BGGroup> BossBgs = new();
}
public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    public Image XPBar;
    public Transform canvas;
    public Transform bgCanvas;
    public Transform bgFolder;
    public Transform DmgTextParant;
    public Transform ClearTab;
    public Transform OverTab;
    public Transform OptionTab;
    public bool isOption = false;
    Tween OptionTween;
    public Slider BGMS,SFXS;

    public GameObject Bossbar;
    public Image BossbarImage;
    public Image characterImage;

    public RectTransform[] iconPosList;

    public RectTransform hpPanel;
    public RectTransform abilityPanel;
    public RectTransform activeSkill;

    public Image[] Heart;
    public Text MainRateText;
    public int Ratevalue;
    public Text[] RateText;
    public RectTransform OptionButton;
    public Toggle DetailCtrlToggle;

    public Image FadeBg;
    public GameObject StagePrefab;
    public List<BG> BGList = new();
    bool isUseTab = false;
    bool isWatchAD = false;
    List<GameObject> curBGObj = new();
    [Space(10)]
    [Header("Item")]
    public int NextHPCount = 200000;

    [Header("Result UI")]
    [SerializeField] Image ResultPanel;

    [SerializeField] GameObject mainText;
    [SerializeField] GameObject bar;

    [SerializeField] TextMeshProUGUI totalScoreText;
    [SerializeField] TextMeshProUGUI waveCountText;
    [SerializeField] TextMeshProUGUI enemyKilledText;
    [SerializeField] TextMeshProUGUI expEarnText;
    [SerializeField] TextMeshProUGUI clearBonusText;

    [SerializeField] Image rankImg;
    [SerializeField] TextMeshProUGUI rankText;

    [SerializeField] Button gotoMain;
    [SerializeField] Button ADButton;

    [SerializeField] GameObject earnMoneyUI;
    [SerializeField] Text earnMoneyText;

    int totalScore = 0;


    public void StageStart() => SceneManager.instance.StageStart();

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        XPBarUpdate();
        Bossbar.SetActive(false);
        hpPanel.DOAnchorPosX(-390, 0);
        abilityPanel.DOAnchorPosX(-300, 0);
        characterImage.rectTransform.DOAnchorPosX(-1435, 0);
        MainRateText.rectTransform.DOAnchorPosY(160, -15f);
        activeSkill.DOMoveX(15, 0);
        OptionButton.DOAnchorPosX(100,0);
        BGMS.value = SoundManager.instance.BGMVolume;
        SFXS.value = SoundManager.instance.SFXVolume;
        DetailCtrlToggle.isOn = SceneManager.instance.DetailCtrl;
        isWatchAD = false;

        ADButton.onClick.AddListener(DoubleReward);
        CloseResult();
    }
    private void Update()
    {
        var g = GameManager.instance;
        if (Ratevalue <= g.GetMoney)
        {
            var value = 1 + Mathf.Clamp(g.GetMoney - Ratevalue, 0, 1000);
            Ratevalue = (int)Mathf.MoveTowards(Ratevalue, g.GetMoney, value);
        }
        MainRateText.text = (Ratevalue < 1000) ? string.Format("{0:D4}", Ratevalue) : Ratevalue.ToString();
        for (int i = 0; i < RateText.Length; i++)
        {
            RateText[i].text = GameManager.instance.GetMoney.ToString() + "점\n" + (g.GetMoney >= 500000 ? "잘하시는데요?" : "좀 더 노력해봐요...");
        }
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
        Time.timeScale = isOption ? 0 : 1;
        if(OptionTween != null) OptionTween.Kill();
        OptionTween = OptionTab.DOLocalMoveY(isOption ? 0 : 800,0.5f).SetUpdate(true);
    }
    public void InitHeart()
    {
        for (int i = 0; i < Heart.Length; i++)
        {
            Heart[i].enabled = false;
        }
        for (int i = 0; i < GameManager.instance.player.HP; i++)
        {
            Heart[i].enabled = true;

        }
    }

    public void ShowImg()
    {
        hpPanel.DOAnchorPosX(194, 1).SetEase(Ease.OutExpo);
        characterImage.rectTransform.DOAnchorPosX(-860, 1).SetEase(Ease.OutExpo);
        MainRateText.rectTransform.DOAnchorPosY(-15f, 1).SetEase(Ease.OutExpo);
        abilityPanel.DOAnchorPosX(0, 1).SetEase(Ease.OutExpo);
        //abilityPanel.transform.DOMoveX(-10, 1f).SetEase(Ease.OutExpo);
        activeSkill.DOAnchorPosX(-200, 1f).SetEase(Ease.OutExpo);
        OptionButton.DOAnchorPosX(-25,1).SetEase(Ease.OutExpo);
        //StartCoroutine(ShowUI());
    }

    

    public void XPBarUpdate()
    {
        float xp = GameManager.instance.XP;
        float maxXp = GameManager.instance.MaxXP;
        XPBar.fillAmount = xp / maxXp;
    }
    public void UseClearTab()
    {
        if (isUseTab) return;

        GameManager.instance.IsGame = false;
        isUseTab = true;
        InitRate();
        ShowResult(false);
        //ClearTab.DOLocalMoveY(0, 1).SetEase(Ease.OutQuad).OnComplete(() => isUseTab = false);
    }
    public void UseOverTab()
    {
        if (isUseTab) return;

        GameManager.instance.IsGame = false;
        isUseTab = true;
        InitRate();
        ShowResult(false);
        //OverTab.DOLocalMoveY(0, 1).SetEase(Ease.OutQuad).OnComplete(() => isUseTab = false);
    }

    void ShowResult(bool isClear)
    {
        StartCoroutine(CalulateResult(isClear));
        
    }

    void CloseResult()
    {
        mainText.gameObject.SetActive(false);
        totalScoreText.gameObject.SetActive(false);
        bar.SetActive(false);
        waveCountText.gameObject.SetActive(false);
        enemyKilledText.gameObject.SetActive(false);
        expEarnText.gameObject.SetActive(false);
        clearBonusText.gameObject.SetActive(false);
        rankImg.gameObject.SetActive(false);
        rankText.gameObject.SetActive(false);
        gotoMain.gameObject.SetActive(false);
        ResultPanel.gameObject.SetActive(false);
        ADButton.gameObject.SetActive(false);
        earnMoneyUI.SetActive(false);
    }

    void SetText(string text, TextMeshProUGUI textObj)
    {
        if(!textObj.gameObject.activeSelf) textObj.gameObject.SetActive(true);
        textObj.text = text;
    }

    IEnumerator CalulateResult(bool isClear)
    {
        var showTextDelay = 1f;
        var calculateDelay = 1.5f;

        ResultPanel.gameObject.SetActive(true);

        ResultPanel.DOFade(0, 0);
        yield return ResultPanel.DOFade(0.75f,1).WaitForCompletion();

        mainText.gameObject.SetActive(true);
        yield return StartCoroutine(Delay(showTextDelay));
        bar.SetActive(true);
        SetText($"총합 점수 : ", totalScoreText);
        yield return StartCoroutine(Delay(calculateDelay));
        
        SetText($"총합 점수 : {0}", totalScoreText);

        yield return StartCoroutine(Delay(showTextDelay));
        SetText($"획득 점수 : ", waveCountText);
        yield return StartCoroutine(Delay(calculateDelay));
        yield return StartCoroutine(CalculatingScore(0, Ratevalue, waveCountText, 2f));

        yield return StartCoroutine(Delay(showTextDelay));
        SetText($"처치한 적 : ", enemyKilledText);
        yield return StartCoroutine(Delay(calculateDelay));
        yield return StartCoroutine(CalculatingScore(0, GameManager.instance.GetKilledEnemyCount(), enemyKilledText, 2f, 1000));

        yield return StartCoroutine(Delay(showTextDelay));
        SetText($"얻은 경험치 : ", expEarnText);
        yield return StartCoroutine(Delay(calculateDelay));
        yield return StartCoroutine(CalculatingScore(0, GameManager.instance.GetEarnedXP(), expEarnText, 2f,100));

        if(isClear)
        {
            yield return StartCoroutine(Delay(showTextDelay));
            SetText($"클리어 보너스 : ", clearBonusText);
            yield return StartCoroutine(Delay(calculateDelay));
            yield return StartCoroutine(CalculatingScore(0, GameManager.instance.clearBonus, clearBonusText, 2f));
        }
        yield return StartCoroutine(Delay(calculateDelay));
        rankImg.gameObject.SetActive(true);
        rankText.gameObject.SetActive(true);
        rankText.text = CalCulateRank();
        earnMoneyUI.SetActive(true);
        var a = totalScore / 100;
        var b = 1 + GameManager.instance.CalculateAddValue(5);
        earnMoneyText.text = $"{Mathf.RoundToInt(a * b) * (isWatchAD ? 2 : 1)}";
        yield return StartCoroutine(Delay(calculateDelay));
        gotoMain.gameObject.SetActive(true);
        ADButton.gameObject.SetActive(true);
    }

    string CalCulateRank()
    {
        string rank = "Error";
        if(totalScore <= 200000)
        {
            rank = "E";
        }
        else if (totalScore <= 400000)
        {
            rank = "D";
        }
        else if (totalScore <= 600000)
        {
            rank = "C";
        }
        else if (totalScore <= 800000)
        {
            rank = "B";
        }
        else if (totalScore <= 1000000)
        {
            rank = "A";
        } 
        else if(totalScore <= 1499999)
        {
            rank = "S";
        } 
        else if(totalScore >= 1500000)
        {
            rank  = "SS";
        }
        return rank;
    }

    IEnumerator Delay(float time, Action act = null) 
    {
        float elapsedTime = 0;
        while(elapsedTime <= time)
        {
            if(IsSkipped())
            {
                if(act != null)
                    act();
                yield break;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    void CalculateScore(int value, int newValue, TextMeshProUGUI text, float time, float multiply = 1)
    {
        string explain = text.text.Split(':')[0];
        value = newValue;
        totalScore += (int)(newValue * multiply);
        text.text = explain + $": {value}";
        totalScoreText.text = $"총합 점수 : {totalScore}";
    }

    IEnumerator CalculatingScore(int value ,int newValue,TextMeshProUGUI text, float time, float multiply = 1)
    {
        float elapsedTime = 0;
        string explain = text.text.Split(':')[0];
        int prevValue = totalScore;
        int curValue = 0;
        while (elapsedTime <= time)
        {
            if(IsSkipped())
            {
                totalScore = prevValue;
                CalculateScore(value, newValue, text, time, multiply);
                yield break;
            }
            elapsedTime += Time.deltaTime;
            value = Mathf.CeilToInt(Mathf.Lerp(value, newValue, (elapsedTime / time)));
            text.text = explain + $": {value}";
            curValue = prevValue + (int)(value * multiply);
            totalScoreText.text = $"총합 점수 : {curValue}";
            yield return null;
        }
        totalScore = curValue;
        value = newValue;
        text.text = explain + $": {value}";
        totalScoreText.text = $"총합 점수 : {totalScore}";
    }

    bool IsSkipped()
    {

        //Mobile
        if (Input.touchCount > 0)
        {
            for(int i = 0; i < Input.touchCount; i++)
            {
                var tempTouch = Input.GetTouch(i);
                if (tempTouch.phase == TouchPhase.Began)
                {
                    return true;
                }
            }
        }

        //PC
        if(Input.GetMouseButtonDown(0))
        {
            return true;
        }
        return false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        CloseResult();
        var a = totalScore / 100;
        var b = 1 + GameManager.instance.CalculateAddValue(5);
        SceneManager.instance.playerData.PlayerMoney += Mathf.RoundToInt(a * b) * (isWatchAD ? 2 : 1);
        SceneManager.instance.MainMenu();
    }
    public void InitRate()
    {
        if (GameManager.instance.GetMoney >= NextHPCount)
        {
            var g = GameManager.instance;
            var pos = new Vector3(15, Random.Range(-g.MoveRange.y + g.MovePivot.y, g.MoveRange.y + g.MovePivot.y), 0);
            PoolManager.Instance.GetObject("HP", pos, Quaternion.identity);
            NextHPCount += 300000;
        }
    }
    public void NextStage()
    {
        if (isUseTab) return;
        CloseResult();

        isUseTab = true;
        ClearTab.DOLocalMoveY(800, 1).SetEase(Ease.OutQuad).OnComplete(() => isUseTab = false);

        StartCoroutine(NextStageCoroutine(false));
    }
    public void InitBackGround(int BackNum, bool isBoss)
    {
        for (int i = 0; i < curBGObj.Count; i++)
        {
            PoolManager.Instance.PoolObject("BG", curBGObj[i]);
        }
        var loopbg = isBoss ? BGList[BackNum].BossBgs : BGList[BackNum].bgs;
        for (int i = 0; i < loopbg.Count; i++)
        {
            float b = 0.8f;
            var bg = isBoss ? BGList[BackNum].BossBgs[i] : BGList[BackNum].bgs[i];

            var BG1 = PoolManager.Instance.GetObject("BG", bgFolder).GetComponent<Image>();
            var BG2 = PoolManager.Instance.GetObject("BG", bgFolder).GetComponent<Image>();

            curBGObj.Add(BG1.gameObject);
            curBGObj.Add(BG2.gameObject);

            BG1.sprite = bg.sprite;
            var ratio = 1080 / BG1.sprite.rect.height;
            BG1.rectTransform.sizeDelta = new Vector2(BG1.sprite.rect.width, BG1.sprite.rect.height) * ratio;
            BG1.transform.localPosition = Vector3.zero;


            BG2.sprite = bg.sprite;
            BG2.rectTransform.sizeDelta = new Vector2(BG2.sprite.rect.width, BG2.sprite.rect.height) * ratio;
            BG2.transform.localPosition = new Vector3(BG2.GetComponent<RectTransform>().rect.width, 0, 0);

            var speed = bg.speed;
            BG1.GetComponent<Map>().MoveSpeed = speed;
            BG2.GetComponent<Map>().MoveSpeed = speed;

            BG1.color = new Color(b, b, b, 1);
            BG2.color = new Color(b, b, b, 1);
        }
    }
    public IEnumerator NextStageCoroutine(bool isBoss, int index = -99)
    {
        int rand = SceneManager.instance.StageIndex;
        if (!isBoss)
        {
            while (rand == SceneManager.instance.StageIndex)
            {
                rand = Random.Range(0, BGList.Count);
            }
            print($"stage {rand + 1}");
        }
        yield return FadeBg.DOColor(new Color(0, 0, 0, 1), 0.75f).WaitForCompletion();
        print(1);
        if (!isBoss) SceneManager.instance.StageIndex = rand;
        InitBackGround(isBoss ? index : rand, isBoss);
        yield return new WaitForSeconds(0.05f);
        print(2);
        yield return FadeBg.DOColor(new Color(0, 0, 0, 0), 0.75f).WaitForCompletion();
        print(3);
        if (!isBoss)
        {
            SpawnManager.instance.SpawnCount = 0;
            GameManager.instance.IsGame = true;
            SpawnManager.instance.Spawn();
        }
    }
    public void DoubleReward()
    {
        SceneManager.instance.ShowAds(GetReward);
    }
        //보상 함수
    public void GetReward(Reward reward)
    {
        isWatchAD = true;
        earnMoneyUI.SetActive(true);
        var a = totalScore / 100;
        var b = 1 + GameManager.instance.CalculateAddValue(5);
        earnMoneyText.text = $"{Mathf.RoundToInt(a * b) * (isWatchAD ? 2 : 1)}";
        ADButton.gameObject.SetActive(false);
        SceneManager.instance.InitAds();
    }
}