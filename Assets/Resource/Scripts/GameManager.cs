using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum eState
{
    Splash = 0,
    Start,
    Setting,
    Calendar,
    Story,
    Ending,
}

/// <summary>
/// 상태 관리를 주로 진행하는 매니저 클래스
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("상태 정보")]
    public eState previous_State;
    public eState m_State;
    public int error_code;

    [Header("설정 변수")]
    public bool isOpenSetting = false;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        Init();
    }

    public void Start()
    {
        SetState(eState.Splash);
    }

    public void Init()
    {
        Application.targetFrameRate = 60;
    }

    public void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("모든 데이터 삭제됨");
        }
#endif

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 나가기 팝업
        }

    }

    public void SetState(eState state, int day = 0)
    {
        previous_State = m_State;
        m_State = state;

        switch (state)
        {
            case eState.Splash:
                break;
            case eState.Start:
                break;
            case eState.Setting:
                isOpenSetting = true;
                break;
            case eState.Calendar:
                break;
            default:
                break;
        }

        if (day == 0)
        {
            return;
        }
        else
        {
            User_Info.Instance.nowDay = day;
            if (PlayerPrefs.GetInt("playDay") <= day)
            {
                User_Info.Instance.Set_Data("playDay", day);
            }
            else {}

            string scene_num = day.ToString();
            SceneManager.LoadSceneAsync("Story"+scene_num);
        }
    }
}