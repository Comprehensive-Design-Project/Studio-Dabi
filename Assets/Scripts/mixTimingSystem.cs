using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class mixTimingSystem : MonoBehaviour
{
    public Slider slider;
    public Slider bottomSlider;
    public Slider topSlider;
    public GameObject gameAdmin;
    public RectTransform backGroundRotate;
    public Image correctPanel;
    public Image coolTimeIndicator;
    public float sliderSpeed = 0.01f;
    public float answerRangeStart = 0f;
    public float answerRangeEnd = 0f;
    public TextMeshProUGUI countText;
    

    bool isNeedReverse = false;
    bool canSubmitAnswer = true;
    int correctCount = 0;
    float imageAlphaValue = 0f;

    void OnEnable()
    {
        correctCount = 0;
        imageAlphaValue = 0f;
        Start();
    }



    // Start is called before the first frame update
    void Start()
    {
        correctCount = 0;
        randomAnswerGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        float angleDeterminedbySliderValue = (slider.value - 0.5f) * 100;

        backGroundRotate.rotation = Quaternion.Euler(0f, 0f, angleDeterminedbySliderValue);

        if(canSubmitAnswer)
        {
            coolTimeIndicator.color = Color.white;

        }
        else
        {
            coolTimeIndicator.color = Color.red;
            
        }

        if (correctPanel.color.a > 0f)
        {
            imageAlphaValue = correctPanel.color.a;
            imageAlphaValue -= 0.015f;
            correctPanel.color = new Color(correctPanel.color.r, correctPanel.color.g, correctPanel.color.b, imageAlphaValue);
        }

        if (correctCount >= 7)
        {
            RandomEvent.inst.isCorutineStart = false;
            RandomEvent.inst.EventCorutine();
            gameAdmin.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(slider.value >= answerRangeStart && slider.value <= answerRangeEnd)
            {
                correctMethod();
            }
            else
            {
                
            }
        }

        if(slider.value == 0f || slider.value == 1f)
        {
            canSubmitAnswer = true;
            isNeedReverse = !isNeedReverse;
            randomAnswerGenerator();
            sliderSpeed = Random.Range(0.0001f, 0.0005f);
        }
        
      
        if(isNeedReverse)
        {
            slider.value -= sliderSpeed;
        }
        else
        {
            slider.value += sliderSpeed;
        }
    }

    public void correctMethod()
    {
        correctCount++;
        canSubmitAnswer = false;
        correctPanel.color = new Color(correctPanel.color.r, correctPanel.color.g, correctPanel.color.b, 1f);
        countText.text = correctCount.ToString();


    }

    public void randomAnswerGenerator()
    {
        answerRangeStart = Random.Range(0.0f, 0.8f);
        answerRangeEnd = answerRangeStart + Random.Range(0.05f, 0.07f);

        bottomSlider.value = answerRangeStart;
        topSlider.value = 1f - answerRangeEnd;
    }
}
