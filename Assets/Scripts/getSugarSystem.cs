using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getSugarSystem : MonoBehaviour
{
    public enum State { Left, Right, Wheel};
    public GameObject[] StateIndicator = new GameObject[3];
    public GameObject[] patternViewer = new GameObject[15];
    public State[] patternArray = new State[15];
    public RawImage strainerImage;

    public GameObject gameAdminObj;

    float imageAlphaValue = 0f;

    public int correctCount = 0;
    

    // 

    void OnEnable()
    {
        for(int i = 0; i < 15; i++)
        {
            patternViewer[i].SetActive(true);
        }
        correctCount = 0;
        Start();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Pattern Generate
        for(int i = 0; i < 15; i++)
        {
            int patternGenerateSeed = Random.Range(0, 3);
            switch(patternGenerateSeed)
            {

                case 0:
                    patternArray[i] = State.Left;
                    patternViewer[i].GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 180f);
                    patternViewer[i].GetComponentInParent<Outline>().effectColor = Color.red;
                break;
                case 1:
                    patternArray[i] = State.Right;
                    patternViewer[i].GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0f);
                    patternViewer[i].GetComponentInParent<Outline>().effectColor = Color.blue;
                    break;
                case 2:
                    patternArray[i] = State.Wheel;
                    patternViewer[i].GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 90f);
                    patternViewer[i].GetComponentInParent<Outline>().effectColor = Color.green;
                    break;
                default:
                    Debug.Log("Pattern Generate Failed!!");
                    continue;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(correctCount == 15)
        {
            gameAdminObj.SetActive(false);
        }

        if(strainerImage.color.a > 0.25f)
        {
            imageAlphaValue = strainerImage.color.a;
            imageAlphaValue -= 0.03f;
            strainerImage.color = new Color(strainerImage.color.r, strainerImage.color.g, strainerImage.color.b, imageAlphaValue);

        }


        if (Input.GetMouseButtonDown(0))
        {
            doScoring(State.Left);
        }

        if(Input.GetMouseButtonDown(1))
        {
            doScoring(State.Right);
        }

        if(Input.GetMouseButtonDown(2))
        {
            // No Rolling Wheel!
            doScoring(State.Wheel);
        }
    }

    public void doScoring(State state)
    {
        if(patternArray[correctCount] == state)
        {
            patternViewer[correctCount].GetComponentInParent<Outline>().effectColor = Color.black;
            patternViewer[correctCount].SetActive(false);
            // play corrent sound

            correctCount++;
            Debug.Log("Good");
            strainerImage.color = new Color(strainerImage.color.r, strainerImage.color.g, strainerImage.color.b, 1f);

        }
        else
        {
            // play wrong sound
            Debug.Log("NO!!!!");
        }
    }
}
