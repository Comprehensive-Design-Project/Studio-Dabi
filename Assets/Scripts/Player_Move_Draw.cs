using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move_Draw : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    public GameObject linePrefab;

    LineRenderer lr;
    EdgeCollider2D col;
    List<Vector2> points = new List<Vector2>();

    float stamina = 100f;
    bool isOnPlayer = false;
    bool canClick = true;
    bool playerMove = false;
    int index = 0;
    float timer = 0f;
  
    void Awake()
    {

    }


    void Update()
    {
        if (canClick)
        {
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
                return;
            }

            //헹동력이 0이 되도 마찬가지
            if (stamina < 0f)
            {
                Debug.Log("스태미나 고갈!");
                playerMove = false;
                ResetVar();
                canClick = true;
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
        Vector3 targetPos = new Vector3(points[index].x, points[index].y, 1);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
        stamina -= 50*Time.deltaTime;

        if (stamina > 90)
        {
            Debug.Log("90 Upper");
        }
        else if (stamina > 70)
        {
            Debug.Log("70 Upper");
        }
        else if (stamina > 50)
        {
            Debug.Log("50 Upper");
        }
        else if (stamina > 30)
        {
            Debug.Log("30 Upper");
        }
        else
        {
            Debug.Log("LOW!!");
        }
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
            // 그리는 중간에 벽과 충돌하면 그리던 내용이 사라져야함 ( 벽에 안 닿게 그리는 것이 핵심 )
            //선을 파괴
            //포인트 배열을 초기화
            if(isOnPlayer)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
