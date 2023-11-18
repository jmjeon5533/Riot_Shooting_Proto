using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform Stick;
    public RectTransform Lever;

    [HideInInspector] public Image StickImg, LeverImg;

    public Vector2 input;
    float AlphaTarget;
    Vector2 minusVec;

    public Vector2 Input { get; private set; }

    private void Awake()
    {
        StickImg = Stick.GetComponent<Image>();
        LeverImg = Lever.GetComponent<Image>();

        RectTransform rectTransform = GetComponent<RectTransform>();
        
        var s = SceneManager.instance;
        rectTransform.sizeDelta = s.ScreenWidth*2;
    }
    private void Update()
    {
        if(!GameManager.instance.IsGame) return;
        if(Mathf.Abs(StickImg.color.a - AlphaTarget) > 0f)
        {
            var a = Mathf.MoveTowards(StickImg.color.a,AlphaTarget,Time.deltaTime * 5f);
            StickImg.color = new Color(1,1,1,a);
        }
        if(LeverImg.color.a < 0.5f)
        {
            var b = Mathf.MoveTowards(LeverImg.color.a,0.5f, Time.deltaTime * 5f);
            LeverImg.color = new Color(1,1,1,b);
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!GameManager.instance.IsGame) return;
        var s = SceneManager.instance;
        var x = Mathf.Abs(s.ScreenArea.x - s.ScreenWidth.x) / 2;
        var y = Mathf.Abs(s.ScreenArea.y - s.ScreenWidth.y) / 2;
        minusVec = new Vector2(x, y);
        
        Stick.localPosition = eventData.position - minusVec - (Stick.sizeDelta / 2);
        input = eventData.position - (Vector2)Stick.localPosition - minusVec - (Stick.sizeDelta / 2);
        Input = input.normalized;
        Input *= input.magnitude / (Stick.rect.width * 0.5f);
        Lever.localPosition = Vector2.ClampMagnitude(input, Stick.rect.width * 0.5f);
        AlphaTarget = 0.3f;
        //print($"{eventData.position} : {Stick.anchoredPosition} : {minusVec}");
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(!GameManager.instance.IsGame) return;
        input = eventData.position - Stick.anchoredPosition - minusVec - (Stick.sizeDelta / 2);
        Input = input.normalized;
        Input *= Vector2.ClampMagnitude(input, Stick.rect.width * 0.5f).magnitude / (Stick.rect.width * 0.5f);
        Lever.anchoredPosition = Vector2.ClampMagnitude(input, Stick.rect.width * 0.5f);
        //print($"{eventData.position} : {Stick.anchoredPosition} : {minusVec}\n{s.ScreenArea.x}-{s.ScreenWidth.x}");
        AlphaTarget = 0.3f;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if(!GameManager.instance.IsGame) return;
        input = Vector2.zero;
        Input = Vector2.zero;
        Lever.anchoredPosition = input;
        AlphaTarget = 0;
    }
}
