using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StatusSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Image Slider;
    [SerializeField] Text Text;
    public int level;
    public int Cost;

    float curValue;
    bool IsPush;
    public string StatText;
    public void OnPointerDown(PointerEventData eventData)
    {
        IsPush = true;
        print(IsPush);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        IsPush = false;
    }
    private void Start()
    {
        TextUpdate();
    }
    void TextUpdate()
    {
        Text.text = $"{StatText} Lv.{(level == 5 ? "Max" : level)}";
    }
    private void Update()
    {
        var money = SceneManager.instance.playerData.PlayerMoney;
        if (IsPush)
        {
            if(level >= 5) return;
            if (money >= 0)
            {
                curValue = Mathf.MoveTowards(curValue, 1, Time.deltaTime);
                SceneManager.instance.playerData.PlayerMoney -= Cost / 100;
                if (curValue >= 1)
                {
                    level++;
                    if (level < 5) curValue -= 1;
                    TextUpdate();
                }
                TitleManager.instance.MoneyUpdate();
            }
        }
        else
        {
            if(level >= 5) return;
            if (curValue > 0)
            {
                curValue = Mathf.MoveTowards(curValue, 0, Time.deltaTime);
                SceneManager.instance.playerData.PlayerMoney += Cost / 100;
                TitleManager.instance.MoneyUpdate();
            }
        }
        Slider.fillAmount = curValue;
    }
}
