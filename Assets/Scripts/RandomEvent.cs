using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvent : MonoBehaviour
{
    public GameObject player_light;
    public GameObject flash_event;
    public GameObject coffee_event;
    int eventID;
    float _timer;
    float flash_battery;
    bool isCorutineStart = false;

    public static RandomEvent inst { get; private set; }
    void Awake() => inst = this;

    private void Start()
    {
        StartCoroutine(EventTimer());
    }
    private void Update()
    {
        flash_battery = FlashManager.FlashInstance.ChargeState();
        if(flash_battery==100)
            flash_event.SetActive(false);
    }

    public void EventCorutine()
    {
        if (isCorutineStart == false)
        {
            Player_Move_Draw.inst.canClick = true;
            StartCoroutine(EventTimer());
        }
        
    }

    public IEnumerator EventTimer()
    {
        isCorutineStart = true;
        _timer = (float)Random.Range(5, 11);

        while(_timer > 0)
        {
            _timer -= Time.deltaTime;
            yield return null;
        }

        if(_timer <= 0)
        {
            isCorutineStart = false;
            CallRandomEvent();
        }
    }

    void CallRandomEvent()
    {
        eventID = Random.Range(1,4);
        
        switch (eventID)
        {
            case 1:
                Debug.Log("랜턴 이벤트 발생");
                player_light.SetActive(true);
                player_light.GetComponent<Light>().LightCorutine();
                flash_event.SetActive(true);
                break;
            case 2:
                Player_Move_Draw.inst.DestroyLine();
                Player_Move_Draw.inst.canClick = false;
                Player_Move_Draw.inst.StopMove();
                Debug.Log("2번 이벤트 발생");
                //coffee_event.SetActive(true);
                //need blocking Click while Coffee Game playing
                EventCorutine();
                break;
            case 3:
                Player_Move_Draw.inst.DestroyLine();
                Player_Move_Draw.inst.canClick = false;
                Player_Move_Draw.inst.StopMove();
                Debug.Log("3번 이벤트 발생");
                EventCorutine();
                break;
            default:
                break;
        }
    }

}
