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
                //������ ������ ���� ���� ������������ ���ƿ��� ���� ������
                //Object�� setActive(false)�� ����
                //�̺�Ʈ �ڷ�ƾ ����
            }
        }
    }
}
