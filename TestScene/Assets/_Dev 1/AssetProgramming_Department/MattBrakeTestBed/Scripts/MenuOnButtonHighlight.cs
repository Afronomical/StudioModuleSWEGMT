using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuOnButtonHighlight : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IPointerExitHandler
{

    public Image TeethImage;
    public Button Button;
    private float lerpSpeed = 4f;
    private bool isHovering = false;
    private float originalY;
    private Vector3 originalPos;
    public ParticleSystem ParticleSystem;
    

    private void Start()
    {
       
        TeethImage.GetComponent<Image>().enabled = false;
        isHovering = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Highlighting button");
        //isHovering= true;
        //TeethImage.GetComponent<Image>().enabled = true;
        TeethImage.GetComponent<Image>().enabled = true;
        isHovering = true;
        ParticleSystem.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       
        Debug.Log("Exited"); 
        TeethImage.GetComponent<Image>().enabled = false;
        ParticleSystem.Stop();
    }

    public void OnSelect(BaseEventData eventData)
    {
      
    }

    //public void Update()
    //{
    //   if(isHovering == false)
    //    {
    //        TeethImage.enabled=false;
    //    }
    //}


}
