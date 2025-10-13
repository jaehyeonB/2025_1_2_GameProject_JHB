using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("UI 요소들")]
    public GameObject questUI;
    public Text questTitleText;
    public Text questDescriptionText;
    public Text questProgressText;
    public Button completeButton;

    [Header("퀘스트 목록")]
    public QuestData[] availableQuests;

    public QuestData currentQuest;
    private int currentQuestIndex = 0;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if(availableQuests.Length > 0)
        {
            StartQuest(availableQuests[0]);
        }
        if(completeButton != null)
        {
            completeButton.onClick.AddListener(CompleteCurrentQuest);
        }
    }

    void Update()
    {
        if(currentQuest != null && currentQuest.isActive)
        {
            CheckQuestProgress();
            UpdateQuestUI();
        }
    }

    void UpdateQuestUI()
    {
        if (currentQuest == null)
            return;
        if(questTitleText != null)
        {
            questTitleText.text = currentQuest.questTitle;
        }

        if(questDescriptionText != null)
        {
            questDescriptionText.text = currentQuest.description;
        }

        if(questProgressText != null)
        {
            questProgressText.text = currentQuest.GetProgressText();
        }
    }

    public void StartQuest(QuestData quest)
    {
        if (quest == null) return;

        currentQuest = quest;
        currentQuest.Initialize();
        currentQuest.isActive = true;

        Debug.Log("퀘스트 시작 : " + questTitleText);
        UpdateQuestUI();
        if(questUI != null)
        {
            questUI.SetActive(true);
        }
    }

    void CheckDeliveryProgress()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null) return;

        float distance = Vector3.Distance(player.position, currentQuest.deliveryPosition);

        if(distance <= currentQuest.deliveryRedius)
        {
            if(currentQuest.currentProgress == 0)
            {
                currentQuest.currentProgress = 1;
            }
        }
        else
        {
            currentQuest.currentProgress = 0;
        }
    }

    //수집 퀘스트 진행 (외부에서 호출)
    public void AddCollectProgress(string itemTag)
    {
        if (currentQuest == null || !currentQuest.isActive) return;

        if (currentQuest.questType == QuestType.Collect && currentQuest.targetTag == itemTag)
        {
            currentQuest.currentProgress++;
            Debug.Log("아이템 수집 : " + itemTag);
        }
    }

    //수집 퀘스트 진행 (외부에서 호출)
    public void AddInteractProgress(string objectTag)
    {
        if (currentQuest == null || !currentQuest.isActive) return;

        if (currentQuest.questType == QuestType.Collect && currentQuest.targetTag == objectTag)
        {
            currentQuest.currentProgress++;
            Debug.Log("상호작용 완료 : " + objectTag);
        }
    }

    public void CompleteCurrentQuest()
    {
        if (currentQuest == null || !currentQuest.isCompleted) return;

        Debug.Log("퀘스트 완료 !" + currentQuest.rewardMessage);

        //완료 버튼 활성화
        if(completeButton != null)
        {
            completeButton.gameObject.SetActive(false);
        }

        //다음 퀘스트가 있으면 시작
        currentQuestIndex++;
        if(currentQuestIndex < availableQuests.Length)
        {
            StartQuest(availableQuests[currentQuestIndex]);
        }
        else
        {
            //모든 퀘스트 완료
            currentQuest = null;
            if(questUI != null)
            {
                questUI.gameObject.SetActive(false);
            }
        }
    }

    void CheckQuestProgress()
    {
        if(currentQuest.questType == QuestType.Delivery)
        {
            CheckDeliveryProgress();
        }

        //퀘스트 완료 체크
        if(currentQuest.IsComplete() && !currentQuest.isCompleted)
        {
            currentQuest.isCompleted = true;

            //완료 버튼 활성화
            if(completeButton != null)
            {
                completeButton.gameObject.SetActive(true);
            }
        }
    }    
}
