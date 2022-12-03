using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadingSlide : MonoBehaviour
{
    public Slider progressBar;


    void Start()
    {
        StartCoroutine(LoadScene());   
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync("Game");
        op.allowSceneActivation = false;
        while (!op.isDone)
        {
            yield return null;
            if (progressBar.value < 0.9f)
            {
                progressBar.value = Mathf.MoveTowards(progressBar.value, 0.9f,Time.deltaTime/2);
            }
            else if(op.progress>=0.9f)
            {
                progressBar.value = Mathf.MoveTowards(progressBar.value,1f, Time.deltaTime);
            }
           
            if (Input.GetKeyDown(KeyCode.Space) && progressBar.value >= 1f && op.progress >= 0.9f)
            {
                op.allowSceneActivation = true;
                SceneManager.LoadScene("Game");
            }
        }
        /*op.isDone;
        op.progress;
        op.allowSceneActivation;*/

    }
    void Update()
    {

    }
}
