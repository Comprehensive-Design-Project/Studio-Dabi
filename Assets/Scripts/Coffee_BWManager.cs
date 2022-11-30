using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coffee_BWManager : MonoBehaviour
{
    public Slider BWslider;
    public Button BWbutton;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BWslider.value -= 0.0001f;
        if(BWslider.value >= 0.5f)
        {
            BWbutton.GetComponent<Image>().color = Color.black;
        }
        else
        {
            BWbutton.GetComponent<Image>().color = Color.white;
        }
    }

    public void getUP()
    {
        BWslider.value += 0.05f;
    }
}
