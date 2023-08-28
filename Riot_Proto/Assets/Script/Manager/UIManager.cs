using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    public Image XPBar;
    public Transform canvas;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        XPBarUpdate();
    }
    public void XPBarUpdate()
    {
        float xp = GameManager.instance.XP;
        float maxXp = GameManager.instance.MaxXP;
        XPBar.fillAmount = xp / maxXp;
    }
    public void ClearTab()
    {
        
    }
}