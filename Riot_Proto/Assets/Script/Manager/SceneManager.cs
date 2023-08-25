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
    public Vector2 ScreenArea;
    public Vector2 minusScreen;

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
    private void Start()
    {

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
    public void SetResolution(Camera[] camera)
    {
        ScreenArea = new Vector2(1920,1080);
        minusScreen = new Vector2(Screen.width, Screen.height);
        Screen.SetResolution((int)ScreenArea.x, (int)(((float)minusScreen.y / minusScreen.x) * ScreenArea.x), true); // SetResolution 함수 제대로 사용하기
        for (int i = 0; i < camera.Length; i++)
        {
            if ((float)ScreenArea.x / ScreenArea.y < (float)minusScreen.x / minusScreen.y) // 기기의 해상도 비가 더 큰 경우
            {
                float newWidth = ((float)ScreenArea.x / ScreenArea.y) / ((float)minusScreen.x / minusScreen.y); // 새로운 너비
                camera[i].rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
            }
            else // 게임의 해상도 비가 더 큰 경우
            {
                float newHeight = ((float)minusScreen.x / minusScreen.y) / ((float)ScreenArea.x / ScreenArea.y); // 새로운 높이
                camera[i].rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
            }
        }
    }
}
