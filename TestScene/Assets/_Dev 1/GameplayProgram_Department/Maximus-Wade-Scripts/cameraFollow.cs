using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    //public Transform Target; // Reference to the player's transform
    public float SmoothSpeed = 0.125f;
    public Vector3 Offset;

    private float startCamSize;
    private Vector3 shakeCoords;

    private Transform Target;
    //public GameManager gameManager;

    private void Start()
    {
        Target = PlayerController.Instance.transform;
        //Target = gameManager.player.transform;
        //Target = FindFirstObjectByType<PlayerController>().GetComponent<Transform>();
        startCamSize = GetComponent<Camera>().orthographicSize;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    CameraZoom(0.2f, 3f);
        //}

        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    CameraZoom(0.2f, 0);
        //}

    }

    void LateUpdate()
    {
        

        
    }

    void FixedUpdate()
    {
        // Calculate the desired camera position based on the player's position and offset
        Vector3 desiredPosition = Target.position + Offset + shakeCoords;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);
        transform.position = smoothedPosition;

        // Make the camera look at the player
        //transform.LookAt(Target);

        

    }

    public void StartShake(float duration, float shakeIntensity)
    {
        StartCoroutine(Shake(duration, shakeIntensity));
    }

    public void CameraZoom(float duration, float zoom)
    {
        StartCoroutine(CamZoom(duration, zoom));
    }

    IEnumerator Shake(float duration, float shakeIntensity)
    {
        Vector3 startPos = transform.position; //should be player pos
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            shakeCoords = (Random.insideUnitSphere * shakeIntensity);
            startPos = transform.position;
            //shakeCoords = new Vector3(0, 0, 0);
            yield return null;
        }
        shakeCoords = new Vector3(0, 0, 0);
        //transform.position = startPos;
    }

    IEnumerator CamZoom(float duration, float zoom)
    {
        float elapsedTime = 0f;
        float startZoomedIn = GetComponent<Camera>().orthographicSize;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            if (zoom == 0)
            {
                GetComponent<Camera>().orthographicSize = Mathf.Lerp(startZoomedIn, startCamSize, elapsedTime / duration);
            }
            else
            {
                GetComponent<Camera>().orthographicSize = Mathf.Lerp(startCamSize, zoom, elapsedTime / duration);
            }
            
            
            
            yield return null;
        }
        
    }
}
