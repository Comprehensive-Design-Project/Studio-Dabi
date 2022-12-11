using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    float moveX;
    float speedX = 1;
    float posX=-17;
    bool iswall;
    bool isChat1;
    bool isChat2;

    bool isChatstate1;
    bool isChatstate2;

    public Transform wallChk;
    public float wallChkDistance;
    public LayerMask w_layer;
    public LayerMask chat_layer1;
    public LayerMask chat_layer2;
    public GameObject txt1;
    public GameObject txt2;
    public GameObject txt3;

    private void Awake()
    {
        isChatstate2 = false;
        txt3.SetActive(false);
        txt1.SetActive(true);
        transform.position = new Vector2(posX, 0);
    }
   

    // Update is called once per frame
    void Update()
    {
        isChat1= Physics2D.Raycast(wallChk.position, Vector2.right, wallChkDistance, chat_layer1);
        isChat2 = Physics2D.Raycast(wallChk.position, Vector2.right, wallChkDistance, chat_layer2);
        iswall =Physics2D.Raycast(wallChk.position, Vector2.right, wallChkDistance, w_layer);
        
        if (iswall)
        {
            speedX = 0;
            Debug.Log("º®¿¡ ´êÀ½");
            moveX = speedX * 3f * Time.deltaTime;
            transform.position = new Vector2(transform.position.x + moveX, transform.position.y);
            SceneManager.LoadScene("Loading 1");
            isChatstate2 = false;
            txt3.SetActive(false);
        }
        else
        {
            moveX = speedX * 3f * Time.deltaTime;
            transform.position = new Vector2(transform.position.x + moveX, transform.position.y);
        }

        if (isChat1)
        {
            isChatstate1 = true;
        }
        if (isChat2)
        {
            isChatstate2 = true;
        }

        if (isChatstate1)
        {
            txt1.SetActive(false);
            txt2.SetActive(true);
        }

        if (isChatstate2)
        {
            isChatstate1 = false;
            txt2.SetActive(false);
            txt3.SetActive(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(wallChk.position, Vector2.right * wallChkDistance);
    }
}
