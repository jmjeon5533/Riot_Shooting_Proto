using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    [SerializeField] Image img;
    [SerializeField] Image coolTimeUI;
    public Image colorImg;

    [SerializeField] float speed = 3;

    public AbilityBase ability;

    float curCooltime;
    float maxCooltime;

    private void Update()
    {
         if(ability.type == AbilityBase.AbilityType.Passive)
            CheckCooltime();
    }

    public void ResetTimer(float value)
    {
        coolTimeUI.fillAmount = value;
    }

    void CheckCooltime()
    {
        if (ability.IsSkillCool())
        {
            coolTimeUI.fillAmount = Mathf.MoveTowards(coolTimeUI.fillAmount, (1 - (ability.GetMinCool() / ability.GetMaxCool())), Time.deltaTime * speed);
        }
        else
        {
            coolTimeUI.fillAmount = 0;
        }
    }

    public Image GetImage()
    {
        return img;
    }
}
