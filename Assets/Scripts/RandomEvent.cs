using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvent : MonoBehaviour
{
    public GameObject player_light;
    int eventID;
    float _timer;

    public static RandomEvent inst { get; private set; }
    void Awake() => inst = this;

    private void Start()
    {
        //코루틴 1회 시작
        StartCoroutine(EventTimer());
    }

    public void EventCorutine()
    {
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
                Debug.Log("손전등 이벤트 발생");
                player_light.SetActive(true);
                player_light.GetComponent<Light>().LightCorutine();
                break;
            case 2:
                Player_Move_Draw.inst.StopMove();
                Debug.Log("2번 이벤트 발생");
                StartCoroutine(EventTimer());
                break;
            case 3:
                Player_Move_Draw.inst.StopMove();
                Debug.Log("3번 이벤트 발생");
                StartCoroutine(EventTimer());
                break;
            default:
                break;
        }
    }

}
