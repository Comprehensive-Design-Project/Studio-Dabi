using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventStartTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "player")
        {
            if(!RandomEvent.inst.isTutoEnd)
            {
                RandomEvent.inst.isTutoEnd = true;
                //바위로 지나온 길을 막고 시작지점으로 돌아오는 길을 열어줌
                //Object를 setActive(false)를 해줌
                //이벤트 코루틴 시작
            }
        }
    }
}
