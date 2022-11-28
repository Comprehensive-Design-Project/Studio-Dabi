using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    Camera camera;

    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        camera.orthographicSize -= 3;
    }
}
