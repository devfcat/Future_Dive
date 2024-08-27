using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 소리 관련 설정을 담당하는 매니저 스크립트
/// </summary>
public class SoundManager : MonoBehaviour
{
    [Header("소리 설정 변수")]
    [Range(0f,1f)] public float BGM_volume;
    [Range(0f,1f)] public float SE_volume;
    [Range(0f,1f)] public float Voice_volume;

    [Header("소리 모음")]
    [Tooltip("배경음")] public List<AudioClip> BGMs;
    [Tooltip("효과음")] public List<AudioClip> SEs;
    [Tooltip("보이스")] public List<AudioClip> Voices;
    
    [Header("오디오 관련 오브젝트")]
    public AudioSource BGMPlayer;
    public AudioSource SEPlayer;
    public AudioSource VoicePlayer;

    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;

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

    public void Init()
    {
        BGMPlayer.clip = null;
        SEPlayer.clip = null;
        VoicePlayer.clip = null;
    }

    public void Load_Settings()
    {
        if (User_Info.Instance.isExistSaveFile == 0)
        {
            BGM_volume = 1f;
            SE_volume = 1f;
            Voice_volume = 1f;
        }
        else
        {
            BGM_volume = PlayerPrefs.GetFloat("BGM");
            SE_volume = PlayerPrefs.GetFloat("SE");
            Voice_volume = PlayerPrefs.GetFloat("Voice");
        }

        BGMPlayer.volume = BGM_volume;
        SEPlayer.volume = SE_volume;
        VoicePlayer.volume = Voice_volume;
    }

    public void Set_Volume_BGM(float volume)
    {
        BGMPlayer.volume = volume;
        PlayerPrefs.SetFloat("BGM", volume);
        PlayerPrefs.Save();
    }

    public void Set_Volume_SE(float volume)
    {
        SEPlayer.volume = volume;
        PlayerPrefs.SetFloat("SE", volume);
        PlayerPrefs.Save();
    }

    public void Set_Volume_Voice(float volume)
    {
        VoicePlayer.volume = volume;
        PlayerPrefs.SetFloat("Voice", volume);
        PlayerPrefs.Save();
    }
}
