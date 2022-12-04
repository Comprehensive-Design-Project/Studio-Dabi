using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashBattery : MonoBehaviour
{
    [SerializeField] private Slider BatterGauge;

    private double gaugeMax = 100;
    private double curGauge;
    
    void Start()
    {
        curGauge = 0;
    }

    // Update is called once per frame
    void Update()
    {
        FillGauge();
    }

    private void FillGauge()
    {
        curGauge = FlashManager.FlashInstance.ChargeState();
        BatterGauge.value = (float)curGauge / (float)gaugeMax;
    }
}
