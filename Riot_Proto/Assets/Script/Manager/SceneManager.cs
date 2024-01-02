using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.IO;

[System.Serializable]
public class PlayerData
{
    public int PlayerMoney;

    public int[] StatusLevel = new int[8];

    [Header("Option")]
    public float BGMVolume;
    public float SFXVolume;
    public bool DetailCtrl;
}
[System.Serializable]
public class Ability
{
    public int index;
    public int level = 1;
}

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance { get; private set; }
    public int CharIndex; //캐릭터 번호
    public int StageIndex; //스테이지 번호
    public int ActiveIndex = -1; //액티브 스킬 번호
    public int ActiveLevel; //액티브 스킬 레벨
    public Vector2 ScreenArea;
    public Vector2 ScreenWidth;
    public string loadingpath = "Main"; //로딩 후 넘어갈 씬
    [Header("Option")]
    public bool DetailCtrl = false;

    [Space(10)]
    public PlayerData playerData;
    public UpgradeInfo[] upgradeInfos;
    [SerializeField] Transform OptionPanel;
    bool OptionMove;

    public void initPanel(int index) => TitleManager.instance.InitPanel(index);
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        JsonLoad();
    }
    private void Start()
    {
        DetailCtrl = playerData.DetailCtrl;
    }
    public void JsonLoad()
    {
        string data = PlayerPrefs.GetString("savedata", "null");
        //print(data);
        playerData = data.Equals("null") || string.IsNullOrEmpty(data) ? new PlayerData() : JsonUtility.FromJson<PlayerData>(data);
    }
    public void JsonSave()
    {
        PlayerData saveData = new PlayerData();
        saveData.PlayerMoney = playerData.PlayerMoney;
        saveData.BGMVolume = playerData.BGMVolume;
        saveData.SFXVolume = playerData.SFXVolume;
        saveData.DetailCtrl = playerData.DetailCtrl;

        for(int i = 0; i < saveData.StatusLevel.Length; i++)
        {
            saveData.StatusLevel[i] = playerData.StatusLevel[i];
        }

        string data = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("savedata", data);
    }
    void OnApplicationQuit()
    {
        JsonSave();
    }
    public void StageStart()
    {
        loadingpath = "Main";
        UnityEngine.SceneManagement.SceneManager.LoadScene("loading");
    }
    public void MainMenu()
    {
        loadingpath = "Title";
        UnityEngine.SceneManagement.SceneManager.LoadScene("loading");
    }
    public void Option(float y)
    {
        if (OptionMove) return;

        OptionMove = true;
        OptionPanel.DOLocalMoveY(y, 1f).SetEase(Ease.OutSine).OnComplete(() => OptionMove = false);
    }
    public void SetResolution(Camera[] camera)
    {
        ScreenArea = new Vector2(1920, 1080);
        ScreenWidth = new Vector2(Screen.width, Screen.height);
        Screen.SetResolution((int)ScreenArea.x, (int)(((float)ScreenWidth.y / ScreenWidth.x) * ScreenArea.x), true); // SetResolution 함수 제대로 사용하기
        for (int i = 0; i < camera.Length; i++)
        {
            if ((float)ScreenArea.x / ScreenArea.y < (float)ScreenWidth.x / ScreenWidth.y) // 기기의 해상도 비가 더 큰 경우
            {
                float newWidth = ((float)ScreenArea.x / ScreenArea.y) / ((float)ScreenWidth.x / ScreenWidth.y); // 새로운 너비
                camera[i].rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
            }
            else // 게임의 해상도 비가 더 큰 경우
            {
                float newHeight = ((float)ScreenWidth.x / ScreenWidth.y) / ((float)ScreenArea.x / ScreenArea.y); // 새로운 높이
                camera[i].rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
            }
        }
        Application.targetFrameRate = 60;
    }
}
