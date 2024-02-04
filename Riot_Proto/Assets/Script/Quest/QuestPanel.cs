using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : MonoBehaviour
{
    public QuestData QuestData
    {
        get { return questData; }
    }
    
    [SerializeField] private Image icon;
    [SerializeField] private Image progress;
    [SerializeField] private Text questText;
    [SerializeField] private GameObject clearTab;
    [SerializeField] private Color progressColor;
    [SerializeField] private Color clearColor;
    [SerializeField] private Button button;
    [SerializeField] private QuestData questData = null;
    [SerializeField] private Text rewardText;
    

    public void Init(QuestData data)
    {
        if (data == null) return;
        questData = data;
        questText.text = data.questName;
        rewardText.text = $"{data.Reward}";
        clearTab.SetActive(questData.IsEarn());
        
    }
    
    public void QuestSelect()
    {
        questData.Select();
        SceneManager.instance.JsonSave();
        if(questData.IsClear() && !questData.IsEarn()) GetReward();
        
    }

    private void ShowQuestProcess()
    {
        if(!questData.IsClear())
        {
            progress.fillAmount = (float)questData.Progress/(float)questData.Clear;
            progress.color = progressColor;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
       
    }

    private void GetReward()
    {
        questData.GetReward();
        clearTab.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        ShowQuestProcess(); 
        if(questData.IsClear())
        {
            progress.fillAmount = 1;
            progress.color = clearColor;
            button.enabled = true;
        } else
        {
            button.enabled = false;
        }
    }
}
