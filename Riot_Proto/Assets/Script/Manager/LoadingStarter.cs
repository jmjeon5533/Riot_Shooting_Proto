using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingStarter : MonoBehaviour
{
    public static LoadingStarter instance {get; private set;}
    [SerializeField] Image loadingbar;
    private void Awake() {
        instance = this;
    }
    private void Start()
    {
        StartCoroutine(stageStart());
        Camera[] camera = { Camera.main };
        SceneManager.instance.SetResolution(camera);
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
