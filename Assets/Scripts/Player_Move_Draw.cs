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

    Vector3 mousePos, transPos, targetPos;

    void Awake()
    {

    }


    void Update()
    {
        //if (Input.GetMouseButton(0))
        //CalTargetPos();
        //MoveToTarget();
        drawLine();
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
            
            GameObject go = Instantiate(linePrefab);
            lr = go.GetComponent<LineRenderer>();
            col = go.GetComponent<EdgeCollider2D>();

            points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            lr.positionCount = 1;
            lr.SetPosition(0, new Vector3(points[0].x, points[0].y, -8f));
        }
        else if (Input.GetMouseButtonDown(0))
        {
            // 그리는 중간에 벽과 충돌하면 그리던 내용이 사라져야함 ( 벽에 안 닿게 그리는 것이 핵심 )
            //선을 파괴
            //포인트 배열을 초기화
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(Vector2.Distance(points[points.Count-1], pos) > 0.1f)
            {
                points.Add(pos);
                lr.positionCount++;
                lr.SetPosition(lr.positionCount - 1, new Vector3(pos.x, pos.y, -8f));
                col.points = points.ToArray();
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            //마우스를 떼면 선을 지우고
            //points 배열을 돌면서 그곳을 목적지로 이동
            //끝까지 이동했거나, 행동력이 0이라면 이동을 중지하고
            points.Clear();
        }
    }
}
