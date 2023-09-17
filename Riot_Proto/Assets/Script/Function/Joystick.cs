using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform Stick;
    public RectTransform Lever;

    Image StickImg, LeverImg;

    public Vector2 input;
    float AlphaTarget;
    Vector2 minusVec;

    private void Awake()
    {
        StickImg = Stick.GetComponent<Image>();
        LeverImg = Lever.GetComponent<Image>();
    }
    private void Update()
    {
        if(Mathf.Abs(StickImg.color.a - AlphaTarget) > 0f)
        {
            var a = Mathf.MoveTowards(StickImg.color.a,AlphaTarget,Time.deltaTime * 5f);
            StickImg.color = new Color(1,1,1,a);
            LeverImg.color = new Color(0,0,0,a);
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        var s = SceneManager.instance;
        var x = Mathf.Abs(s.ScreenArea.x - s.minusScreen.x) / 2;
        var y = Mathf.Abs(s.ScreenArea.y - s.minusScreen.y) / 2;
        minusVec = new Vector2(x, y);
        
        Stick.anchoredPosition = eventData.position - minusVec;
        input = eventData.position - Stick.anchoredPosition - minusVec;
        Lever.anchoredPosition = Vector2.ClampMagnitude(input, Stick.rect.width * 0.5f);
        AlphaTarget = 0.7f;
    }
    public void OnDrag(PointerEventData eventData)
    {
        input = eventData.position - Stick.anchoredPosition - minusVec;
        Lever.anchoredPosition = Vector2.ClampMagnitude(input, Stick.rect.width * 0.5f);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        input = Vector2.zero;
        Lever.anchoredPosition = input;
        AlphaTarget = 0;
    }
}
