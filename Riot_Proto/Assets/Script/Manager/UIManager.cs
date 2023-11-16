using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    public RectTransform XPRectTransform;
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
    public Image curPowerCount;
    public RectTransform powerPanel;

    public Image FadeBg;
    public GameObject StagePrefab;
    public List<BG> BGList = new();
    bool isUseTab = false;
    List<GameObject> curBGObj = new();
    [Space(10)]
    [Header("Item")]
    public int NextHPCount = 200000;

    public void StageStart() => SceneManager.instance.StageStart();

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        XPBarUpdate();
        Bossbar.SetActive(false);
        hpPanel.DOAnchorPosX(612, 0);
        abilityPanel.DOAnchorPosX(-300, 0);
        characterImage.rectTransform.DOAnchorPosX(-1435, 0);
        MainRateText.rectTransform.DOAnchorPosY(160, 0);
        powerPanel.DOAnchorPosX(-1200, 0);
        XPRectTransform.DOAnchorPosX(-1200, 0);
        activeSkill.DOMoveX(15, 0);
        BGMS.value = SoundManager.instance.BGMVolume;
        SFXS.value = SoundManager.instance.SFXVolume;
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
            RateText[i].text = GameManager.instance.GetMoney.ToString();
        }
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
        hpPanel.DOAnchorPosX(-20, 1).SetEase(Ease.OutExpo);
        characterImage.rectTransform.DOAnchorPosX(-830, 1).SetEase(Ease.OutExpo);
        MainRateText.rectTransform.DOAnchorPosY(0, 1).SetEase(Ease.OutExpo);
        powerPanel.DOAnchorPosX(-540, 1).SetEase(Ease.OutExpo);
        XPRectTransform.DOAnchorPosX(-540, 1).SetEase(Ease.OutExpo);
        abilityPanel.DOAnchorPosX(0, 1).SetEase(Ease.OutExpo);
        //abilityPanel.transform.DOMoveX(-10, 1f).SetEase(Ease.OutExpo);
        activeSkill.DOAnchorPosX(-200, 1f).SetEase(Ease.OutExpo);
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
        ClearTab.DOLocalMoveY(0, 1).SetEase(Ease.OutQuad).OnComplete(() => isUseTab = false);
    }
    public void UseOverTab()
    {
        if (isUseTab) return;

        GameManager.instance.IsGame = false;
        isUseTab = true;
        InitRate();
        OverTab.DOLocalMoveY(0, 1).SetEase(Ease.OutQuad).OnComplete(() => isUseTab = false);
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.instance.playerData.PlayerMora += GameManager.instance.GetMoney;
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
}