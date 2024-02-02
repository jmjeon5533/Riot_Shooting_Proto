using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData : IListener
{
    public string questName;
    [SerializeField] private bool isClear = false;
    public int progress = 0;
    [SerializeField] private int clear;
    [SerializeField] private int reward;
    [SerializeField] private bool isEarned = false;
    [SerializeField] private QuestInfo quest;
    private bool isInit = false;

    public int Progress
    {
        get { return progress; }
    }

    public int Clear
    {
        get { return clear; }
    }

    public int Reward
    {
        get { return reward; }
    }
 
    public QuestData(QuestInfo info)
    {
        questName = info.QuestName;
        this.clear = info.Clear;
        quest = info;
        reward = info.Reward;
        EventManager.Instance.AddListener(info.EventType, this);
    }

    public void Init()
    {
        if(!isInit)
        {
            isInit = true;
            EventManager.Instance.AddListener(quest.EventType, this);
        }
    }

    public void Select()
    {
        
    }

    public bool IsClear()
    {
        if(progress >= clear)
        {
            isClear = true;
        }
        return isClear;
    }

    public bool IsEarn()
    {
        return isEarned;
    }

    public void GetReward()
    {
        SceneManager.instance.playerData.PlayerMoney += reward;
        isEarned = true;
        
    }

    public void OnEvent(Event_Type type, Component sender, object param = null)
    {
        
        if(quest.EventType == Event_Type.EnemyDeath)
        {
            if (quest.Target != null && quest.Target != "")
            {
                var target = sender as EnemyBase;
                if(quest.Target == target.EnemyTag)
                {
                    progress++;
                }
            } else if(quest.Target != null)
            {
                progress++;
            }
        } else
        {
            progress++;
        }
        if (progress >= clear)
        {
            progress = clear;
            isClear = true;
        }
        Debug.Log(progress);
    }
}
