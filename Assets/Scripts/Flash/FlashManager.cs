using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashManager : MonoBehaviour
{
  
    private static FlashManager flashInstance;
    public static FlashManager FlashInstance {
        get
        {
            if (null == flashInstance)
            {
                return null;
            }
            return flashInstance;
        }
    }
    private bool isVisted; // ���� �湮���� �� true, �Ʒ� �湮���� �� false�� ��ȯ
    private float battery; // ���͸� �� 

    private void Awake()
    {
        battery = 0;
        isVisted = false;
        if (flashInstance == null)
        {
            flashInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public bool VisitedState() // ���� �ֱٿ� �湮�� ��ư�� ��ġ�� �˱� ����
    {
        return isVisted;
    }

    public float ChargeState() // ���� ������ ������ �˱� ����
    {
        return battery;
    }

    public void Charging() // ���͸� �� 1% ����
    {
        if (battery < 100)
            battery += 1;
    }

    public void Visited()
    {
        if (isVisted)
        {
            isVisted = false;
            return;
        }
        else
            isVisted = true;
    }

    void Start()
    {
        
    }
    void Update()
    {
       
    }
}
