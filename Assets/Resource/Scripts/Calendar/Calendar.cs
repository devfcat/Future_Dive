using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 시작 전 스토리 진행상태 확인 패널에 부착되는 스크립트
/// </summary>
public class Calendar : MonoBehaviour
{
    [Header("달력 버튼")]
    public List<GameObject> BTN_Days;

    [Header("달력 제어 변수")]
    public int lastDay;
    public bool isReadyEnd = false;

    public void Start()
    {
        Setting();
    }

    // 달력 상태를 설정함
    public void Setting()
    {
        isReadyEnd = false; // 준비중

        // 가장 최근까지 플레이한 내용을 가져옴
        lastDay = User_Info.Instance.playDay;

        // 달력 버튼 비활성화
        for (int i = 0; i < BTN_Days.Count; i++)
        {
            BTN_Days[i].GetComponent<Button>().enabled = false;
            BTN_Days[i].transform.GetChild(0).gameObject.SetActive(false);
            BTN_Days[i].SetActive(false);
        }

        // 해당하는 날짜까지 달력 활성화
        for (int i = 0; i < lastDay; i++)
        {
            BTN_Days[i].SetActive(true);
        }

        // 마지막 날(가장 최근 해금)만 접근 가능하게 설정
        BTN_Days[lastDay-1].transform.GetChild(0).gameObject.SetActive(true);
        BTN_Days[lastDay-1].GetComponent<Button>().enabled = true;

        isReadyEnd = true; // 준비 완료
    }
}
