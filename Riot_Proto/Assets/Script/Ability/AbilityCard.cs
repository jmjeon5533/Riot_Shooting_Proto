using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using UnityEngine.UI;

public class AbilityCard : MonoBehaviour
{
    public static AbilityCard Instance { get; private set; }

    public List<AbilityBase> abilities;

    public Dictionary<string, int> abilityLevels = new();

    public List<AbilityBase> curAbilityList;
    public Dictionary<string, AbilityBase> curAbilityDic = new();
    public List<AbilityBase> curPassiveList = new List<AbilityBase>(); 


    public Transform[] cards;

    [SerializeField] bool isDuplicate = false;


    [Header("Card Show Panel")]
    [SerializeField] float bounceHeight = 70f;

    [SerializeField] float upPosY = 200;
    [SerializeField] float downPosY = -550;

    [SerializeField] GameObject panel;

    //[SerializeField] Transform targetPos;
    [SerializeField] List<AbilityBase> selectabs = new List<AbilityBase>();
    //[SerializeField] float width;
    [Header("Card Select State")]
    public bool isSelect = false;
    public bool isClick = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isSelect)
        {
            if (!IsAbilityLimit()) Select();
        }
    }



    //�ɷµ��� ��� ��ȭ�Ǿ����� Ȯ���ϴ� �Լ�
    public bool IsAbilityLimit()
    {
        foreach (AbilityBase ability in abilities)
        {
            if (!abilityLevels.ContainsKey(ability.skillName))
            {
                return false;
            }
            if (IsAbilityLevelCheck(ability.skillName))
            {
                return false;
            }
        }
        return true;
    }
    public void Select()
    {
        StartCoroutine(ISelect(false));
    }

    //ī�忡 �ɷ��� �����ϰ� �����Ű�� �Լ�
    IEnumerator ISelect(bool end)
    {
        isSelect = true;
        panel.SetActive(true);
        if (!end && !isDuplicate)
        {
            Debug.Log("0");
            panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 0);
            panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0.5f), 0.5f);

        }

        selectabs.Clear();
        for (int i = 0; i < cards.Length; i++)
        {
            if (!end)
            {

                AbilityBase ab = GetRandomAbility();
                selectabs.Add(ab);
                cards[i].GetComponent<Select>().SetAbility(ab);
                StartCoroutine(ICardSpawn(cards[i], cards[i].position, new Vector3(cards[i].position.x, upPosY, 0), 1f));
            }
            else
            {
                StartCoroutine(ICardEnd(cards[i], cards[i].position, new Vector3(cards[i].position.x, downPosY, 0), 1f));
            }

            yield return new WaitForSecondsRealtime(0.15f);
        }
        if (end)
        {
            if (!GameManager.instance.IsLevelDupe())
                panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0f), 1f);
        }
        yield return new WaitForSecondsRealtime(1.5f);

        if (end)
        {
            isSelect = false;
            isClick = false;
            //panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 0.5f);
            if (GameManager.instance.IsLevelDupe())
            {
                isDuplicate = true;
                GameManager.instance.AddXP(0);
                Debug.Log("1");
            }
            else
            {
                panel.SetActive(false);
                isDuplicate = false;
                Debug.Log("2");
            }
        }
        // isSelect = !end;
    }


    bool IsAbilityLevelCheck(string skillName)
    {
        if (curAbilityDic[skillName].type == AbilityBase.AbilityType.Stats)
        {
            if (abilityLevels[skillName] >= 5)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else if (curAbilityDic[skillName].type == AbilityBase.AbilityType.Passive)
        {
            if (abilityLevels[skillName] >= 3)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }
    }

    //���� 5�� �ƴ� �ɷ� �� �������� �������� �Լ�
    AbilityBase GetRandomAbility()
    {
        List<AbilityBase> abs = abilities.ToList();
        List<AbilityBase> removeToList = new List<AbilityBase>();
        for (int i = 0; i < abs.Count; i++)
        {
            if ((abilityLevels.ContainsKey(abs[i].skillName) && !IsAbilityLevelCheck(abs[i].skillName)))
            {
                removeToList.Add(abs[i]);

            }
            if(curPassiveList.Count >= 3 && !curPassiveList.Contains(abs[i]))
            {
                removeToList.Add(abs[i]);
            }
        }
        foreach (AbilityBase rem in removeToList)
        {
            abs.Remove(rem);
        }
        removeToList.Clear();
        //Debug.Log(abs.Count + " test");
        if (abs.Count - cards.Length >= 0)
        {

            for (int i = 0; i < abs.Count; i++)
            {
                foreach (AbilityBase ability in selectabs)
                {
                    if (ability.skillName == abs[i].skillName) removeToList.Add(abs[i]);
                }
            }
        }
        foreach (AbilityBase rem in removeToList)
        {
            abs.Remove(rem);
        }
        removeToList.Clear();

        AbilityBase ab = abs[Random.Range(0, abs.Count)];
        if (abs.Count - cards.Length >= 0)
        {
            while (selectabs.Contains(ab))
            {
                ab = abs[Random.Range(0, abs.Count)];
            }
        }
        if (curAbilityDic.ContainsKey(ab.skillName))
        {
            //Debug.Log(ab.gameObject.name);
            ab = curAbilityDic[ab.skillName];
            //Debug.Log(curAbilityDic[ab.skillName].skillName);
            ab.level = abilityLevels[ab.skillName] + 1;
        }
        else
        {
            ab.level = abilityLevels.ContainsKey(ab.skillName) ? abilityLevels[ab.skillName] + 1 : 1;
        }
        return ab;
    }

    //ī�� ���� �Լ�
    IEnumerator ICardSpawn(Transform t, Vector3 startPos, Vector3 endPos, float duration)
    {
        GameManager.instance.player.Shield(2f);
        t.position = startPos;
        t.DOMove(endPos + (transform.up * bounceHeight), duration).SetUpdate(true);
        yield return new WaitForSecondsRealtime(duration);
        t.DOMove(endPos, duration / 2).SetUpdate(true);
    }
    //ī�� ���� �Լ�
    IEnumerator ICardEnd(Transform t, Vector3 startPos, Vector3 endPos, float duration)
    {

        t.position = startPos;
        t.DOMove(startPos + (Vector3.up * bounceHeight), duration / 2).SetUpdate(true);
        yield return new WaitForSecondsRealtime(duration / 2);
        t.DOMove(endPos, duration).SetUpdate(true).OnComplete(() =>
            {
                if(!GameManager.instance.IsLevelDupe()) Time.timeScale = 1;
            }
        );
    }
    //������ ī�带 ���� �ɷ� �迭�� �߰���Ű�� ������ �����ϴ� �Լ�
    public void SelectEnd(AbilityBase abi)
    {
        isClick = true;
        if (abilityLevels.ContainsKey(abi.skillName))
        {
            abilityLevels[abi.skillName]++;
            curAbilityDic[abi.skillName].LevelUp();
        }
        else
        {
            curAbilityList.Add(abi);
            abilityLevels.Add(abi.skillName, 1);
            curAbilityDic.Add(abi.skillName, abi);
            if(!curPassiveList.Contains(abi) && curPassiveList.Count < 3)
            {
                curPassiveList.Add(abi);
            }
        }
        GameManager.instance.StopCoroutine(GameManager.instance.FadeCoroutine);
        GameManager.instance.FadeCoroutine = null;
        GameManager.instance.SelectChance--;
            StartCoroutine(ISelect(true));
    }
}