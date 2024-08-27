using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Collections;
using UnityEngine;

/// <summary>
/// 대사 한 묶음
/// </summary>
[Serializable]
public class Dialogue
{
    [SerializeField] public string name_ch;
    [SerializeField] public string dialogue;
}

/// <summary>
/// 한 챕터의 전체 대사
/// </summary>
[Serializable]
public class Chapters
{
    [SerializeField] public string name_Chapter;
    [SerializeField] public List<Dialogue> dialogues;
}

/// <summary>
/// 대사 데이터에서 특정 대사를 가져올 수 있도록 하는 매니저 클래스
/// </summary>
public class Dialogue_Manager : MonoBehaviour
{
    [Tooltip("로딩중인가")] public bool isLoading = false;
    [Tooltip("임시저장된 대사 데이터")] public List<Dialogue> Temp_Dialogue;
    [ReadOnly] public string Default_PlayerName = "";
    
    private static Dialogue_Manager instance;
    public static Dialogue_Manager Instance
    {
        get {
            if(!instance)
            {
                instance = FindObjectOfType(typeof(Dialogue_Manager)) as Dialogue_Manager;

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

    public void Start()
    {
#if UNITY_EDITOR
        Test();
#endif
    }

    public void Test()
    {
        Load_Dialogue(0);
    }

    /// <summary>
    /// 어떠한 챕터의 대사집을 불러옴
    /// </summary>
    /// <param name="chapter">챕터 번호</param>
    public void Load_Dialogue(int chapter)
    {
        StartCoroutine(Coroutine_LoadChapterDialogue(chapter));
    }

    IEnumerator Coroutine_LoadChapterDialogue(int chapter)
    {
        isLoading = true;

        Clear_DialogueData();

        string filepath = "Assets/Resource/Dialogue/" + chapter.ToString() + ".json";
        string jsonContents = File.ReadAllText(filepath);
        Chapters content = JsonUtility.FromJson<Chapters>(jsonContents.Trim());

        for (int i = 0; i < content.dialogues.Count; i++)
        {
            Temp_Dialogue.Add(content.dialogues[i] as Dialogue);
#if UNITY_EDITOR
            Debug.Log(Temp_Dialogue[i].name_ch + ": " + Temp_Dialogue[i].dialogue);
#endif
        }
        
        isLoading = false;
        // Debug.Log("데이터 로드 완료");

        yield return null;
    }

    /// <summary>
    /// 임시 저장된 대사집 초기화
    /// </summary>
    public void Clear_DialogueData()
    {
        try
        {
            Temp_Dialogue.Clear();
        }
        catch
        {

        }
    }

    /// <summary>
    /// 임시 저장된 대사집에서 index번 대사를 반환
    /// </summary>
    /// <param name="index">대사 순서</param>
    /// <returns></returns>
    public string Get_A_Dialogue(int index)
    {
        return Temp_Dialogue[index].dialogue;
    }

    /// <summary>
    /// 임시 저장된 대사집에서 index번 대사의 발화자의 이름 반환
    /// </summary>
    /// <param name="index">이름</param>
    /// <returns></returns>
    public string Get_A_Name(int index)
    {
        return Temp_Dialogue[index].name_ch;
    }
}
