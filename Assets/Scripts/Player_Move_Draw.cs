using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class Player_Move_Draw : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    public GameObject linePrefab;
    public CameraSetting camSet;

    LineRenderer lr;
    EdgeCollider2D col;
    List<Vector2> points = new List<Vector2>();

    float stamina = 100f;
    public float maxStamina = 100f;
    bool isOnPlayer = false;
    public bool canClick = true;
    bool playerMove = false;
    int index = 0;
    float timer = 0f;

    [Header("UI")]
    public Scrollbar scrollbar;//��ũ�ѹ�
    public Image scroll;

    public static Player_Move_Draw inst { get; private set; }

    void Awake()
    {
        inst = this;
        scroll.fillAmount = stamina;
        camSet = GameObject.Find("Main Camera").GetComponent<CameraSetting>();
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
                Debug.Log("������ �̵� !");
                StopMove();
                canClick = true;
                return;
            }

            if (stamina < 0f)
            {
                Debug.Log("���¹̳� ����!");
                StopMove();
                canClick = true;
                return;
            }

            MoveToTarget(index);

            timer += Time.deltaTime;

            if (timer > 20*Time.deltaTime)
            { 
                index++;
                timer = 0f;
            }
        }
    }

    void MoveToTarget(int index)
    {
        //transform.position = Vector3.MoveTowards(transform.position, points[index], Time.deltaTime*speed);
        transform.DOMove(points[index], 0.35f).SetEase(Ease.Linear);
        stamina -= 50*Time.deltaTime;
        scroll.fillAmount = stamina*0.01f;
    }
    public void StopMove()
    {
        playerMove = false;
        ResetVar();
    }

    void ResetVar()
    {
        index = 0;
        timer = 0f;
        stamina = maxStamina;
        points.Clear();
    }

    public void DestroyLine()
    {
        GameObject line = GameObject.Find("Line(Clone)");
        if (line != null)
        {
            Destroy(line);
            ResetVar();
            isOnPlayer = false;
        } 
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
                        DestroyLine();
                        canClick = true;
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
                canClick = false;
                playerMove = true;
                GameObject line = GameObject.Find("Line(Clone)");
                Destroy(line);
                isOnPlayer = false;
            }
        }
    }
}
