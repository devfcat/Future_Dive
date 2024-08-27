using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 스토리 씬 공통 관리 매니저
/// </summary>
public class StoryManager : MonoBehaviour
{
    public int num_Chapter;

    [Header("스토리보드 UI")]
    public TextMeshProUGUI TMP_storyTitle;
    public GameObject popUp_WannaBackMenu;
    public GameObject BTN_End; // 스토리 조회 종료 버튼
    public GameObject panel_End;

    [Header("이 챕터의 말풍선")]
    [Tooltip("말풍선 리스트")] public List<GameObject> BubbleList;
    [Tooltip("현재 조회중인 말풍선 리스트")] public int thisBubbleNum;

    private static StoryManager _instance;
    public static StoryManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(StoryManager)) as StoryManager;

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
        BTN_End.SetActive(false);

        Dialogue_Manager.Instance.Load_Dialogue(num_Chapter);
        TMP_storyTitle.text = num_Chapter.ToString() + "일차";

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
            BTN_End.SetActive(true);
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
        GameManager.Instance.SetState(eState.Calendar);
    }

    // 모든 말풍선을 보고 끝내기 버튼을 누르면 
    public void Onclick_End(int plusDay = 1)
    {
        StartCoroutine(Coroutine_StoryEnd(plusDay));
    }

    // 엔딩 씬으로 넘어가는 메서드
    public void Going_Ending()
    {
        int state_like = 0;

        // 스탯 점수 계산해서 해당 엔딩으로 연결
        if (state_like > 0)
        {
            GameManager.Instance.SetState(eState.Ending_H);
        }
        else
        {

        }
    }

    // n일 일수 스토리를 해금하고 달력 메뉴로 돌아감
    IEnumerator Coroutine_StoryEnd(int plusDay)
    {
        panel_End.SetActive(true);
        yield return new WaitForSeconds(3f);
        User_Info.Instance.Set_Data("playDay", num_Chapter + plusDay);
        GameManager.Instance.SetState(eState.Calendar);
    }
}
