using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActiveSkill : MonoBehaviour, IPointerDownHandler
{
    float maxCooltime;
    float cooltime = 0;

    bool useSkill = false;

    [SerializeField] Image coolTimeUI;

    [SerializeField] float speed = 3;


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
        if(Input.GetKeyDown(KeyCode.X))
        {
            OnClick();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClick();
    }

    public void OnClick() {
        var g = GameManager.instance;
        AbilityBase skill = AbilityCard.Instance.GetActiveSKill();
        if (skill == null) return;
        if (!skill.IsSkillCool())
        {
            coolTimeUI.fillAmount = 1;
            skill.Ability();
            
            Instantiate(g.Bomb,g.player.transform.position,Quaternion.identity);
            maxCooltime = skill.GetMaxCool();
            useSkill = true;
        }

    }
}
