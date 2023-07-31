using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AbilityCard : MonoBehaviour
{
    public static AbilityCard Instance { get; private set; }

    public List<AbilityBase> abilities;

    public Dictionary<string, int> abilityLevels = new Dictionary<string, int>();

    public List<AbilityBase> curAbilityList;

    public Transform[] cards;


    [Header("Card Show Panel")]
    [SerializeField] float bounceHeight = 70f;

    [SerializeField] float upPosY = 200;
    [SerializeField] float downPosY = -550;

    //[SerializeField] Transform targetPos;

    //[SerializeField] float width;
    [Header("Card Select State")]
    public bool isSelect = false;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            if(!IsAbilityLimit())
                Select();
        }
    }

    

    //�ɷµ��� ��� ��ȭ�Ǿ����� Ȯ���ϴ� �Լ�
    bool IsAbilityLimit()
    {
        foreach(AbilityBase ability in abilities)
        {
            if(!abilityLevels.ContainsKey(ability.skillName))
            {
                return false;
            }
            if(abilityLevels[ability.skillName] < 5)
            {
                return false;
            }
            
        }
        return true;
    }

    private void Select()
    {
        StartCoroutine(ISelect(false));
    }

    //ī�忡 �ɷ��� �����ϰ� �����Ű�� �Լ�
    IEnumerator ISelect(bool end)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (!end)
            {
                
                cards[i].GetComponent<Select>().SetAbility(GetRandomAbility());
                StartCoroutine(ICardSpawn(cards[i], cards[i].position, new Vector3(cards[i].position.x, upPosY, 0), 1f));
            }
            else StartCoroutine(ICardEnd(cards[i], cards[i].position, new Vector3(cards[i].position.x, downPosY, 0), 1f));
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(1.5f);
        isSelect = (end) ? false : true;
    }


    //���� 5�� �ƴ� �ɷ� �� �������� �������� �Լ�
    AbilityBase GetRandomAbility()
    {
        AbilityBase ab = abilities[Random.Range(0, abilities.Count)];

        while (abilityLevels.ContainsKey(ab.skillName) && abilityLevels[ab.skillName] >= 5)
        {
            ab = abilities[Random.Range(0, abilities.Count)];
        }
        ab.level = (abilityLevels.ContainsKey(ab.skillName)) ? abilityLevels[ab.skillName] : 1;
        return ab;

    }

    //ī�� ���� �Լ�
    IEnumerator ICardSpawn(Transform t, Vector3 startPos, Vector3 endPos, float duration)
    {
        t.position = startPos;
        yield return t.DOMove(endPos + transform.up * bounceHeight, duration).WaitForCompletion();
        t.DOMove(endPos, duration / 2);

    }


    //ī�� ���� �Լ�
    IEnumerator ICardEnd(Transform t, Vector3 startPos, Vector3 endPos, float duration)
    {
        t.position = startPos;
        yield return t.DOMove(startPos + Vector3.up * bounceHeight, duration / 2).WaitForCompletion();
        t.DOMove(endPos, duration).WaitForCompletion();

    }


    //������ ī�带 ���� �ɷ� �迭�� �߰���Ű�� ������ �����ϴ� �Լ�
    public void SelectEnd(AbilityBase abi)
    {
        curAbilityList.Add(abi);
        if(abilityLevels.ContainsKey(abi.skillName))
        {
            abilityLevels[abi.skillName]++;
        } else
        {
            abilityLevels.Add(abi.skillName, 2);
        }
        
        StartCoroutine(ISelect(true));
    }

}




