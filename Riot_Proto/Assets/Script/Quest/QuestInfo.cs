using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable][CreateAssetMenu(fileName = "Quest Data", menuName = "Quest Data", order = 0)]
public class QuestInfo : ScriptableObject
{
    [SerializeField] private string questName;  
    public string QuestName
    {
        get { return questName; }
    }
    [SerializeField] private Event_Type eventType;
    public Event_Type EventType
    {
        get { return eventType; }
    }
    [SerializeField] private int clear;
    public int Clear
    {
        get { return clear; }
    }
    [SerializeField] private string target;
    public string Target
    {
        get { return target; }
    }
    [SerializeField] private int reward;
    public int Reward
    {
        get { return reward; }
    }

   
}
