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
                Debug.Log("1�� �̺�Ʈ �߻�");
                break;
            case 2:
                Debug.Log("2�� �̺�Ʈ �߻�");
                break;
            case 3:
                Debug.Log("3�� �̺�Ʈ �߻�");
                break;
        }
    }

}
