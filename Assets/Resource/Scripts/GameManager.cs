using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum eState
{
    Splash = 0,
    Start = 1,
    Setting = 3,
    Calendar = 2,
    Story = 4,
    Ending,
}

/// <summary>
/// 상태 관리를 주로 진행하는 매니저 클래스
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("상태 정보")]
    public eState previous_State;
    public eState m_State; // 현재 상태
    public int error_code;

    [Header("설정 변수")]
    public bool isOpenSetting = false;

    [Header("전역으로 사용되는 UI")]
    public GameObject popUp_Exit; // 종료하시겠습니까 팝업
    public GameObject panel_Loading; // 로딩 화면
    public GameObject BTN_Setting; // 설정 버튼
    public GameObject popUp_Setting; // 설정 팝업

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

        StartCoroutine(Coroutine_Loading(1.5f));

        switch (state)
        {
            case eState.Splash:
                break;
            case eState.Start:
                SceneManager.LoadSceneAsync("Start");
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
            // 현재 조회 중인 일수 대입
            User_Info.Instance.nowDay = day;
            // 저장된 일수보다 현재 이동한 스토리 일수가 더 크면 저장
            if (PlayerPrefs.GetInt("playDay") <= day)
            {
                User_Info.Instance.Set_Data("playDay", day);
            }
            else 
            {
                // 다시 보기 중
            }

            // 해당 일수의 스토리로 이동
            string scene_num = day.ToString();
            SceneManager.LoadSceneAsync("Story"+scene_num);
        }
    }

    IEnumerator Coroutine_Loading(float time)
    {
        panel_Loading.SetActive(true);
        yield return new WaitForSeconds(time);
        panel_Loading.SetActive(false);
    }
}