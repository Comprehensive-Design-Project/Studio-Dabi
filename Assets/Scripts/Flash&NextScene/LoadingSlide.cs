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
        AsyncOperation operation = SceneManager.LoadSceneAsync("DrawTest");
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            yield return null;
            if (progressBar.value < 0.9f)
            {
                progressBar.value = Mathf.MoveTowards(progressBar.value, 0.9f,Time.deltaTime/2);
            }
            else if(operation.progress>=0.9f)
            {
                progressBar.value = Mathf.MoveTowards(progressBar.value,1f, Time.deltaTime);
            }
           
            if (Input.GetKeyDown(KeyCode.Space) && progressBar.value >= 1f && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
                //SceneManager.LoadScene("DrawTest");
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
