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
    public Dictionary<string, AbilityBase> curAbilityDic = new Dictionary<string, AbilityBase>();   

    public Transform[] cards;


    [Header("Card Show Panel")]
    [SerializeField] float bounceHeight = 70f;

    [SerializeField] float upPosY = 200;
    [SerializeField] float downPosY = -550;

    //[SerializeField] Transform targetPos;
    List<AbilityBase> selectabs = new List<AbilityBase>();
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Select();
        }
    }

    

    //능력들이 모두 강화되었는지 확인하는 함수
    public void Select()
    {
        foreach(AbilityBase ability in abilities)
        {
            if(!abilityLevels.ContainsKey(ability.skillName))
            {
                StartCoroutine(ISelect(false));
            }
            if(abilityLevels[ability.skillName] < 5)
            {
                StartCoroutine(ISelect(false));
            }
        }
    }

    //카드에 능력을 배정하고 등장시키는 함수
    IEnumerator ISelect(bool end)
    {
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
            else StartCoroutine(ICardEnd(cards[i], cards[i].position, new Vector3(cards[i].position.x, downPosY, 0), 1f));
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(1.5f);
        isSelect = (end) ? false : true;
    }


    //레벨 5가 아닌 능력 중 랜덤으로 가져오는 함수
    AbilityBase GetRandomAbility()
    {
        List<AbilityBase> abs = abilities;
        foreach(AbilityBase a in abs)
        {
            if((abilityLevels.ContainsKey(a.skillName) && abilityLevels[a.skillName] >= 5)) {
                abs.Remove(a);
            }
        }
        Debug.Log(abs.Count);
        
        AbilityBase ab = abs[Random.Range(0, abs.Count)];
        if(abs.Count - cards.Length > 0)
        {
            while(selectabs.Contains(ab))
            {
                ab = abs[Random.Range(0, abs.Count)];
            }
        }
        //while (abilityLevels.ContainsKey(ab.skillName) && abilityLevels[ab.skillName] >= 5)
        //{
        //    ab = abilities[Random.Range(0, abilities.Count)];
        //    if ( && selectabs.Contains(ab)) continue;
           
        //}
        ab.level = (abilityLevels.ContainsKey(ab.skillName)) ? abilityLevels[ab.skillName] : 1;
        return ab;

    }

    //카드 등장 함수
    IEnumerator ICardSpawn(Transform t, Vector3 startPos, Vector3 endPos, float duration)
    {
        t.position = startPos;
        yield return t.DOMove(endPos + transform.up * bounceHeight, duration).WaitForCompletion();
        t.DOMove(endPos, duration / 2);

    }


    //카드 퇴장 함수
    IEnumerator ICardEnd(Transform t, Vector3 startPos, Vector3 endPos, float duration)
    {
        t.position = startPos;
        yield return t.DOMove(startPos + Vector3.up * bounceHeight, duration / 2).WaitForCompletion();
        t.DOMove(endPos, duration).WaitForCompletion();

    }


    //선택한 카드를 현재 능력 배열에 추가시키고 선택을 종료하는 함수
    public void SelectEnd(AbilityBase abi)
    {

        if(abilityLevels.ContainsKey(abi.skillName))
        {
            abilityLevels[abi.skillName]++;
            curAbilityDic[abi.skillName].LevelUp();
        } else
        {
            curAbilityList.Add(abi);
            abilityLevels.Add(abi.skillName, 2);
            curAbilityDic.Add(abi.skillName, abi);
        }
        
        StartCoroutine(ISelect(true));
    }

}




