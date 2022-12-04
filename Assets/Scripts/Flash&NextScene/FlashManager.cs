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
    private bool isVisted; // 위에 방문했을 땐 true, 아래 방문했을 땐 false를 반환
    private double battery; // 배터리 양 

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

    public bool VisitedState() // 가장 최근에 방문한 버튼의 위치를 알기 위함
    {
        return isVisted;
    }

    public double ChargeState() // 현재 충전된 정도를 알기 위함
    {
        return battery;
    }

    public void Charging() // 배터리 양 1% 증가
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
