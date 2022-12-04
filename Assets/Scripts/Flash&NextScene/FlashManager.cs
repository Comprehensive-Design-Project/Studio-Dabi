using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashManager : MonoBehaviour
{
  
    private static FlashManager flashInstance;
    public GameObject light_evented;
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
    private double battery; // ���͸� �� 

    private void Awake()
    {
        battery = 100;
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

    public void GaugeDown()
    {
        battery -= 0.5;
        if (battery > 0)
            Invoke("GaugeDown", 0.1f);
    }
    void Start()
    {
        
    }
    void Update()
    {
        if (battery <= 0)
            light_evented.SetActive(true);
        else if (battery >= 100)
        {
            light_evented.SetActive(false);
            GaugeDown();
        }
           
    }
}
