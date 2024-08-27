using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

/// <summary>
/// 일러스트 말풍선
/// </summary>
public class Bubble_Illust : MonoBehaviour
{
    public int illustNum;
    
    [Header("일러스트 말풍선에 대사가 있는가 / 대사 등장 딜레이")]
    public bool isHasDialogue = false;
    public float dialogue_Delay = 3f;

    [Header("해당 대사 번호 / 다음 말풍선 번호")]
    public int index = 0;
    public int nextNum = 0;

    [Tooltip("일러스트")] public GameObject target_Illust;

    [ReadOnly] public string name_ch;
    [ReadOnly] public string dialogue;

    [ReadOnly] public bool canNext = false;

    public TextMeshProUGUI TMP_name;
    public TextMeshProUGUI TMP_dialogue;
    [Tooltip("타이핑 효과 딜레이")] public float text_Delay = 0.1f;

    public void OnEnable()
    {
        Init_Illust();

        if (isHasDialogue)
        {
            StartCoroutine(Coroutine_Init(dialogue_Delay));
        }
        
    }

    // 일러스트 켬
    public void Init_Illust()
    {
        target_Illust.SetActive(true);

        // 일러스트 해금
        User_Info.Instance.Set_Data("isOpen_illust_" + illustNum, 1);
    }

    // 대사가 있다면 지연하여 띄움
    IEnumerator Coroutine_Init(float delay)
    {
        yield return new WaitForSeconds(delay);
        Init();
    }

    public void Init()
    {
        name_ch = Dialogue_Manager.Instance.Get_A_Name(index);
        dialogue = Dialogue_Manager.Instance.Get_A_Dialogue(index);

        if (dialogue.Contains(Dialogue_Manager.Instance.Default_PlayerName))
        {
            // 기본 이름일 경우 아무것도 하지 않음
            if (User_Info.Instance.UserName == Dialogue_Manager.Instance.Default_PlayerName)
            {
                
            }
            else // 플레이어 이름이 들어간 대사는 파싱함
            {
                string[] dialogues = dialogue.Split(Dialogue_Manager.Instance.Default_PlayerName);
                string result = "";
                for (int i = 0; i < dialogues.Length; i++)
                {
                    result += dialogues[i];
                    if (i < dialogues.Length-1)
                    {
                        result += User_Info.Instance.UserName;
                    }
                }
#if UNITY_EDITOR
                Debug.Log(result);
#endif
                dialogue = result;
            }
        }

        Show();
    }

    // 대사 띄움
    public void Show()
    {
        TMP_name.text = name_ch;
        StartCoroutine(Coroutine_textPrint(text_Delay));
    }

    // 글자 타이핑 효과
    IEnumerator Coroutine_textPrint(float delay)
    {
        int count = 0;

        while (count != dialogue.Length)
        {
            if (count < dialogue.Length)
            {
                TMP_dialogue.text += dialogue[count].ToString();
                count++;
            }
            else
            {
                // 모든 글자를 출력함
            }

            yield return new WaitForSeconds(delay);
        }

        if (count == dialogue.Length)
        {
            canNext = true;
        }
    }

    /// <summary>
    /// 말풍선 다음 버튼 누르기
    /// </summary>
    public void Onclick_Dialogue()
    {
        if (canNext)
        {
            StoryManager.Instance.Next_Bubble(nextNum);
        }
        else
        {
            StopCoroutine(Coroutine_textPrint(text_Delay));
            TMP_dialogue.text = dialogue;

            canNext = true;
        }
    }

    // 일러스트 누르면 바로 다음 말풍선으로 넘어감
    public void Onclick_Next()
    {
        StoryManager.Instance.Next_Bubble(nextNum);
    }
}
