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
            //���� �������� Ŭ���� ���� ĳ���Ϳ��� ��
            
            GameObject go = Instantiate(linePrefab);
            lr = go.GetComponent<LineRenderer>();
            col = go.GetComponent<EdgeCollider2D>();

            points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            lr.positionCount = 1;
            lr.SetPosition(0, new Vector3(points[0].x, points[0].y, -8f));
        }
        else if (Input.GetMouseButtonDown(0))
        {
            // �׸��� �߰��� ���� �浹�ϸ� �׸��� ������ ��������� ( ���� �� ��� �׸��� ���� �ٽ� )
            //���� �ı�
            //����Ʈ �迭�� �ʱ�ȭ
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
            //���콺�� ���� ���� �����
            //points �迭�� ���鼭 �װ��� �������� �̵�
            //������ �̵��߰ų�, �ൿ���� 0�̶�� �̵��� �����ϰ�
            points.Clear();
        }
    }
}
