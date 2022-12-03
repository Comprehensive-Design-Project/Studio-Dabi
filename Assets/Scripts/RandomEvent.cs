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
    bool isCoffeeMaded = false;

    public static RandomEvent inst { get; private set; }
    void Awake() => inst = this;

    private void Start()
    {
        //?????? 1?? ????
        StartCoroutine(EventTimer());
    }
    private void Update()
    {
        isCoffeeMaded = CoffeeGameSystem.isGameEnd;
        flash_battery = FlashManager.FlashInstance.ChargeState();
        if(flash_battery==100)
            flash_event.SetActive(false);
        
        if (isCoffeeMaded)
            coffee_event.SetActive(false);
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
                Debug.Log("?????? ?????? ????");
                player_light.SetActive(true);
                player_light.GetComponent<Light>().LightCorutine();
                flash_event.SetActive(true);
                break;
            case 2:
                Player_Move_Draw.inst.StopMove();
                Debug.Log("2?? ?????? ????");
                coffee_event.SetActive(true);
                //need blocking Click while Coffee Game playing
                StartCoroutine(EventTimer());
                break;
            case 3:
                Player_Move_Draw.inst.StopMove();
                BatManager.inst.GameStart();
                Debug.Log("3?? ?????? ????");
                StartCoroutine(EventTimer());
                break;
            default:
                break;
        }
    }

}
