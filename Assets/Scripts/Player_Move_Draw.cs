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
            //���� �������� Ŭ���� ���� ĳ���Ϳ��� ��
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
            // �׸��� �߰��� ���� �浹�ϸ� �׸��� ������ ��������� ( ���� �� ��� �׸��� ���� �ٽ� )
            //���� �ı�
            //����Ʈ �迭�� �ʱ�ȭ
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
                    //�ൿ���� 0�̸� break
                    //�� ���� �̵��ϴ� ���� ���߰� �� �ڸ��� ���� �ؾ���


                    //������ ���� �̰� �ϳ��� ������Ʈ ƽ �ȿ��� �Ͼ�� ���� ���ϴ´�� ���� �ʴ´ٴ� ��
                    //�����̴� �Լ��� �� ���� ��ư�� �������� �� � �ο� ������ �ٲ��༭ ������Ʈ �ȿ��� �̵��ϰ� �ؾ��Ѵ�.
                    //�̵��� ������ �迭�� �ʱ�ȭ�ϰ� Ŭ���� �����ϵ��� Ǯ���ָ� �ȴ�.
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
