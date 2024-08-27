using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Start 씬의 UI 관리매니저 스크립트
/// </summary>
public class StartManager : MonoBehaviour
{
    [Header("팝업")]
    public GameObject popUp_Info;
    public GameObject popUp_WannaReset;

    [Header("버튼")]
    public GameObject BTN_NewStart;
    public GameObject BTN_LoadStart;

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        // 이어할 파일이 있을 경우에만 이어하기 버튼 활성화
        if (User_Info.Instance.isExistSaveFile == 0)
        {
            BTN_NewStart.SetActive(true);
            BTN_LoadStart.SetActive(false);
        }
        else
        {
            BTN_NewStart.SetActive(true);
            BTN_LoadStart.SetActive(true);
        }
    }

    public void Start_NewGame()
    {
        if (User_Info.Instance.isExistSaveFile == 0)
        {
            GameManager.Instance.SetState(eState.Calendar);
        }
        else
        {
            // 세이브가 있는데 새로 시작할지 물어보는 팝업 띄움
            Open_WannaReset();
        }
    }

    public void Open_WannaReset()
    {
        popUp_WannaReset.SetActive(true);
    }

    public void Reset()
    {
        popUp_WannaReset.SetActive(false);
        User_Info.Instance.Reset_Data();
        Start_NewGame();
    }

    public void Start_LoadGame()
    {
        GameManager.Instance.SetState(eState.Calendar);
    }

    public void OnClick_Information()
    {
        // 설명 및 개발 정보 팝업 띄움
    }
}
