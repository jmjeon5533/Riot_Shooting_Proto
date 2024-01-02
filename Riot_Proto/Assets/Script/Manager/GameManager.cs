using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] Camera MainCamera, UICamera, EffectCamera,BackCamera;
    [Space(10)]
    public Player player;
    public Joystick joystick;
    public Vector2 MoveRange;
    public Vector2 MovePivot;
    public float EnemyPower = 1;
    [Space(10)]
    public GameObject XPPrefab;
    public int MaxXP;
    public int AddMaxXP;
    public int XP;
    public int Level;
    public int SelectChance;
    public int GetMoney = 0;
    [Space(10)]
    public List<GameObject> playerPrefab = new();
    public List<GameObject> ItemList = new();
    public List<GameObject> curEnemys = new();

    public GameObject Bomb;

    public Shader dissolveShader;
    public Texture2D dissolveSprite;

    public GameObject curBGM;

    public bool IsGame = false;

    public Coroutine FadeCoroutine;

    [SerializeField] public float itemCoolCount;
    [SerializeField] public readonly int clearBonus = 150000;

    private int earnedXp = 0;
    private int killedEnemyCount = 0;

    
    void AddExpValue(int value)
    {
        earnedXp += value;
    }

    public void AddKillCount()
    {
        killedEnemyCount++;
    }

    public int GetKilledEnemyCount()
    {
        return killedEnemyCount;
    }

    public int GetEarnedXP()
    {
        return earnedXp;    
    }
    
    void Awake()
    {
        instance = this;
        Camera[] camera = { MainCamera, EffectCamera, UICamera, BackCamera};
        SceneManager.instance.SetResolution(camera);
    }
    void Start()
    {
        InitPlayer();
        AbilityBase.SetSubtractCool(0);
        UIManager.instance.InitBackGround(SceneManager.instance.StageIndex,false);
        UIManager.instance.FadeBg.transform.SetAsLastSibling();
        InitBGM("Stage1");
    }
    void InitPlayer()
    {
        var info = SceneManager.instance.upgradeInfos;
        var player = Instantiate(playerPrefab[SceneManager.instance.CharIndex], new Vector3(-12f, 0, 0), Quaternion.identity).GetComponent<Player>();
        player.damage += (int)CalculateAddValue(0);
        player.AttackCooltime -= CalculateAddValue(1);
        player.MoveSpeed += CalculateAddValue(2);
        player.CritRate += (int)CalculateAddValue(3);
        player.CritDamage += (int)CalculateAddValue(4);
        player.bulletLevel += (int)CalculateAddValue(7);
    }
    public float CalculateAddValue(int index)
    {
        var info = SceneManager.instance;
        return info.upgradeInfos[index].UpgradeValue * info.playerData.StatusLevel[index];   
    }
    public void InitBGM(string BGMPath)
    {
        if(curBGM != null) Destroy(curBGM);
        curBGM = SoundManager.instance.SetAudio(BGMPath,SoundManager.SoundState.BGM,true);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(MovePivot, MoveRange * 2);
    }
    public void AddXP(int Value)
    {
        if(!IsGame) return;
        var ab = AbilityCard.Instance;
        XP += Value;
        AddExpValue(Value);
        while(XP >= MaxXP)
        {
            SelectChance++;
            XP -= MaxXP;
            MaxXP += AddMaxXP;
            Level++;
        }
        if (SelectChance >= 1 && !ab.isSelect)
        {
            if (!ab.IsAbilityLimit()) ab.Select();
            FadeCoroutine ??= StartCoroutine(FadeTime(0));
        }
        UIManager.instance.XPBarUpdate();
    }

    public bool IsLevelDupe()
    {
        if (SelectChance >= 1) return true;
        else return false;
    }

    public void SetCameraShake(float time, float power)
    {
        StartCoroutine(CameraShake(time, power));
    }

    IEnumerator CameraShake(float time, float power)
    {
        float curTime = 0;
        Vector3 originPos = Camera.main.transform.position;
        var cam = Camera.main;
        while (curTime < time)
        {
            var pos = originPos + (Random.insideUnitSphere * power);
            pos.z = originPos.z;
            cam.transform.position = pos;
            curTime += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        cam.transform.position = originPos; 
    }

    IEnumerator FadeTime(float target)
    {
        var wait = new WaitForSecondsRealtime(0);
        while (Mathf.Abs(Time.timeScale - target) >= 0f)
        {
            Time.timeScale = Mathf.MoveTowards(Time.timeScale, target, 2 * Time.deltaTime);
            yield return wait;
        }
    }
    public static Vector3 CalculateBezier(Vector3 pos1, Vector3 pos2, Vector3 pos3, float t)
    {
        var p12 = Vector3.Lerp(pos1, pos2, t);
        var p23 = Vector3.Lerp(pos2, pos3, t);

        var p1223 = Vector3.Lerp(p12, p23, t);

        return p1223;
    }
}
