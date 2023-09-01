using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActiveSkill : MonoBehaviour, IPointerDownHandler
{
    float maxCooltime;
    float cooltime = 0;

    [SerializeField] float speed;

    bool useSkill = false;

    [SerializeField] Image coolTimeUI;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        AbilityBase ability = AbilityCard.Instance.GetActiveSKill();
        if(ability.IsSkillCool())
        {
            coolTimeUI.fillAmount =  Mathf.MoveTowards(coolTimeUI.fillAmount, (1 - (ability.GetMinCool()/ability.GetMaxCool())), Time.deltaTime * speed);
        } else
        {
            coolTimeUI.fillAmount = 0;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        AbilityBase skill = AbilityCard.Instance.GetActiveSKill();
        if (skill == null) return;
        if (!skill.IsSkillCool())
        {
            coolTimeUI.fillAmount = 1;
            skill.Ability();
            maxCooltime = skill.GetMaxCool();
            useSkill = true;
        }

    }
}
