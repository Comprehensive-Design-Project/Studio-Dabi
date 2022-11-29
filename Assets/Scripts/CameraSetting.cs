using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    Camera camera;
    Vector3 cameraPos;
    float speed = 5f;
    bool isRightEdge, isLeftEdge, isTopEdge, isBottomEdge = false;

    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        //camera.orthographicSize -= 3;
    }

    void Update()
    {
        cameraPos = camera.transform.position;
        update_mousePosition();
    }

    void update_mousePosition()
    {
        Vector2 mousePos = camera.ScreenToViewportPoint(Input.mousePosition);

        if (mousePos.x < 0f)
        {
            mousePos.x = 0f;
            isLeftEdge = true;
        }
        else
        {
            isLeftEdge = false;
        }

        if (mousePos.x > 1f)
        {
            mousePos.x = 1f;
            isRightEdge = true;
        }
        else
        {
            isRightEdge = false;
        }

        if (mousePos.y < 0f)
        {
            mousePos.y = 0f;
            isBottomEdge = true;
        }
        else
        {
            isBottomEdge = false;
        }

        if (mousePos.y > 1f)
        {
            mousePos.y = 0f;
            isTopEdge = true;
        }
        else
        {
            isTopEdge = false;
        }

        if (isLeftEdge)
        {
            camera.transform.position = new Vector3(cameraPos.x - Time.deltaTime * speed, cameraPos.y, cameraPos.z);
        }
        else if (isRightEdge)
        {
            camera.transform.position = new Vector3(cameraPos.x + Time.deltaTime * speed, cameraPos.y, cameraPos.z);
        }
        if (isTopEdge)
        {
            camera.transform.position = new Vector3(cameraPos.x, cameraPos.y + Time.deltaTime * speed, cameraPos.z);
        }
        else if (isBottomEdge)
        {
            camera.transform.position = new Vector3(cameraPos.x, cameraPos.y - Time.deltaTime * speed, cameraPos.z);
        }       
    }
}
