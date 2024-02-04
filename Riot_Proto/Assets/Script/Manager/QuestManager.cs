using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class TimeUtils
{
    public static string GetCurrentDate()
    {
        return DateTime.Now.ToString();
    }
}

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance
    {
        get; set;
    }

    public int questId;
    [SerializeField] private List<QuestInfo> questInfos = new();
    [SerializeField] private QuestPanel[] questPanels = new QuestPanel[3];

    public QuestData[] questPanelDatas = new QuestData[3];
    //[SerializeField] public List<QuestData> questList = new();
    private List<QuestInfo> curQuestInfoList = new List<QuestInfo>();

    

    private void OnLevelWasLoaded(int level)
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Title") return;
        if(TitleManager.instance != null)
        {
            questPanels = TitleManager.instance.GetQuestPanels();
            //var data = SceneManager.instance.questData;
            for (int i = 0; i < questPanelDatas.Length; i++)
            {
                questPanels[i].Init(questPanelDatas[i]);
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else
        {
            Destroy(this.gameObject);
        }
       
    }

    private void Start()
    {
        Initialize();
        //if (SceneManager.instance != null)
        //{
        //    SceneManager.instance.JsonSave();
        //}
        
    }

    public List<QuestData> GetCurrentQuests()
    {
        //List<QuestData> list = new();
        //for(int i = 0; i < 3; i++)
        //{
        //    if(questPanels[i].QuestData != null)
        //    {
        //        list.Add(questPanels[i].QuestData);
        //    }
        //}
        return questPanelDatas.ToList();
    }

    private void Initialize()
    {
        //Debug.Log(TimeUtils.GetCurrentDate().Split(' ')[0].Split('-')[2]);
        var data = SceneManager.instance.questData;
        var date = data.date;
        if(string.IsNullOrEmpty(date)) date = TimeUtils.GetCurrentDate();
        Debug.Log(date);
        date = date.Split(' ')[0];
        date = date.Split('-')[2];
        //questList = data.selectData;
        var newDate = TimeUtils.GetCurrentDate();
        newDate = newDate.Split(' ')[0];
        newDate = newDate.Split('-')[2];
        
        Debug.Log(data.showData.Count);
        
        for (int i = 0; i < data.showData.Count; i++)
        {
            Debug.Log(data.showData[i].questName);
            Debug.Log(data.showData[i].IsClear());
            data.showData[i].Init();
            questPanels[i].Init(data.showData[i]);
            questPanelDatas[i] = data.showData[i];
        }
        
        if(data.showData.Count == 0)
        {
            GenerateData(true);
        } else
        {
            GenerateData();
        }
        Debug.Log(int.Parse(date) + " " + int.Parse(newDate));
        if (int.Parse(date) != int.Parse(newDate))
        {
            GenerateData(true);
        }
        ShowPanel();
    }

    public void GenerateData(bool reset = false)
    {
        
        curQuestInfoList.Clear();
        for(int i = 0; i < 3; i++)
        {
            if (!reset && questPanels[i].QuestData != null) continue;
            QuestInfo info = questInfos[UnityEngine.Random.Range(0,questInfos.Count)];
            while(curQuestInfoList.Contains(info))
            {
                info = questInfos[UnityEngine.Random.Range(0, questInfos.Count)];
            }
            curQuestInfoList.Add(info);
            QuestData data = new QuestData(info);
            questPanels[i].Init(data);
            questPanelDatas[i] = data;
            
        }
    }

    public void InitPanel()
    {
        for (int i = 0; i < 3; i++)
        {
            var panel = questPanels[i].GetComponent<RectTransform>();
            panel.DOAnchorPosX(0, 0);
        }
        
    }

    public void ShowPanel()
    {
        StartCoroutine(ShowQuestPanels());
        IEnumerator ShowQuestPanels()
        {
            for (int i = 0; i < questPanelDatas.Length; i++)
            {
                questPanels[i].Init(questPanelDatas[i]);
            }
            for (int i = 0; i < 3; i++)
            {
                var panel = questPanels[i].GetComponent<RectTransform>();
                panel.DOAnchorPosX(300, 0);
            }
            for (int i = 0; i < 3; i++)
            {
                var panel = questPanels[i].GetComponent<RectTransform>();
                panel.DOAnchorPosX(0, 0.6f);
                yield return new WaitForSeconds(0.3f);
            }
           
        }
    }

    public void HidePanel()
    {
        StartCoroutine(HideQuestPanels());
        IEnumerator HideQuestPanels()
        {
            for (int i = 0; i < 3; i++)
            {
                var panel = questPanels[i].GetComponent<RectTransform>();
                panel.DOAnchorPosX(0, 0);
            }
            for (int i = 0; i < 3; i++)
            {
                var panel = questPanels[i].GetComponent<RectTransform>();
                panel.DOAnchorPosX(300, 0.6f);
                yield return new WaitForSeconds(0.3f);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
       
    }
}
