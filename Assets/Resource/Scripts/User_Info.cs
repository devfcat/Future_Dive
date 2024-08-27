using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

/// <summary>
/// 유저 플레이 정보를 담당하는 클래스
/// </summary>
public class User_Info : MonoBehaviour
{
    [Header("플레이어 정보")]
    [ReadOnly] public string UserName = "";
    [ReadOnly] public int nowDay;

    [Header("플레이 스탯")]
    [ReadOnly] public int state_like;
    [ReadOnly] public int state_truth;
    [ReadOnly] public int state_asmodeus;
    
    [Header("세이브 내역")]
    [ReadOnly] public int isExistSaveFile = 0;
    [ReadOnly] public int playDay; // 활성화된 일수
    [ReadOnly] public int saved_like;
    [ReadOnly] public int saved_truth;
    [ReadOnly] public int saved_asmodeus;
    [ReadOnly] public int isOpenHE = 0;
    [ReadOnly] public int isOpenBE = 0;
    [ReadOnly] public int isOpenFE = 0;

    [Header("일러스트 해금 내역")]
    public int illust_size;
    [ReadOnly] public int isOpen_illust_00;
    [ReadOnly] public int isOpen_illust_01;
    [ReadOnly] public int isOpen_illust_02;
    [ReadOnly] public int isOpen_illust_03;
    [ReadOnly] public int isOpen_illust_04;
    [ReadOnly] public int isOpen_illust_05;
    [ReadOnly] public int isOpen_illust_06;


    private static User_Info instance;
    public static User_Info Instance
    {
        get {
            if(!instance)
            {
                instance = FindObjectOfType(typeof(User_Info)) as User_Info;

                if (instance == null)
                    Debug.Log("no Singleton obj");
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        Get_Data();
    }

    // 저장되어 있는 데이터들을 가져옴
    public void Get_Data()
    {
        try
        {
            isExistSaveFile = PlayerPrefs.GetInt("isExistSaveFile");
        }
        catch 
        {
            isExistSaveFile = 0;
        }

        if (isExistSaveFile > 0)
        {
            try
            {
                playDay = PlayerPrefs.GetInt("playDay");

                saved_like = PlayerPrefs.GetInt("saved_like");
                saved_truth = PlayerPrefs.GetInt("saved_truth");
                saved_asmodeus = PlayerPrefs.GetInt("saved_asmodeus");
            }
            catch
            {
                isExistSaveFile = 0;
            }
        }
        else
        {
            // 세이브 없음
        }

        try
        {
            UserName = PlayerPrefs.GetString("UserName"); // 유저 이름은 내 프로필 창에서 변경가능하도록
        }
        catch
        {
            UserName = "";
        }

        try
        {
            isOpenHE = PlayerPrefs.GetInt("isOpenHE");
            isOpenFE = PlayerPrefs.GetInt("isOpenFE");
            isOpenBE = PlayerPrefs.GetInt("isOpenBE");
        }
        catch
        {

        }

        isOpen_illust_00 = PlayerPrefs.GetInt("isOpen_illust_00");


        // 소리 설정 가져옴
        SoundManager.Instance.Load_Settings();
    }

    public void Set_Data(string dataName, int data)
    {
        PlayerPrefs.SetInt(dataName, data);

        if (dataName == "playDay")
        {
            PlayerPrefs.SetInt("isExistSaveFile", 1);
        }

        PlayerPrefs.Save();

        Get_Data();
    }

    public void Set_Name(string name)
    {
        PlayerPrefs.SetString("UserName", name);
        PlayerPrefs.Save();

        Get_Data();
    }

    public void Reset_Data()
    {
        if (isExistSaveFile > 0)
        {
            PlayerPrefs.DeleteKey("playDay");
            PlayerPrefs.DeleteKey("saved_like");
            PlayerPrefs.DeleteKey("saved_truth");
            PlayerPrefs.DeleteKey("saved_asmodeus");

            PlayerPrefs.SetInt("isExistSaveFile", 0);
            PlayerPrefs.Save();
        }
        else
        {
            // 이미 세이브파일이 없음
        }

        Get_Data();
    }
}
