using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            if(RandomEvent.inst.isTutoEnd)
            {
                //엔딩으로
            }
        }
    }
}
