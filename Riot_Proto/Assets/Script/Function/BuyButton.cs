using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    [SerializeField] Ability ability;
    Button button;
    Image border;
    void Awake()
    {
        button = GetComponent<Button>();
        border = transform.GetChild(0).GetComponent<Image>();
    }
    void Start()
    {
        ability.index = Random.Range(0,3);
        ability.level = Random.Range(1,4);

        button.image.sprite = TitleManager.instance.ASkillSprite[ability.index];
        Color[] colors = { Color.yellow, new Color(1,0.5f,0,1), Color.red};
        border.color = colors[ability.level - 1];
        button.onClick.AddListener(()=> 
        {
            SceneManager.instance.playerData.abilitiy.Add(ability);
            TitleManager.instance.ASkillButtonAdd(SceneManager.instance.playerData.abilitiy.Count - 1);
        });
    }
}
