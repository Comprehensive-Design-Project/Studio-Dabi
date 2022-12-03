using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimingGameSystem : MonoBehaviour
{

    public float mainSliderSpeed = 0.1f;

    public Slider mainGameSlider;
    public Slider startValueSlider;
    public Slider endValueSlider;
    public TextMeshProUGUI scoreText;

    float startVal;
    float endVal;
    float valueRange;

    float currentValue;
    int howMuchTimeSpended = 0;
    bool isGameCompleted = false;

    //StartValueSlider : RightRoute, 0-100
    //EndValueSlider : ReverseRoute, 0-100

    //if need Range 10-30 then
    //StartValueSlider pos : 10
    //EndValueSlider pos : 100 - 30 = 70
    //then right range

    void OnEnable()
    {
        
        currentValue = 0f;
        howMuchTimeSpended = 0;
        startValueSlider.value = 0f;
        endValueSlider.value = 100f;
        Start();
    }

    // Start is called before the first frame update
    void Start()
    {
      startVal = Random.Range(0, 70f);
      endVal = startVal + Random.Range(20f, 30f);
      valueRange = endVal - startVal;
        startValueSlider.value = startVal;
        endValueSlider.value = 100f - endVal;
    }

    // Update is called once per frame
    void Update()
    {
        if (howMuchTimeSpended > 3000)
        {
            Debug.Log("Game Complete!");
            isGameCompleted = true;
            scoreText.color = Color.green;
        }

        if (!isGameCompleted)
        {
            if (currentValue == 0f || currentValue == 100f)
            {
                // caution! you are not playing the GAME!!!!
                // if you AFK then game result gonna be FAIL
            }

            mainGameSlider.value += mainSliderSpeed;
            currentValue = mainGameSlider.value;

            if (startVal < currentValue && currentValue < endVal)
            {
                howMuchTimeSpended++;
                scoreText.text = howMuchTimeSpended.ToString();
            }
        }




    }

    public void whenClick()
    {
        mainSliderSpeed *= -1f;
    }
}
