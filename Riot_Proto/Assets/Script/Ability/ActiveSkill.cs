using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ActiveSkill : MonoBehaviour, IPointerDownHandler
{
    float maxCooltime;
    float cooltime = 0;

    Color color;

    bool useSkill = false;

    bool isOne = false;

    [SerializeField] Image coolTimeUI;

    [SerializeField] float speed = 3;


    // Start is called before the first frame update
    void Start()
    {
        color = coolTimeUI.color;
    }

    private void Update()
    {
        AbilityBase ability = AbilityCard.Instance.GetActiveSKill();
        //if(ability.IsSkillCool())
        //{
        //    coolTimeUI.fillAmount =  Mathf.MoveTowards(coolTimeUI.fillAmount, (1 - (ability.GetMinCool()/ability.GetMaxCool())), Time.deltaTime * speed);
        //} else
        //{
        //    coolTimeUI.fillAmount = 0;
        //}
        if(!ability.IsSkillCool() && !isOne)
        {
            isOne = true;
            coolTimeUI.DOColor(Color.white, 0.2f).OnComplete(() => { coolTimeUI.DOFade(0, 0.4f); });
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
            coolTimeUI.color = color;
            coolTimeUI.fillAmount = 1;
            skill.Ability();
            if(g.curEnemys.Count > 0 )
                Instantiate(g.Bomb,g.player.transform.position,Quaternion.Euler(-90,0,0));
            maxCooltime = skill.GetMaxCool();
            useSkill = true;
            isOne = false;
        }

    }
}
