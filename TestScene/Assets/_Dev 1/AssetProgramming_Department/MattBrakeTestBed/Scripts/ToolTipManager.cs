using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ToolTipManager : MonoBehaviour
{
    public static ToolTipManager instance;
    public TextMeshProUGUI toolTipText;
    public TextMeshProUGUI toolTipTopText;
    public GameObject topToolTip;
    public GameObject bottomToolTip;
    private RectTransform backgroundRectTransform;

    private void Awake()
    {
        instance = this;
        //backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
       // toolTipText = transform.Find("ToolTipText").GetComponent<TextMeshProUGUI>();


    }

    private void Start()
    {
        bottomToolTip.SetActive(false);
        topToolTip.SetActive(false);
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


    public static void ShowTopToolTip_Static(string toolTipString, float displayDuration)
    {
       instance.ShowTopToolTip(toolTipString, displayDuration);
    }
    
    public static void HideTopToolTip_Static()
    {
        instance.HideTopToolTip();
    }

   public static void ShowBottomToolTip_Static(string tooltipString)
    {
        instance.ShowBottomToolTip(tooltipString);
    }

    public static void HideBottomToolTip_Static()
    {
        instance.HideBottomToolTip();
    }
}
