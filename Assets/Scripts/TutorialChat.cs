using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialChat : MonoBehaviour
{
    int count;
    public TMP_Text talk;
    public GameObject textBox;
    readonly string[] str= new string[2];

    private void Awake()
    {
        count = 0;
        str[0] = "미탐사된 동굴 속에는 무엇이 있는지 아무도 모릅니다.\n" +
            "항상 위험 요소에 대비하여 무사히 귀환하기를 바랍니다.";
        str[1] = "마우스 드래그를 이용하여,\n플레이어가 무사히 탐사를 마칠 수 있게 도와주세요!\n(맵 끝으로 이동)";
    }

    private void Start()
    {
        StartCoroutine(ChangeChat());
    }

    private IEnumerator ChangeChat()
    {
        while (count < 2)
        {
            yield return new WaitForSeconds(5f);
            talk.text = str[count];
            talk.text.Replace("\\n", "\n");
            count++;
        }
        yield return new WaitForSeconds(5f);
        textBox.SetActive(false);
    }

    private void stopChat()
    {
        StopAllCoroutines();
        textBox.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "nothing")
        {
            Player_Move_Draw.inst.StopMove();
            stopChat();
            SceneManager.LoadScene("Loading 1");
        }
    }

}
