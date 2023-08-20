using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform Lever;
    RectTransform rect;
    public Vector2 input;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        input = eventData.position - rect.anchoredPosition;
        Lever.anchoredPosition = Vector2.ClampMagnitude(input, rect.rect.width * 0.5f);
    }
    public void OnDrag(PointerEventData eventData)
    {
        input = eventData.position - rect.anchoredPosition;
        Lever.anchoredPosition = Vector2.ClampMagnitude(input, rect.rect.width * 0.5f);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        input = Vector2.zero;
        Lever.anchoredPosition = input;
    }
}
