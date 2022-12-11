using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            if(RandomEvent.inst.isTutoEnd)
            {
                SceneManager.LoadScene("Ending");
            }
        }
    }
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space)) // ġƮŰ
        {
            SceneManager.LoadScene("Ending");
        }*/
    }
}
