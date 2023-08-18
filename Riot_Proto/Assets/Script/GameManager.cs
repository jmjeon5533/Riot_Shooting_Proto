using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public Player player;
    public Vector2 MoveRange;
    public Vector2 MovePivot;
    public float BGSpeed;

    [Space(10)]
    public GameObject XPPrefab;
    public int MaxXP;
    public int AddMaxXP;
    public int XP;
    public int Level;
    public int SelectChance;

    public List<GameObject> playerPrefab = new List<GameObject>();
    public List<GameObject> StagePrefab = new List<GameObject>();

    public List<GameObject> curEnemys = new List<GameObject>();

    public bool IsGame = false;

    public Coroutine FadeCoroutine;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Instantiate(playerPrefab[SceneManager.instance.CharIndex], new Vector3(-12f, 0, 0), Quaternion.identity);
        Instantiate(StagePrefab[SceneManager.instance.StageIndex], UIManager.instance.canvas);
        var bg2 = Instantiate(StagePrefab[SceneManager.instance.StageIndex], UIManager.instance.canvas).GetComponent<RectTransform>();
        bg2.transform.localPosition += new Vector3(bg2.rect.width, 0, 0);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(MovePivot, MoveRange * 2);
    }
    public void AddXP(int Value)
    {
        var ab = AbilityCard.Instance;
        XP += Value;
        for (int i = MaxXP; i <= XP; i += AddMaxXP)
        {
            SelectChance++;
            XP -= MaxXP;
            MaxXP += AddMaxXP;
            Level++;
        }
        if (SelectChance >= 1)
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
