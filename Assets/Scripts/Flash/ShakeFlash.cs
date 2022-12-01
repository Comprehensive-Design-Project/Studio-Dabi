using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeFlash : MonoBehaviour
{
    private float shakeTime;
    private float shakeIntensity;
    private float shakeCnt=0;
    private float currentCnt=1;

    void Start()
    {
        
    }

  
    void Update()
    {
        shakeCnt = FlashManager.FlashInstance.ChargeState();
        if(shakeCnt%4==0 && currentCnt!=shakeCnt)
            OnShakeFlash(1f, 0.1f);
        currentCnt = shakeCnt;
        if (Input.GetKeyDown("1"))
            OnShakeFlash(1f, 0.1f);
    }

    public void OnShakeFlash(float shakeTime=1.0f, float shakeIntensity=0.1f)
    {
        this.shakeTime = shakeTime;
        this.shakeIntensity = shakeIntensity;
        StopCoroutine("ShakeByRotation");
        StartCoroutine("ShakeByRotation");
    }

    private IEnumerator ShakeByRotation()
    {
        Vector3 startRotation = transform.eulerAngles;
        float power = 10f;
        while(shakeTime > 0.0f)
        {
            float x = 0;
            float y = 0;
            float z = Random.Range(-1f, 0f);
            transform.rotation = Quaternion.Euler(startRotation + new Vector3(x, y, z) * shakeIntensity * power);
            shakeTime -= Time.deltaTime;
            yield return null;
        }
        transform.rotation = Quaternion.Euler(startRotation);
    }

}
