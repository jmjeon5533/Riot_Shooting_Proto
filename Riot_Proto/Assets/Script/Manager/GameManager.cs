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
    [Space(10)]
    public List<GameObject> playerPrefab = new();
    public List<GameObject> ItemList = new();
    public List<GameObject> curEnemys = new();

    public bool IsGame = false;

    public Coroutine FadeCoroutine;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Camera[] camera = { MainCamera, EffectCamera, UICamera, BackCamera};
        SceneManager.instance.SetResolution(camera);
        Instantiate(playerPrefab[SceneManager.instance.CharIndex], new Vector3(-12f, 0, 0), Quaternion.identity);
        UIManager.instance.InitBackGround(SceneManager.instance.StageIndex);
        UIManager.instance.FadeBg.transform.SetAsLastSibling();
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
