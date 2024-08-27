using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    [Header("UI")]
    public int num_Chapter;
    public TextMeshProUGUI TMP_Title;
    public GameObject popUp_WannaBackMenu;

    public GameObject ending_Credit;
    public GameObject BTN_Home;

    [Header("이 챕터의 말풍선")]
    [Tooltip("말풍선 리스트")] public List<GameObject> BubbleList;
    [Tooltip("현재 조회중인 말풍선 리스트")] public int thisBubbleNum;

    private static EndingManager _instance;
    public static EndingManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(EndingManager)) as EndingManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    public void Start()
    {
        Init();
    }

    // 스토리 조회를 위한 초기화
    public void Init()
    {   
        BTN_Home.SetActive(false);
        ending_Credit.SetActive(false);

        Dialogue_Manager.Instance.Load_Dialogue(num_Chapter);

        for (int i = 0; i < BubbleList.Count; i++)
        {
            BubbleList[i].SetActive(false);
        }

        Next_Bubble(); // 첫 번째 대사를 띄움
    }

    // 다음 말풍선을 띄우는 메서드
    // 0 (처음), -1(끝내기)
    public void Next_Bubble(int num = 0)
    {
        if (num == 0) // 시작
        {
            thisBubbleNum = num;
            BubbleList[thisBubbleNum].SetActive(true);
        }
        else if (num == -1) // 마지막 말풍선에서 다음 말풍선을 띄울 때
        {
            BubbleList[thisBubbleNum].SetActive(false);
            thisBubbleNum = -1;
            ending_Credit.SetActive(true);
            BTN_Home.SetActive(true);
        }
        else // 현재 조회중이던 말풍선을 끄고 다음 말풍선을 띄움
        {
            BubbleList[thisBubbleNum].SetActive(false);
            thisBubbleNum = num;
            BubbleList[thisBubbleNum].SetActive(true);
        }
    }
    
    // 이전 화면으로 돌아가기
    public void Onclick_BackToMenu()
    {
        // 메뉴로 돌아가겠냐는 팝업을 띄움
        popUp_WannaBackMenu.SetActive(true);
    }

    // 이전 화면으로 돌아가기
    public void Back_Menu()
    { 
        popUp_WannaBackMenu.SetActive(false);
        GameManager.Instance.SetState(eState.Start);
    } 
}
