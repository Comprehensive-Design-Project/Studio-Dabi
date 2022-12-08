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
    public double battery; // ���͸� �� 

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

    public double ChargeState() // ���� ������ ������ �˱� ����
    {
        return battery;
    }

    public void Charging() // ���͸� �� 1% ����
    {
        if (battery < 100)
            battery += 5;
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
    
    public void GaugeInit()
    {
        battery = 0;
    }
    public void GaugeDown()
    {
        //battery -= 0.5;
       // if (battery > 0)
            //Invoke("GaugeDown", 0.1f);
    }
    void Start()
    {
        
    }
    void Update()
    {

    }
}
