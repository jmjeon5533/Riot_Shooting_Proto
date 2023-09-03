using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    public Image XPBar;
    public Transform canvas;
    public Transform ClearTab;
    public Transform OverTab;
    public Image[] Heart;
    
    public Image FadeBg;
    bool isUseTab = false;
    [HideInInspector] public Image BG1, BG2;
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
        if(isUseTab) return;

        GameManager.instance.IsGame = false;
        isUseTab = true;
        ClearTab.DOLocalMoveY(0,1).SetEase(Ease.OutQuad).OnComplete(() => isUseTab = false);
    }
    public void UseOverTab()
    {
        if(isUseTab) return;

        GameManager.instance.IsGame = false;
        isUseTab = true;
        OverTab.DOLocalMoveY(0,1).SetEase(Ease.OutQuad).OnComplete(() => isUseTab = false);
    }
    public void MainMenu()
    {
        SceneManager.instance.MainMenu();
    }
    public void NextStage()
    {
        if(isUseTab) return;

        isUseTab = true;
        ClearTab.DOLocalMoveY(800,1).SetEase(Ease.OutQuad).OnComplete(() => isUseTab = false);

        StartCoroutine(NextStageCoroutine());
    }
    IEnumerator NextStageCoroutine()
    {
        int rand = SceneManager.instance.StageIndex;
        while(rand == SceneManager.instance.StageIndex)
        {
            rand = Random.Range(0,GameManager.instance.BGList.Count);
        }
        print($"stage {rand + 1}");
        yield return FadeBg.DOColor(new Color(0,0,0,1), 1).WaitForCompletion();
        print(1);
        SceneManager.instance.StageIndex = rand;
        GameManager.instance.InitBackGround(rand);
        yield return new WaitForSeconds(0.5f);
        print(2);
        yield return FadeBg.DOColor(new Color(0,0,0,0), 1).WaitForCompletion();
        print(3);
        SpawnManager.instance.SpawnCount = 0;
        GameManager.instance.IsGame = true;
        SpawnManager.instance.Spawn();
    }
}