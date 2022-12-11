using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    public Camera cam;
    public Transform player;
    Vector3 cameraPos;

    void Start()
    {
    }

    void Update()
    {
        cameraPos = cam.transform.position;
        Update_playerPosition();

    }

    void Update_playerPosition()
    {
        cam.transform.position = new Vector3(player.position.x, player.position.y, cameraPos.z);
    }
}
