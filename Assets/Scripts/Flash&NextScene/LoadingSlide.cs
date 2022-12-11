using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadingSlide : MonoBehaviour
{
    public Slider progressBar;
    public GameObject loading;
    public GameObject loadingComplete;
    public GameObject nextButton;

    private void Awake()
    {
        loadingComplete.SetActive(false);
        nextButton.SetActive(false);
        loading.SetActive(true);
        progressBar.value = 0;
    }

    void Start()
    {
        StartCoroutine(LoadScene());   
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync("Tutorial");
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            yield return null;
            if (progressBar.value < 0.9f)
            {
                progressBar.value = Mathf.MoveTowards(progressBar.value, 0.9f,Time.deltaTime/3);
            }
            else if(operation.progress>=0.9f)
            {
                progressBar.value = Mathf.MoveTowards(progressBar.value,1f, Time.deltaTime);
                loading.SetActive(false);
                loadingComplete.SetActive(true);
                nextButton.SetActive(true);
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
