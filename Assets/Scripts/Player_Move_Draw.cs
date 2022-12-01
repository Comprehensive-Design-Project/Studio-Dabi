using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move_Draw : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    bool isOnPlayer = false;
    bool canClick = true;
    
    public GameObject linePrefab;
    LineRenderer lr;
    EdgeCollider2D col;
    List<Vector2> points = new List<Vector2>();

    Vector3 mousePos, transPos, targetPos;

    void Awake()
    {

    }


    void Update()
    {

        //if (Input.GetMouseButton(0))
           //CalTargetPos();
        //MoveToTarget();
        if (canClick)
        {
            drawLine();
        }
        
    }

    void CalTargetPos()
    {
        mousePos = Input.mousePosition;
        transPos = Camera.main.ScreenToWorldPoint(mousePos);
        targetPos = new Vector3(transPos.x, transPos.y, 0);
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
    }

    void drawLine()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //전제 조건으로 클릭된 것이 캐릭터여야 함
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
                GameObject line = GameObject.Find("Line(Clone)");
                Destroy(line);
                for (int i=0; i < points.Count; i++)
                {
                    //행동력이 0이면 break
                    //이 때는 이동하던 것을 멈추고 그 자리에 서게 해야함


                    //문제는 지금 이게 하나의 업데이트 틱 안에서 일어나서 내가 원하는대로 되지 않는다는 것
                    //움직이는 함수를 손 봐서 버튼이 떼어졌을 때 어떤 부울 변수를 바꿔줘서 업데이트 안에서 이동하게 해야한다.
                    //이동이 끝나면 배열을 초기화하고 클릭이 가능하도록 풀어주면 된다.
                    targetPos = new Vector3(points[i].x, points[i].y, 1);
                    transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
                }

                isOnPlayer = false;
                points.Clear();
                canClick = true;
            }
        }
    }
}
