using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 선택지 말풍선
/// </summary>
public class Bubble_Select : MonoBehaviour
{
    [Header("해당 대사 번호")]
    public int index = 0;

    [Header("선택지들")]
    public List<GameObject> popUp_Selects;
    public List<int> next_nums;

    [ReadOnly] public string name_ch;
    [ReadOnly] public string dialogue;

    [ReadOnly] public bool canNext = false;

    public TextMeshProUGUI TMP_name;
    public TextMeshProUGUI TMP_dialogue;
    [Tooltip("타이핑 효과 딜레이")] public float text_Delay = 0.1f;

    public void OnEnable()
    {
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
            // 선택지 팝업 띄움
            Set_Selectings();

            canNext = true;
        }
    }

    /// <summary>
    /// 말풍선 누르기
    /// </summary>
    public void Onclick()
    {
        if (canNext)
        {
            // 선택지를 누르면 다음 말풍선으로 이동함
        }
        else
        {
            StopCoroutine(Coroutine_textPrint(text_Delay));
            TMP_dialogue.text = dialogue;
            // 선택지 팝업 띄움
            Set_Selectings();

            canNext = true;
        }
    }

    // 선택지 띄움
    public void Set_Selectings()
    {
        for (int i = 0; i < popUp_Selects.Count; i++)
        {
            popUp_Selects[i].GetComponent<Seletor>().next_num = next_nums[i];
            popUp_Selects[i].SetActive(true);
        }
    }
}
