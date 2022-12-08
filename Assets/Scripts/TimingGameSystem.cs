using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimingGameSystem : MonoBehaviour
{

    public float mainSliderSpeed = 0.1f;
    public int goalScore = 1500;

    public Slider mainGameSlider;
    public Slider startValueSlider;
    public Slider endValueSlider;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI indicateScoreText;
    public GameObject TimingGameAdmin;

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
        goalScore = Random.Range(1500, 2500);
        indicateScoreText.text = "/" + goalScore.ToString();
        Start();
    }

    // Start is called before the first frame update
    void Start()
    {
      startVal = Random.Range(0, 60f);
      endVal = startVal + Random.Range(30f, 40f);
      valueRange = endVal - startVal;
        startValueSlider.value = startVal;
        endValueSlider.value = 100f - endVal;
    }

    // Update is called once per frame
    void Update()
    {
        if (howMuchTimeSpended >= goalScore)
        {
            Debug.Log("Game Complete!");
            isGameCompleted = true;
            scoreText.color = Color.green;

            RandomEvent.inst.isCorutineStart = false;
            RandomEvent.inst.EventCorutine();

            TimingGameAdmin.SetActive(false);
            // need : setActive false after 3 seconds
            return;
        }

        if (!isGameCompleted)
        {
            if (currentValue == 0f || currentValue == 100f)
            {
                // caution! you are not playing the GAME!
                // if you AFK then game result gonna be FAIL
            }

            mainGameSlider.value += mainSliderSpeed;
            currentValue = mainGameSlider.value;

            if (startVal < currentValue && currentValue < endVal)
            {
                howMuchTimeSpended++;
                scoreText.text = howMuchTimeSpended.ToString();
            }
            else
            {
                howMuchTimeSpended = 0;
            }

            //make red-text animation when scores going 0
        }
            




    }

    public void whenClick()
    {

        // when speed has selected, value is vaild when only one invert
        if(mainSliderSpeed > 0f)
        {
            mainSliderSpeed *= -1f;
        }
        else
        {
            mainSliderSpeed = Random.Range(0.1f, 0.2f);
        }

        
    }
}
