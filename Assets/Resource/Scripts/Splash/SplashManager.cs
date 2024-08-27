using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class SplashManager : MonoBehaviour
{
    public GameObject splash;

    void Start()
    {
        splash.SetActive(true);
        StartCoroutine(Coroutine_Starting(3f));
    }

    IEnumerator Coroutine_Starting(float time)
    {
        yield return new WaitForSeconds(time);
        GameManager.Instance.SetState(eState.Start);
    }
}
