using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoffeeGameSystem : MonoBehaviour
{
    public float BlackWhiteDecreaseValue = 0.0005f;
    public float BlackWhiteIncreaseValue = 0.1f;
    public float ColorBarSpeedValue = 0.001f;
    public GameObject CoffeeSwitch; 
   
    public static bool isGameEnd = false;
    // Using 6 colors : Red, Orange, Yellow, Green, Blue, Purple
    public GameObject[] patternArray = new GameObject[7];
    public Color[] colorArray = new Color[6];
    public Color[] BWArray = new Color[2];
    public Image[] sliderBackGroundArray = new Image[6];
    public Color[] tempColorArray = new Color[6];
    public Slider[, ] slider_2DArray = new Slider[4, 4];
    public int[,] destArray = new int[4, 2];

    int selector = 0;
    int answerSelector = 0;
    int contents = 0;
    bool isGameCompleted = false;
    int runnerStarter = 0;
    int runnerDestination = 1;

    public Slider BWslider;
    public GameObject answerPatternTile;
    public Color playerColorSelection;

    // Start is called before the first frame update

    void OnEnable()
    {
        //Game Initialize
        isGameCompleted = false;
        isGameEnd = false;
        answerSelector = 0;
        Start();
    }

    void Start()
    {
        BWArray[0] = Color.black;
        BWArray[1] = Color.white;

        // colorArray는 읽기 전용이어야하므로 필요한경우 새로운 배열에 복사해서 사용 해야한다
        colorArray[0] = Color.red;
        colorArray[1] = new Color(1, 0.5f, 0, 1);
        colorArray[2] = Color.yellow;
        colorArray[3] = Color.green;
        colorArray[4] = Color.blue;
        colorArray[5] = Color.magenta;

       SliderInitialize();
       patternGenerate();
       sliderColorGenarate();
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
        // colorArray를 복사한다
        // tempColorArray에 중복이 발생하지 않도록 준비하는 과정 중 하나이다
        tempColorArray = (Color[])colorArray.Clone();

        // tempColorArray의 색을 하나 뽑아서 사용하는데 black으로 이미 사용 중임을 표기한다
        // 모든 색이 소모 될 때 까지 반복하도록 한다
        while(true)
        {
            bool isComplete = false;

            for(int i=0; i<6; i++)
            {
                isComplete = true;
                if(tempColorArray[i] != Color.black)
                {
                    isComplete = false;
                    break;
                }
            }

            if (isComplete)
            {
                selector = 0;
                return;
            }

            int randomIndex = Random.Range(0, 6);

            if(tempColorArray[randomIndex] != Color.black)
            {
                sliderBackGroundArray[selector].color = tempColorArray[randomIndex];
                tempColorArray[randomIndex] = Color.black;
                selector++;
            }
            else
            {
                continue;
            }
        }
    }

    public void SliderInitialize()
    {
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 2; j++)
            {
                destArray[i,j] = -1;
            }
        }

        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                slider_2DArray[i, j] = null;
            }
        }

        destArray[0, 0] = 1;
        destArray[1, 0] = 2;
        destArray[1, 1] = 3;
        destArray[2, 0] = 0;
        destArray[2, 1] = 3;
        destArray[3, 0] = 0;

        slider_2DArray[0, 1] = GameObject.Find("Slider0").GetComponent<Slider>();
        slider_2DArray[1, 2] = GameObject.Find("Slider2").GetComponent<Slider>();
        slider_2DArray[1, 3] = GameObject.Find("Slider1").GetComponent<Slider>();
        slider_2DArray[2, 0] = GameObject.Find("Slider3").GetComponent<Slider>();
        slider_2DArray[2, 3] = GameObject.Find("Slider4").GetComponent<Slider>();
        slider_2DArray[3, 0] = GameObject.Find("Slider5").GetComponent<Slider>();
        Debug.Log("Initiaize Completed!");
    }

    void Update()
    {

        BWslider.value -= BlackWhiteDecreaseValue;
        if(BWslider.value >= 0.5f)
        {
            answerPatternTile.GetComponent<Outline>().effectColor = Color.white;
        }
        else
        {
            answerPatternTile.GetComponent<Outline>().effectColor = Color.black;
        }
        // 아무키나 누르면 채점을 시작 한다. 마우스 클릭 빼고!
        if (Input.anyKeyDown)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
                return;

            scoring();
        }

        if(!isGameCompleted)
        {
            sliderRunner();
        }

    }

    public void getUP()
    {
        BWslider.value += BlackWhiteIncreaseValue;
    }


    public void sliderRunner()
    {
        if (isGameCompleted) return;


        Slider targetSlider = slider_2DArray[runnerStarter, runnerDestination];
        answerPatternTile.GetComponent<RawImage>().color = targetSlider.gameObject.transform.Find("Fill Area/Fill").GetComponent<Image>().color;
        
        if (targetSlider.value < 1f)
        {
            targetSlider.value += ColorBarSpeedValue;
            
        }
        else
        {

            targetSlider.value = 0f;
            Debug.Log(targetSlider.name + "의 작동을 마쳤습니다 ");
            runnerStarter = runnerDestination;
            runnerDestination = Shake(runnerStarter);
            Debug.Log(slider_2DArray[runnerStarter, runnerDestination].name + "의 작동 시작 ");
            sliderRunner();
        }
    }

    public int Shake(int start)
    {
        while (true)
        {
            int r = Random.Range(0, 2);
            if (destArray[start, r] != -1)
            {
                int contents = destArray[start, r];
                return contents;
            }
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
            //틀렸을 때의 누적 카운트도 셀 수 있다
            //게임 개발 방향에 따라 수정 될 여지는 있다
        }

        // 모든 타일을 맞춘 상황이다
        if (answerSelector >= 7)
        {
            isGameCompleted = true;
            isGameEnd = true;
            Debug.Log("Good!! Reward Here!!");
            CoffeeSwitch.SetActive(false);
        }
    }
}
