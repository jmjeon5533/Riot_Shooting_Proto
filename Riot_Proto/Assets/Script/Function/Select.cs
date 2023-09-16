using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Select : MonoBehaviour, IPointerDownHandler
{
    public string abilityName;
    
    public string explain;

    public AbilityBase ability;

    [SerializeField] Transform explainView;

    [SerializeField] Text nameText;
    [SerializeField] Text description;

    [SerializeField] Text statText;

    [SerializeField] Image icon;

    [SerializeField] Vector2 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAbility(AbilityBase ab)
    {
        ability = ab;
        abilityName = ab.skillName;
        
        explain = ab.skillDescription;
        description.text = explain;
        icon.sprite = ab.skillImage;
        if(ab.type == AbilityBase.AbilityType.Passive && !AbilityCard.Instance.curAbilityList.Contains(ab))
        {
            statText.text = "";
        } else
        {
            statText.text = "";
            //statText.text = ab.GetStatText();
            description.text = explain + "\n\n" + ab.GetStatText();
        }
        nameText.text = abilityName + " " + GetLevelText(ab.cardLevel);
    }

    string GetLevelText(int lv)
    {
        switch(lv)
        {
            case 1:
                return "I";
            case 2:
                return "II";
            case 3:
                return "III";

            case 4:
                return "IV";
            case 5:
                return "V";
            default:
                Debug.LogError("This is an unusual level");
                return "Error";

        }
    }


    public void SelectAbility()
    {
        if (!AbilityCard.Instance.isSelect || AbilityCard.Instance.isClick || AbilityCard.Instance.isMove) {
            return;
        }

        AbilityBase ab;
        if (AbilityCard.Instance.curAbilityDic.ContainsKey(ability.skillName)) { 
            ab = AbilityCard.Instance.curAbilityDic[ability.skillName];
        } else {
            ab = Instantiate(ability.gameObject, GameManager.instance.player.transform).GetComponent<AbilityBase>();

        }
        AbilityCard.Instance.SelectEnd(ab);

    }

   
    public void OnPointerDown(PointerEventData eventData)
    {
        SelectAbility();
    }
}
