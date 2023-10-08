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
}
public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    public Image XPBar;
    public Transform canvas;
    public Transform bgCanvas;
    public Transform DmgTextParant;
    public Transform ClearTab;
    public Transform OverTab;
    public Image[] Heart;
    public Text[] RateText;

    public Image FadeBg;
    public GameObject StagePrefab;
    public List<BG> BGList = new();
    bool isUseTab = false;
    List<GameObject> curBGObj = new();
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        XPBarUpdate();
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
        ClearTab.DOLocalMoveY(0, 1).SetEase(Ease.OutQuad).OnComplete(() => isUseTab = false);
    }
    public void UseOverTab()
    {
        if (isUseTab) return;

        GameManager.instance.IsGame = false;
        isUseTab = true;
        OverTab.DOLocalMoveY(0, 1).SetEase(Ease.OutQuad).OnComplete(() => isUseTab = false);
    }
    public void MainMenu()
    {
        SceneManager.instance.playerData.PlayerMora += GameManager.instance.GetMoney;
        SceneManager.instance.MainMenu();
    }
    public void InitRate()
    {
        for(int i = 0; i < RateText.Length; i++)
        {
            RateText[i].text = GameManager.instance.GetMoney.ToString();
        }
    }
    public void NextStage()
    {
        if (isUseTab) return;

        isUseTab = true;
        ClearTab.DOLocalMoveY(800, 1).SetEase(Ease.OutQuad).OnComplete(() => isUseTab = false);

        StartCoroutine(NextStageCoroutine());
    }
    public void InitBackGround(int BackNum)
    {
        for (int i = 0; i < curBGObj.Count; i++)
        {
            PoolManager.Instance.PoolObject("BG", curBGObj[i]);
        }
        for (int i = 0; i < BGList[BackNum].bgs.Count; i++)
        {
            float b = 0.8f;

            var BG1 = PoolManager.Instance.GetObject("BG",bgCanvas).GetComponent<Image>();
            var BG2 = PoolManager.Instance.GetObject("BG",bgCanvas).GetComponent<Image>();

            curBGObj.Add(BG1.gameObject);
            curBGObj.Add(BG2.gameObject);

            BG1.sprite = BGList[BackNum].bgs[i].sprite;
            var ratio = 1080 / BG1.sprite.rect.height;
            BG1.rectTransform.sizeDelta = new Vector2(BG1.sprite.rect.width, BG1.sprite.rect.height) * ratio;
            BG1.transform.localPosition = Vector3.zero;


            BG2.sprite = BGList[BackNum].bgs[i].sprite;
            BG2.rectTransform.sizeDelta = new Vector2(BG2.sprite.rect.width, BG2.sprite.rect.height) * ratio;
            BG2.transform.localPosition = new Vector3(BG2.GetComponent<RectTransform>().rect.width, 0, 0);

            var speed = BGList[BackNum].bgs[i].speed;
            BG1.GetComponent<Map>().MoveSpeed = speed;
            BG2.GetComponent<Map>().MoveSpeed = speed;

            BG1.color = new Color(b,b,b,1);
            BG2.color = new Color(b,b,b,1);
        }
    }
    IEnumerator NextStageCoroutine()
    {
        int rand = SceneManager.instance.StageIndex;
        while (rand == SceneManager.instance.StageIndex)
        {
            rand = Random.Range(0, BGList.Count);
        }
        print($"stage {rand + 1}");
        yield return FadeBg.DOColor(new Color(0, 0, 0, 1), 1).WaitForCompletion();
        print(1);
        SceneManager.instance.StageIndex = rand;
        InitBackGround(rand);
        yield return new WaitForSeconds(0.5f);
        print(2);
        yield return FadeBg.DOColor(new Color(0, 0, 0, 0), 1).WaitForCompletion();
        print(3);
        SpawnManager.instance.SpawnCount = 0;
        GameManager.instance.IsGame = true;
        SpawnManager.instance.Spawn();
    }
}