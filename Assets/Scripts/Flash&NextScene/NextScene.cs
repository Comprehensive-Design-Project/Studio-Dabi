using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public enum FlashType
{
    Top,
    Bottom,
    Exit,
    Start,
    Load,
    Game,
    End
}

public class NextScene : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public FlashType nowType;
    public Transform buttonScale;
    Vector3 defaultScale;

    void Start()
    {
        defaultScale = buttonScale.localScale;
    }

    public void onClick()
    {
        switch (nowType)
        {
            case FlashType.Start:
                SceneManager.LoadScene("Loading");
                break;
            case FlashType.End:
                SceneManager.LoadScene("Start");
                break;
            case FlashType.Game:
                SceneManager.LoadScene("Ending");
                break;
        }
    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}
