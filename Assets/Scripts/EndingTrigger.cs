using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            if(RandomEvent.inst.isTutoEnd)
            {
                //엔딩으로
                Debug.Log("엔딩으로 갑니다.");
            }
        }
    }
}
