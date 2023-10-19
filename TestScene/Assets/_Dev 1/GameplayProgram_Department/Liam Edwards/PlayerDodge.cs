using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeSystem : MonoBehaviour
{
    public float dodgeDistance = 2.5f;
    public float dodgeCoolDown = 1f;
    private float dodgeTimer;
    private Vector3 dodgeDirection;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dodgeTimer <= 0)
            {
                dodgeTimer = dodgeCoolDown;
                dodgeDirection = Vector3.Normalize(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
                transform.position += dodgeDirection * dodgeDistance;
            }
        }
        dodgeTimer -= Time.deltaTime;
    }
}