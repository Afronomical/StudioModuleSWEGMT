using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCursor : MonoBehaviour
{

    public Transform player;

    void Update()

    {

        Vector3 mousePos = Input.mousePosition;

        mousePos.z = 10;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        worldPos.y = player.position.y;

        player.LookAt(worldPos);

    }

}

