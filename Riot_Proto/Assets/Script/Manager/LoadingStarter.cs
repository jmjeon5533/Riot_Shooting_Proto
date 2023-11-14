using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingStarter : MonoBehaviour
{
    public static LoadingStarter instance {get; private set;}
    [SerializeField] Image loadingbar;
    [SerializeField] Text loadingText;
    int TipIndex;
    string[] Tip = {
        "플레이어가 피격되면 무기레벨이 감소합니다.",
        "궁극기 스킬을 사용하면 무적 상태에 진입합니다.",
        "적을 여럿 처리하면 무기 강화 아이템을 드롭합니다.",
    };
    private void Awake() {
        instance = this;
    }
    private void Start()
    {
        StartCoroutine(stageStart());
        Camera[] camera = { Camera.main };
        SceneManager.instance.SetResolution(camera);
        TipIndex = Random.Range(0,Tip.Length);
        loadingText.text = Tip[TipIndex];
    }
    public void NextTip()
    {
        TipIndex = TipIndex+1;
        if(TipIndex == Tip.Length) TipIndex = 0;
        loadingText.text = Tip[TipIndex];
    }
     IEnumerator stageStart()
    {
        var s = SceneManager.instance;
        var time = 1.5f;
        var curtime = 0f;
        while (curtime <= time)
        {
            curtime += Time.deltaTime;
            loadingbar.fillAmount = Mathf.Lerp(curtime, time, 0.01f);
            yield return null;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(s.loadingpath);
        if(s.loadingpath == "Title")
        {
            SceneManager.instance.Invoke(() => TitleManager.instance.InitPanel(0), Time.deltaTime);
        }
    }
}
