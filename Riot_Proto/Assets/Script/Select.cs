using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Select : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public string abilityName;
    
    public string explain;

    public AbilityBase ability;

    [SerializeField] Transform explainView;

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
    }

    public void SelectAbility()
    {
        if (!AbilityCard.Instance.isSelect) return;
        //Instantiate(ability.gameObject, Player.Instance.transform);
        AbilityCard.Instance.SelectEnd();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        explainView.gameObject.SetActive(true);
        explainView.GetChild(0).GetComponent<Text>().text = abilityName;
        explainView.GetChild(1).GetComponent<Text>().text = explain;
        explainView.position = eventData.position + offset;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        explainView.gameObject.SetActive(false);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        explainView.position = eventData.position + offset;

    }
}
