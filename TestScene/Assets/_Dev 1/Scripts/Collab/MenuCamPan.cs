using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class MenuCamPan : MonoBehaviour
{
    [SerializeField] Material[] timeColour;

    [SerializeField] GameObject[] waypoint;

    [SerializeField] int currentWayPointIndex;

    [SerializeField] float speed = 5f;

    [SerializeField] Image menuCanvas;

    [SerializeField] Transform initialCamPos;


    private void Awake()
    {
        initialCamPos.position = transform.position;
    }

    public enum TimeOfDay
    {
        dawn, day, dusk, night
    }

    

    public TimeOfDay timeOfDay;

    private void Start()
    {
        

        transform.position = new Vector3 (initialCamPos.position.x, initialCamPos.position.y, transform.position.z);
        
        transform.position = new Vector3 (waypoint[currentWayPointIndex].transform.position.x, waypoint[currentWayPointIndex].transform.position.y, transform.position.z) ;

        timeOfDay= TimeOfDay.night;
        SetTimeMaterial();
        
    }
    private void Update()
    {
       // CheckIfPenultimateNode(waypoint, currentWayPointIndex);
        if (Vector2.Distance(waypoint[currentWayPointIndex].transform.position, transform.position) < 3)
        {
           
            currentWayPointIndex++;
            if(currentWayPointIndex >= waypoint.Length)
            {
                currentWayPointIndex = 0;
               
                

            }

        }
        transform.position = Vector2.MoveTowards(transform.position, waypoint[currentWayPointIndex].transform.position, Time.deltaTime * speed);


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

    public void SetTimeMaterial()
    {
       // menuCanvas.color = timeColour[(int)TimeOfDay.night].color;
        
    }
    private void CheckIfPenultimateNode(GameObject[] arrayOfNodes, int currentIndex)
    {
        if (currentIndex == arrayOfNodes.Length - 1)
        {
            SetTimeMaterial();
            Debug.Log("Time is now" + timeOfDay.ToString());
            timeOfDay++;
            
        }
    }
}

