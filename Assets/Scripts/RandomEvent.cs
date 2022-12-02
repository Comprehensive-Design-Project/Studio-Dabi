using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvent : MonoBehaviour
{
    int eventID;

    void CallRandomEvent()
    {
        eventID = Random.Range(1,4);
        switch (eventID)
        {
            case 1:
                Debug.Log("1번 이벤트 발생");
                break;
            case 2:
                Debug.Log("2번 이벤트 발생");
                break;
            case 3:
                Debug.Log("3번 이벤트 발생");
                break;
        }
    }

}
