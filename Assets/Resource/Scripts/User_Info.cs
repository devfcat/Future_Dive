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
    [ReadOnly] public int playDay;
    [ReadOnly] public int saved_like;
    [ReadOnly] public int saved_truth;
    [ReadOnly] public int saved_asmodeus;
    [ReadOnly] public int isOpenHE = 0;
    [ReadOnly] public int isOpenBE = 0;
    [ReadOnly] public int isOpenFE = 0;
    // [ReadOnly] public List<int> unLockedIllust;

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
    }

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

                isOpenHE = PlayerPrefs.GetInt("isOpenHE");
                isOpenFE = PlayerPrefs.GetInt("isOpenFE");
                isOpenBE = PlayerPrefs.GetInt("isOpenBE");
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
    }

    public void Set_Data(string dataName, int data)
    {
        PlayerPrefs.SetInt(dataName, data);
        PlayerPrefs.SetInt("isExistSaveFile", 1);

        PlayerPrefs.Save();
    }

    public void Reset_Data()
    {
        Get_Data();

        if (isExistSaveFile > 0)
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
