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
        str[0] = "��Ž��� ���� �ӿ��� ������ �ִ��� �ƹ��� �𸨴ϴ�.\n" +
            "�׻� ���� ��ҿ� ����Ͽ� ������ ��ȯ�ϱ⸦ �ٶ��ϴ�.";
        str[1] = "���콺 �巡�׸� �̿��Ͽ�,\n�÷��̾ ������ Ž�縦 ��ĥ �� �ְ� �����ּ���!\n(�� ������ �̵�)";
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
