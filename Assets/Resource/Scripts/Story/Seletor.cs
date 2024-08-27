using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 선택지 말풍선에 달린 선택지
/// </summary>
public class Seletor : MonoBehaviour
{
    public int next_num = -1;
 
    public void Onclick()
    {
        if (next_num >= 0)
        {
           StoryManager.Instance.Next_Bubble(next_num); 
        }
    }
}
