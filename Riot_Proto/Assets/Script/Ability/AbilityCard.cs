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

    public AbilityBase[] activeSkills;

    public Dictionary<string, int> abilityLevels = new Dictionary<string,int>();

    public List<AbilityBase> curAbilityList;
    public Dictionary<string, AbilityBase> curAbilityDic = new Dictionary<string, AbilityBase>();
    public List<AbilityBase> curPassiveList = new List<AbilityBase>();
    public Dictionary<string, AbilityBase> curPassiveDic = new Dictionary<string, AbilityBase>();

    private AbilityBase activeSkill;


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
    public bool isMove = false;
    [Header("Skill UI")]
    [SerializeField] GameObject activeSkillUI;
    [SerializeField] Image skillCoolUI;
    [SerializeField] GameObject skillListUI;

    List<GameObject> skillIcons = new List<GameObject>();
    [SerializeField] GameObject skillIconObj;
    [SerializeField] Text clickText;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
        if (SceneManager.instance.ActiveIndex == -1) return;
        AddSkill(activeSkills[SceneManager.instance.ActiveIndex]);
            
    }

    Tween tween;

    // Update is called once per frame
    void Update()
    {
        
    }



    //�ɷµ��� ��� ��ȭ�Ǿ����� Ȯ���ϴ� �Լ�
    public bool IsAbilityLimit()
    {
        foreach (AbilityBase ability in abilities)
        {
            if (ability.type.Equals(AbilityBase.AbilityType.Passive) && !curPassiveList.Contains(ability) && curPassiveList.Count >= 3) continue;
            if (ability.type.Equals(AbilityBase.AbilityType.Active) && activeSkill != null && !ability.Equals(activeSkill)) continue;
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
        SoundManager.instance.SetAudio("LevelUp", SoundManager.SoundState.SFX, false);
    }

    //ī�忡 �ɷ��� �����ϰ� �����Ű�� �Լ�
    IEnumerator ISelect(bool end)
    {
        isSelect = true;
        panel.SetActive(true);
        if (!end && !isDuplicate)
        {
            panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 0);
            panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0.5f), 0.5f);
            tween = clickText.DOFade(1, 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);

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
                panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0.5f), 0.5f);
                GameManager.instance.AddXP(0);
            }
            else
            {
                panel.SetActive(false);
                isDuplicate = false;
                clickText.DOFade(0, 0.5f).OnComplete(() => tween.Pause());
                
            }
        }
        // isSelect = !end;
    }


    bool IsAbilityLevelCheck(string skillName)
    {
       
            if (abilityLevels[skillName] >= 4)
            {
                return false;
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
            if (abs[i].type == AbilityBase.AbilityType.Active && activeSkill != null && !abs[i].skillName.Equals(activeSkill.skillName))
            {
                removeToList.Add(abs[i]);
                continue;
            }
            if(activeSkill == null && abs[i].type.Equals(AbilityBase.AbilityType.Active))
            {
                removeToList.Add(abs[i]);
                continue;
            }
            if (curPassiveList.Count >= 3 && !curPassiveDic.ContainsKey(abs[i].skillName) && abs[i].type == AbilityBase.AbilityType.Passive)
            {
                removeToList.Add(abs[i]);
            }
        }
        for (int i = 0; i < abs.Count; i++)
        {
            if ((abilityLevels.ContainsKey(abs[i].skillName) && !IsAbilityLevelCheck(abs[i].skillName)))
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
            ab.cardLevel = abilityLevels[ab.skillName] + 1;
        }
        else
        {
            ab.cardLevel = abilityLevels.ContainsKey(ab.skillName) ? abilityLevels[ab.skillName] + 1 : 1;
        }
        return ab;
    }

    //ī�� ���� �Լ�
    IEnumerator ICardSpawn(Transform t, Vector3 startPos, Vector3 endPos, float duration)
    {
        isMove = true;
        GameManager.instance.player.Shield(2f);
        t.position = startPos;
        t.DOMove(endPos + (transform.up * bounceHeight), duration).SetUpdate(true);
        yield return new WaitForSecondsRealtime(duration);
        t.DOMove(endPos, duration / 2).SetUpdate(true);
        isMove = false;
    }
    //ī�� ���� �Լ�
    IEnumerator ICardEnd(Transform t, Vector3 startPos, Vector3 endPos, float duration)
    {
        isMove = true;
        t.position = startPos;
        t.DOMove(startPos + (Vector3.up * bounceHeight), duration / 2).SetUpdate(true);
        yield return new WaitForSecondsRealtime(duration / 2);
        t.DOMove(endPos, duration).SetUpdate(true).OnComplete(() =>
            {
                if (!GameManager.instance.IsLevelDupe()) Time.timeScale = 1;
            }
        );
        isMove = false;
    }

    public void AddSkill(AbilityBase abi)
    {
        if (abi == null) return;
        if (abilityLevels.ContainsKey(abi.skillName))
        {
            abilityLevels[abi.skillName]++;
            curAbilityDic[abi.skillName].LevelUp();
        }
        else
        {
            AbilityBase ab;
            if (curAbilityDic.ContainsKey(abi.skillName))
            {
                ab = curAbilityDic[abi.skillName];
            }
            else
            {
                ab = Instantiate(abi.gameObject, GameManager.instance.player.transform).GetComponent<AbilityBase>();
            }
            curAbilityList.Add(ab);
            abilityLevels.Add(ab.skillName, 1);
            curAbilityDic.Add(ab.skillName, ab);
            if (!curPassiveList.Contains(ab) && curPassiveList.Count < 3 && ab.type == AbilityBase.AbilityType.Passive)
            {
                curPassiveList.Add(ab);
                curPassiveDic.Add(ab.skillName, ab);
            }
            if (activeSkill == null && ab.type == AbilityBase.AbilityType.Active)
            {
                activeSkill = ab;
                abilities.Add(abi);
                ShowActiveSkillButton();
                AddSkillList(ab);
            }
        }
    }

    void AddSkillList(AbilityBase abi)
    {
        if (abi.type == AbilityBase.AbilityType.Stats) return;
        var img = Instantiate(skillIconObj, skillListUI.transform).GetComponent<Image>();
        //img.transform.parent = skillListUI.transform;
        img.gameObject.name = abi.skillName;
        img.transform.localScale = Vector3.one;
        img.transform.localPosition = Vector3.zero;
        img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 90);
        img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 90);
        img.GetComponent<SkillIcon>().GetImage().rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 80);
        img.GetComponent<SkillIcon>().GetImage().rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 80);
        img.GetComponent<SkillIcon>().GetImage().sprite = abi.skillImage;
        abi.skillIcon = img.GetComponent<SkillIcon>();
        img.GetComponent<SkillIcon>().ability = abi;
        skillIcons.Add(img.gameObject);
        var u = UIManager.instance;
        img.rectTransform.anchoredPosition = u.iconPosList[0].anchoredPosition;
        for(int i = 0; i < skillIcons.Count; i++)
        {
            skillIcons[skillIcons.Count-i-1].GetComponent<Image>().rectTransform.DOAnchorPos(u.iconPosList[i + 1].anchoredPosition, 1);
        }

    }

    void SetSkillColor(AbilityBase abi)
    {
        foreach(var img in skillIcons)
        {
            if(img.name.Equals(abi.skillName))
            {
                var imgColor = img.GetComponent<SkillIcon>();
                switch(abilityLevels[abi.skillName])
                {
                    case 2:
                        imgColor.colorImg.color = Color.green;
                        break;
                    case 3:
                        imgColor.colorImg.color = Color.blue;
                        break;
                    case 4:
                        imgColor.colorImg.color = new Color(100/255f,1 / 255f, 255 / 255f);
                        break;
                    case 5:
                        imgColor.colorImg.color = new Color(255 / 255f, 175 / 255f, 1 / 255f);
                        break;
                    case 6:
                        imgColor.colorImg.color = new Color(1 / 255f, 255 / 255f, 255 / 255f);
                        break;
                }
                break;
            }
        }
    }
    
    //������ ī�带 ���� �ɷ� �迭�� �߰���Ű�� ������ �����ϴ� �Լ�
    public void SelectEnd(AbilityBase abi)
    {
        isClick = true;
        if (abilityLevels.ContainsKey(abi.skillName))
        {
            abilityLevels[abi.skillName]++;
            curAbilityDic[abi.skillName].LevelUp();
            SetSkillColor(abi);
        }
        else
        {
            AddSkillList(abi);
            curAbilityList.Add(abi);
            abilityLevels.Add(abi.skillName, 1);
            curAbilityDic.Add(abi.skillName, abi);
            if(!curPassiveList.Contains(abi) && curPassiveList.Count < 3 && abi.type == AbilityBase.AbilityType.Passive)
            {
                curPassiveList.Add(abi);
                curPassiveDic.Add(abi.skillName, abi);
            }
            if(activeSkill == null && abi.type == AbilityBase.AbilityType.Active)
            {
                activeSkill = abi;
                ShowActiveSkillButton();
            }
        }
        GameManager.instance.StopCoroutine(GameManager.instance.FadeCoroutine);
        GameManager.instance.FadeCoroutine = null;
        GameManager.instance.SelectChance--;
            StartCoroutine(ISelect(true));
        
    }

    void ShowActiveSkillButton()
    {
        activeSkillUI.SetActive(true);
        activeSkillUI.GetComponent<Image>().sprite = activeSkill.skillImage;
        skillCoolUI.fillAmount = 0;
    }

    

    public AbilityBase GetActiveSKill()
    {
        return activeSkill;
    }
}