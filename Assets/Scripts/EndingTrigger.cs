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
                //��������
                Debug.Log("�������� ���ϴ�.");
            }
        }
    }
}
