using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvent : MonoBehaviour
{
    public GameObject player_light;
    public GameObject off_flahsEvent;
    public GameObject coffee_refine_event;
    public GameObject coffee_shake_event;
    public bool isTutoEnd = false;
    int eventID;
    float _timer;

    public bool isCorutineStart = false;
    
    public static RandomEvent inst { get; private set; }
    void Awake() => inst = this;

    private void Update()
    {
       if (FlashManager.FlashInstance.ChargeState() >= 95)
        {
            FlashManager.FlashInstance.battery = 0;
            off_flahsEvent.SetActive(false);
            player_light.SetActive(true);
            isCorutineStart = false;
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
        _timer = (float)Random.Range(20, 41);

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
        eventID = Random.Range(1,5);

        switch (eventID)
        {
            case 1:
                player_light.SetActive(true);
                FlashManager.FlashInstance.GaugeInit();
                player_light.GetComponent<Light>().LightCorutine();
                break;
            case 2:
                Player_Move_Draw.inst.DestroyLine();
                Player_Move_Draw.inst.canClick = false;
                Player_Move_Draw.inst.StopMove();
                coffee_refine_event.SetActive(true);
                break;
            case 3:
                Player_Move_Draw.inst.DestroyLine();
                Player_Move_Draw.inst.canClick = false;
                Player_Move_Draw.inst.StopMove();
                coffee_shake_event.SetActive(true);
                break;
            case 4:
                Player_Move_Draw.inst.DestroyLine();
                Player_Move_Draw.inst.canClick = false;
                Player_Move_Draw.inst.StopMove();
                BatManager.inst.InvokeGame();

                break;
            default:
                break;
        }
    }

}
