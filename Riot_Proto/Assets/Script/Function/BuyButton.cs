using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public Ability ability;
    Button button;
    Image border;
    Text MuchText;
    public bool isSale;
    public Image saleImage;
    void Awake()
    {
        button = GetComponent<Button>();
        border = transform.GetChild(0).GetComponent<Image>();
        MuchText = transform.GetChild(1).GetComponent<Text>();
        saleImage = transform.GetChild(2).GetComponent<Image>();
    }
    void Start()
    {
        ability.index = Random.Range(0, 3);
        ability.level = Random.Range(1, 4);
        MuchText.text = $"{ability.level * 6000}";

        saleImage.color = isSale ? new Color(0, 0, 0, 0.8f) : new Color(0, 0, 0, 0f);

        button.image.sprite = TitleManager.instance.ASkillSprite[ability.index];
        Color[] colors = { Color.yellow, new Color(1, 0.5f, 0, 1), Color.red };
        border.color = colors[ability.level - 1];
        button.onClick.AddListener(() =>
        {
            TitleManager.instance.SelectBuySkill(this);
        });
    }
}
