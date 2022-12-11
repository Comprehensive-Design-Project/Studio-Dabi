using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventStartTrigger : MonoBehaviour
{
    public static EventStartTrigger inst { get; private set; }

    void Awake() => inst = this;

    [SerializeField]
    public GameObject rock_ending;
    [SerializeField]
    public GameObject rock_blocking;
    [SerializeField]
    public GameObject textUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            if(!RandomEvent.inst.isTutoEnd)
            {
                RandomEvent.inst.isTutoEnd = true;
                rock_ending.SetActive(false);
                rock_blocking.SetActive(true);
                SoundManager.inst.PlayRock();
                textUI.SetActive(true);
                StartCoroutine(CloseText());
                StartCoroutine(RandomEvent.inst.EventTimer());
            }
        }
    }

    public IEnumerator CloseText()
    {

        yield return new WaitForSeconds(4f);
        textUI.SetActive(false);
        yield return null;
        
    }

    public void StartBatText()
    {
        StartCoroutine(BatText());
    }

    public IEnumerator BatText()
    {
        textUI.GetComponent<Text>().text = "박쥐에게 물렸나보다..\n(최대 스태미나 감소 15초)";
        textUI.GetComponent<Text>().text.Replace("\\n", "\n");
        textUI.SetActive(true);
        yield return new WaitForSeconds(4f);
        textUI.SetActive(false);
    }
}
