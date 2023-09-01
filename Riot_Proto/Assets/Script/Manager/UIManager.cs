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
    public Image FadeBg;
    bool isclearTab = false;
    [HideInInspector] public Image BG1, BG2; 
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        XPBarUpdate();
    }
    public void XPBarUpdate()
    {
        float xp = GameManager.instance.XP;
        float maxXp = GameManager.instance.MaxXP;
        XPBar.fillAmount = xp / maxXp;
    }
    public void UseClearTab()
    {
        if(isclearTab) return;

        GameManager.instance.IsGame = false;
        isclearTab = true;
        ClearTab.DOLocalMoveY(0,1).SetEase(Ease.OutQuad).OnComplete(() => isclearTab = false);
    }
    public void NextStage()
    {
        if(isclearTab) return;

        isclearTab = true;
        ClearTab.DOLocalMoveY(800,1).SetEase(Ease.OutQuad).OnComplete(() => isclearTab = false);

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
        GameManager.instance.IsGame = true;
        SpawnManager.instance.Spawn();
    }
}