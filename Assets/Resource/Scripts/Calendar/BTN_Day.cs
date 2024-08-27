using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 스토리 선택 메뉴의 메서드
/// </summary>
public class BTN_Day : MonoBehaviour
{
    [Tooltip("버튼의 일수")] public int day;
    public TextMeshProUGUI TMP_day;

    public void Start()
    {
        TMP_day.text = "";
    }

    public void Init()
    {
        TMP_day.text = day.ToString();
        this.GetComponent<Button>().onClick.AddListener(Onclick);
    }

    public void Onclick()
    {
        GameManager.Instance.SetState(eState.Story, day);
    }
}
