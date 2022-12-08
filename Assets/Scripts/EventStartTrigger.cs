using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventStartTrigger : MonoBehaviour
{
    [SerializeField]
    public GameObject rock_ending;
    [SerializeField]
    public GameObject rock_blocking;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            if(!RandomEvent.inst.isTutoEnd)
            {
                RandomEvent.inst.isTutoEnd = true;
                rock_ending.SetActive(false);
                rock_blocking.SetActive(true);
                StartCoroutine(RandomEvent.inst.EventTimer());
            }
        }
    }
}
