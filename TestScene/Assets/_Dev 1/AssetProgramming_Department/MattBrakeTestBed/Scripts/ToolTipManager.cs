using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ToolTipManager : MonoBehaviour
{
    //public static ToolTipManager instance;
    public TextMeshProUGUI toolTipText;
    public TextMeshProUGUI toolTipTopText;
    public GameObject topToolTip;
    public GameObject bottomToolTip;
    private RectTransform backgroundRectTransform;

    private void Awake()
    {
        //instance = this;
        //backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
       // toolTipText = transform.Find("ToolTipText").GetComponent<TextMeshProUGUI>();


    }

    private void Start()
    {
        topToolTip.SetActive(false);
        bottomToolTip.SetActive(false);
    }


    private void ShowTopToolTip(string tooltipString, float displayDuration)
    {
        topToolTip.SetActive(true);
        toolTipTopText.text = tooltipString;
        Debug.Log(tooltipString);
        if(displayDuration > 0)
        {
            StartCoroutine(RemoveToolTipAfterDelay(displayDuration));
        }

    }
    public void ShowTopToolTip(string tooltipString)
    {
        topToolTip.SetActive(true);
        toolTipTopText.text = tooltipString; 
    }
    public void HideTopToolTip()
    {
        topToolTip.SetActive(false);
    }

   public void ShowBottomToolTip(string tooltipstring)
    {
        bottomToolTip.SetActive(true);
        toolTipText.text = tooltipstring;
    }

    public void HideBottomToolTip()
    {
        if (bottomToolTip != null)
            bottomToolTip.SetActive(false);

        
    }
    

    private IEnumerator RemoveToolTipAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        HideTopToolTip();
    }


    public void ShowTopToolTip_Static(string toolTipString, float displayDuration)
    {
       ShowTopToolTip(toolTipString, displayDuration);
    }
    
    public void HideTopToolTip_Static()
    {
        HideTopToolTip();
    }

   public void ShowBottomToolTip_Static(string tooltipString)
    {
        ShowBottomToolTip(tooltipString);
    }

    public void HideBottomToolTip_Static()
    {
        HideBottomToolTip();
    }
    public void ShowTopToolTip_Static(string toolTipString)
    {
        ShowTopToolTip(toolTipString); 
    }
}
