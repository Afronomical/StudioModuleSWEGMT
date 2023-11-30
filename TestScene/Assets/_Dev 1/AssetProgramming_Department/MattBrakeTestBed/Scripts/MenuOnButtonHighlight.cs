using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuOnButtonHighlight : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{

    public Image Teeth;
    public Button Button;
    private float lerpSpeed = 4f;
    private bool isHovering = false;
    private float originalY;
    private Vector3 originalPos; 
    

    private void Start()
    {
        originalY = Teeth.transform.position.y; 
        originalPos = Teeth.transform.position;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Highlighting button");
        isHovering= true;
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      ////reset pos and have teeth image go back to original 
      isHovering= false;
      ResetTeethPos();
    }

    public void OnSelect(BaseEventData eventData)
    {
      
    }

    private void Update()
    {
       if(isHovering)
        {
           //mathf.lerp from current image pos to the y pos of the mouse pos 
           float targetY = Input.mousePosition.y;
            Vector3 lerpedPos = Teeth.transform.position;
            lerpedPos.y = Mathf.Lerp(lerpedPos.y, targetY, Time.deltaTime * lerpSpeed);
            Teeth.transform.position = lerpedPos;
        }
        else
        {
             Teeth.transform.position = Vector3.Lerp(Teeth.transform.position, originalPos, Time.deltaTime * lerpSpeed);
        }
    }

    private void ResetTeethPos()
    {
        Teeth.transform.position = originalPos; 
    }
}
