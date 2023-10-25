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
    }
     IEnumerator stageStart()
    {
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
}
