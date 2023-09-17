using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    [SerializeField] Ability ability;
    Button button;
    Image border;
    Text MuchText;
    bool isSale;
    Image saleImage;
    void Awake()
    {
        button = GetComponent<Button>();
        border = transform.GetChild(0).GetComponent<Image>();
        MuchText = transform.GetChild(1).GetComponent<Text>();
        saleImage = transform.GetChild(2).GetComponent<Image>();
    }
    void Start()
    {
        ability.index = Random.Range(0,3);
        ability.level = Random.Range(1,4);
        MuchText.text = $"{ability.level * 2000}";

        saleImage.color = isSale ? new Color(0,0,0,0.8f) : new Color(0,0,0,0f);

        button.image.sprite = TitleManager.instance.ASkillSprite[ability.index];
        Color[] colors = { Color.yellow, new Color(1,0.5f,0,1), Color.red};
        border.color = colors[ability.level - 1];
        button.onClick.AddListener(()=> 
        {
            if(SceneManager.instance.playerData.PlayerMora >= ability.level * 2000 && !isSale)
            {
                SceneManager.instance.playerData.abilitiy.Add(ability);
                TitleManager.instance.ASkillButtonAdd(SceneManager.instance.playerData.abilitiy.Count - 1);
                SceneManager.instance.playerData.PlayerMora -= ability.level * 2000;
                saleImage.color = new Color(0,0,0,0.8f);
                isSale = true;
                TitleManager.instance.InitPanel(2);
            }
        });
    }
}
