using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvent : MonoBehaviour
{
    public GameObject player_light;
    public GameObject off_flahsEvent;
    public GameObject coffee_event;
    public GameObject timing_event;
    int eventID;
    float _timer;

    bool isCorutineStart = false;
    
    public static RandomEvent inst { get; private set; }
    void Awake() => inst = this;

    private void Start()
    {
        StartCoroutine(EventTimer());
    }
    private void Update()
    {
       if (FlashManager.FlashInstance.ChargeState() >= 95)
        {
            off_flahsEvent.SetActive(false);
            player_light.SetActive(true);
            EventCorutine();
        }
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
                player_light.SetActive(true);
                FlashManager.FlashInstance.GaugeInit(); // battery gauge?? ???? 0???? ??????
                player_light.GetComponent<Light>().LightCorutine();
                break;
            case 2:
                Player_Move_Draw.inst.DestroyLine();
                Player_Move_Draw.inst.canClick = false;
                Player_Move_Draw.inst.StopMove();
                coffee_event.SetActive(true);

                //EventCorutine();
                break;
            case 3:
                Player_Move_Draw.inst.DestroyLine();
                Player_Move_Draw.inst.canClick = false;
                Player_Move_Draw.inst.StopMove();

                timing_event.SetActive(true);
                //EventCorutine();
                break;
            default:
                break;
        }
    }

}
