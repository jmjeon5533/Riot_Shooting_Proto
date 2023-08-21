using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance { get; private set; }
    public int CharIndex; //캐릭터 번호
    public int StageIndex; //스테이지 번호

    [Space(10)]
    [Header("Option")]
    public bool CtrlLock;
    [SerializeField] Transform OptionPanel;
    public Toggle CtrlToggle;
    bool OptionMove;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    public void StageStart()
    {
        CtrlLock = CtrlToggle.isOn;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
    public void Option(float y)
    {
        if (OptionMove) return;

        OptionMove = true;
        OptionPanel.DOLocalMoveY(y, 1f).SetEase(Ease.OutSine).OnComplete(() => OptionMove = false);
    }
}
