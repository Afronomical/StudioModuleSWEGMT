using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamPan : MonoBehaviour
{
    [SerializeField] Material dayTime;

    [SerializeField] GameObject[] waypoint;

    [SerializeField] int currentWayPointIndex;

    [SerializeField] float speed = 5f;




    public enum TimeOfDay
    {
        day, night, dusk, dawn
    }

    public TimeOfDay timeOfDay;

    private void Start()
    {
        transform.position = waypoint[currentWayPointIndex].transform.position;
    }
    private void FixedUpdate()
    {
        if (Vector2.Distance(waypoint[currentWayPointIndex].transform.position, transform.position) < 1)
        {
            currentWayPointIndex++;
            if(currentWayPointIndex >= waypoint.Length)
            {
                currentWayPointIndex = 0;
            }

        }
        transform.position = Vector2.LerpUnclamped(transform.position, waypoint[currentWayPointIndex].transform.position, Time.deltaTime * speed);


        switch (timeOfDay)
        {
            case TimeOfDay.day:
                //Set material day
                break;

            case TimeOfDay.night:
                //Set material night
                break;

            case TimeOfDay.dusk:
                //Set material dusk
                break;

            case TimeOfDay.dawn:
                //Set material dawn
                break;

            default:
                break;
        }
    }
}

