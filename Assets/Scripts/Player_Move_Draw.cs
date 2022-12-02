using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player_Move_Draw : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    public GameObject linePrefab;
    CameraSetting camSet;

    LineRenderer lr;
    EdgeCollider2D col;
    List<Vector2> points = new List<Vector2>();

    float stamina = 100f;
    bool isOnPlayer = false;
    bool canClick = true;
    bool playerMove = false;
    int index = 0;
    float timer = 0f;

    [Header("UI")]
    public Scrollbar scrollbar;//스크롤바
    public Image scroll;

    public static Player_Move_Draw inst { get; private set; }

    void Awake()
    {
        inst = this;
        scroll.fillAmount = stamina;
        camSet = GameObject.Find("Main Camera").GetComponent<CameraSetting>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.CompareTag("Event"))
        {
            playerMove = false;
            ResetVar();
            canClick = true;
            //카메라를 해제 ?
            camSet.FreeTrack();
        }
    }

    void Update()
    {

        if (canClick)
        {
            scroll.fillAmount = 1;
            drawLine();
        }
        
        if (playerMove)
        {
            if (index > points.Count - 1)
            {
                Debug.Log("끝까지 이동 !");
                playerMove = false;
                ResetVar();
                canClick = true;
                camSet.FreeTrack();
                return;
            }

            if (stamina < 0f)
            {
                Debug.Log("스태미나 고갈!");
                playerMove = false;
                ResetVar();
                canClick = true;
                camSet.FreeTrack();
                return;
            }

            MoveToTarget(index);

            timer += Time.deltaTime;

            if (timer > 0.03f)
            { 
                index++;   
                timer = 0f;  
            }
        }
    }

    void MoveToTarget(int index)
    {
        transform.position = Vector3.MoveTowards(transform.position, points[index], Time.deltaTime * speed);
        stamina -= 50*Time.deltaTime;

        scroll.fillAmount = stamina*0.01f;
    }

    void ResetVar()
    {
        index = 0;
        timer = 0f;
        stamina = 100f;
        points.Clear();
    }

    void drawLine()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 rayPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.name == "Player")
                {
                    isOnPlayer = true;

                    GameObject go = Instantiate(linePrefab);
                    lr = go.GetComponent<LineRenderer>();
                    col = go.GetComponent<EdgeCollider2D>();

                    points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    lr.positionCount = 1;
                    lr.SetPosition(0, points[0]);
                }
            }   
        }
        else if (Input.GetMouseButton(0))
        {
            if (isOnPlayer)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("wall"))
                    {
                        GameObject line = GameObject.Find("Line(Clone)");
                        Destroy(line);
                        ResetVar();
                        canClick = true;
                        isOnPlayer = false;
                        return;
                    }
                }

                if (Vector2.Distance(points[points.Count - 1], pos) > 0.1f)
                {
                    points.Add(pos);
                    lr.positionCount++;
                    lr.SetPosition(lr.positionCount - 1, pos);
                    col.points = points.ToArray();
                }
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if (isOnPlayer)
            {
                camSet.SetTrack();
                canClick = false;
                playerMove = true;
                GameObject line = GameObject.Find("Line(Clone)");
                Destroy(line);
                isOnPlayer = false;
            }
        }
    }
}
