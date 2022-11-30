using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoffeeGameSystem : MonoBehaviour
{
    // Make 6 Color
    // Red, Orange, Yellow, Green, Blue, Purple
    [SerializeField]
    public Color redColor = new Color();
    public Color orangeColor = new Color();
    public Color yellowColor = new Color();
    public Color greenColor = new Color();
    public Color blueColor = new Color();
    public Color purpleColor = new Color();

    public GameObject[] patternArray = new GameObject[7];
    public Color[] colorArray = new Color[7];
    public Color[] BWArray = new Color[2];
    public Image[] sliderBackGroundArray = new Image[6];
    public Color[] tempColorArray = new Color[7];
    int selector = 0;
    int answerSelector = 0;
    public Slider _testSlider;

    public GameObject answerPatternTile;

    // Start is called before the first frame update
    void Start()
    {
        redColor = Color.red;
        orangeColor = new Color(1, 0.5f, 0, 1);
        yellowColor = Color.yellow;
        greenColor = Color.green;
        blueColor = Color.blue;
        purpleColor = Color.magenta;

        BWArray[0] = Color.black;
        BWArray[1] = Color.white;

        // colorArray는 읽기 전용이어야하므로 필요한경우 새로운 배열에 복사해서 사용 해야한다
        colorArray[0] = Color.red;
        colorArray[1] = new Color(1, 0.5f, 0, 1);
        colorArray[2] = Color.yellow;
        colorArray[3] = Color.green;
        colorArray[4] = Color.blue;
        colorArray[5] = Color.magenta;

        patternGenerate();
       // sliderColorGenarate();
    }

    // 플레이어가 수행해야 할 타일 패턴 7개를 정의하는 메소드이다
    public void patternGenerate()
    {
        for (int i = 0; i < 7; i++)
        {
            patternArray[i].GetComponent<RawImage>().color = colorArray[Random.Range(0, 6)];
            patternArray[i].GetComponent<Outline>().effectColor = BWArray[Random.Range(0, 2)];

        }
    }

    // 윷놀이 보드 모양으로 배치된 슬라이더의 색을 배치하는 메소드이다
    public void sliderColorGenarate()
    {
        for(int i = 0; i <= 6; i++)
        {
            tempColorArray[i] = colorArray[i];
        }

        while(true)
        {
            bool isComplete = false;

            for(int i=0; i<7; i++)
            {
                isComplete = true;
                if(tempColorArray[i] != Color.black)
                {
                    isComplete = false;
                }
            }

            if (isComplete)
            {
                selector = 0;
                break;
            }

            int randomIndex = Random.Range(0, 6);

            if(tempColorArray[randomIndex] != Color.black)
            {
                Color targetColor = tempColorArray[randomIndex];
                tempColorArray[randomIndex] = Color.black;
                sliderBackGroundArray[selector].color = targetColor;
                selector++;
            }
            else
            {
                continue;
            }


        }


        // get new color array copy from new array
        // random apply to slider fill
            // apply one color, and blank color in array
        // if detect blank color, re-random
        // if new color array dosen't have any black, can go
    }

    void Update()
    {
        _testSlider.value += 0.001f;
        if (_testSlider.value >= 0.99f)
        {
            _testSlider.value = 0f;
            answerPatternTile.GetComponent<RawImage>().color = colorArray[Random.Range(0, 6)];
        }

        // space를 누르면 채점을 시작한다
        if(Input.GetKeyDown(KeyCode.Space))
        {
            scoring();
        }
    }

    public void scoring()
    {
        Color BWAnswer = patternArray[answerSelector].GetComponent<Outline>().effectColor;
        Color insideAnswer = patternArray[answerSelector].GetComponent<RawImage>().color;
        Color BWSelection = answerPatternTile.GetComponent<Outline>().effectColor;
        Color insideSelection = answerPatternTile.GetComponent<RawImage>().color;

        if(BWAnswer == BWSelection && insideAnswer == insideSelection)
        {

            patternArray[answerSelector].SetActive(false);
            answerSelector++;
        }
        else
        {
            Debug.Log("Wrong Answer");
        }

        if (answerSelector >= 7) Debug.Log("Good!! Reward Here!!");

        //틀렸을 때의 누적 카운트도 셀 수 있도록 만들 생각이다

        
    }
}
