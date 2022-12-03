using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Text : MonoBehaviour
{
    private bool firtstText=true;
    private bool secondText=false;
    private bool thirdText=false;

    public GameObject totalObject;
    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;

    private void Awake()
    {
        Text2.SetActive(false);
        Text3.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (thirdText)
            {
                thirdText = false;
                Text3.SetActive(false);
                Destroy(totalObject);
            }
            if (secondText)
            {
                secondText = false;
                thirdText = true;
                Text2.SetActive(false);
                Text3.SetActive(true);
            }
            if (firtstText)
            {
                firtstText = false;
                secondText = true;
                Text1.SetActive(false);
                Text2.SetActive(true);
            }
            
        }
    }
}
