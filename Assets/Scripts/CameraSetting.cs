using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    Camera camera;
    Transform player;
    Vector3 cameraPos;

    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        cameraPos = camera.transform.position;
        Update_playerPosition();

    }

    void Update_playerPosition()
    {
        camera.transform.position = new Vector3(player.position.x, player.position.y, cameraPos.z);
    }
}
