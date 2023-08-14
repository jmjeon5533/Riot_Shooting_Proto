using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class AbilityCard : MonoBehaviour
{
    public static AbilityCard Instance { get; private set; }

    public List<AbilityBase> abilities;

    public Dictionary<string, int> abilityLevels = new();

    public List<AbilityBase> curAbilityList;
    public Dictionary<string, AbilityBase> curAbilityDic = new();

    public Transform[] cards;


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
            if (abilityLevels[ability.skillName] < 5)
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
        panel.SetActive(true);
        if (!end)
        {
            //isSelect = true;
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
            panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0f), 1f);
        }
        yield return new WaitForSecondsRealtime(1.5f);

        if (end)
        {

            //isSelect = false;
            // panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 0.5f);
            panel.SetActive(false);
        }
        isSelect = !end;
    }


    //���� 5�� �ƴ� �ɷ� �� �������� �������� �Լ�
    AbilityBase GetRandomAbility()
    {
        List<AbilityBase> abs = new List<AbilityBase>(abilities);
        List<AbilityBase> removeToList = new List<AbilityBase>();
        for(int i = 0; i < abs.Count; i++)
        {
            if ((abilityLevels.ContainsKey(abs[i].skillName) && abilityLevels[abs[i].skillName] >= 5))
            {
                removeToList.Add(abs[i]);
                
            }
        }
        foreach(AbilityBase rem in removeToList)
        {
            abs.Remove(rem);
        } 
        removeToList.Clear();
        Debug.Log(abs.Count + " test");
        if (abs.Count - cards.Length >= 0)
        {

            for (int i = 0; i < abs.Count; i++)
            {
                foreach(AbilityBase ability in selectabs)
                {
                    if(ability.skillName == abs[i].skillName) removeToList.Add(abs[i]);
                }
            }
        }
        foreach (AbilityBase rem in removeToList)
        {
            abs.Remove(rem);
        }
        removeToList.Clear();
        Debug.Log(abs.Count);
        
       
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
            Debug.Log(ab.gameObject.name);
            ab = curAbilityDic[ab.skillName];
            Debug.Log(curAbilityDic[ab.skillName].skillName);
            ab.level = abilityLevels[ab.skillName];
            
        }
        else
        {
            ab.level = (abilityLevels.ContainsKey(ab.skillName)) ? abilityLevels[ab.skillName] : 1;
        }
        
        return ab;
    }

    //ī�� ���� �Լ�
    IEnumerator ICardSpawn(Transform t, Vector3 startPos, Vector3 endPos, float duration)
    {
        t.position = startPos;
        t.DOMove(endPos + transform.up * bounceHeight, duration).SetUpdate(true);
        yield return new WaitForSecondsRealtime(duration);
        t.DOMove(endPos, duration / 2).SetUpdate(true);
    }
    //ī�� ���� �Լ�
    IEnumerator ICardEnd(Transform t, Vector3 startPos, Vector3 endPos, float duration)
    {

        t.position = startPos;
        t.DOMove(startPos + Vector3.up * bounceHeight, duration / 2).SetUpdate(true);
        yield return new WaitForSecondsRealtime(duration / 2);
        t.DOMove(endPos, duration).SetUpdate(true).WaitForCompletion();
    }
    //������ ī�带 ���� �ɷ� �迭�� �߰���Ű�� ������ �����ϴ� �Լ�
    public void SelectEnd(AbilityBase abi)
    {
        if (abilityLevels.ContainsKey(abi.skillName))
        {
            abilityLevels[abi.skillName]++;
            curAbilityDic[abi.skillName].LevelUp();
        }
        else
        {
            curAbilityList.Add(abi);
            abilityLevels.Add(abi.skillName, 2);
            curAbilityDic.Add(abi.skillName, abi);
        }
        GameManager.instance.StopCoroutine(GameManager.instance.FadeCoroutine);
        GameManager.instance.FadeCoroutine = null;
        Time.timeScale = 1;
        StartCoroutine(ISelect(true));
    }
}