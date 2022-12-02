using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvent : MonoBehaviour
{
    int eventID;
    float _timer;

    public static RandomEvent inst { get; private set; }
    private void Start()
    {
        //�ڷ�ƾ 1ȸ ����
        StartCoroutine(EventTimer());
    }

    public IEnumerator EventTimer()
    {
        _timer = (float)Random.Range(5, 11);

        while(_timer > 0)
        {
            _timer -= Time.deltaTime;
            yield return null;
        }

        if(_timer <= 0)
        {
            CallRandomEvent();
        }
    }
    void CallRandomEvent()
    {
        eventID = Random.Range(1,4);
        switch (eventID)
        {
            case 1:
                Debug.Log("1�� �̺�Ʈ �߻�");
                StartCoroutine(EventTimer());
                break;
            case 2:
                Debug.Log("2�� �̺�Ʈ �߻�");
                StartCoroutine(EventTimer());
                break;
            case 3:
                Debug.Log("3�� �̺�Ʈ �߻�");
                StartCoroutine(EventTimer());
                break;
            default:
                break;
        }
    }

}
