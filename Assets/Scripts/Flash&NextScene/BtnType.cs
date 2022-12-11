using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnType : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public FlashType currentType;
    private bool currentVisted;
  //  private float number = 0;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        currentVisted = FlashManager.FlashInstance.VisitedState();
        switch (currentType)
        {
            case FlashType.Bottom:
                //Debug.Log("�Ʒ�");
                if (currentVisted)
                {
                    FlashManager.FlashInstance.Charging();
                    FlashManager.FlashInstance.Visited();
                }
                break;
            case FlashType.Top:
                //Debug.Log("��");
                if (!currentVisted)
                {
                    FlashManager.FlashInstance.Charging();
                    FlashManager.FlashInstance.Visited();
                }
                break;
            case FlashType.Exit:
                //Debug.Log("Ż��");
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log(FlashManager.FlashInstance.ChargeState());
    }
}
