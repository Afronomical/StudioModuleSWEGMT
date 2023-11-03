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
    private RectTransform backgroundRectTransform;

    private void Awake()
    {
        instance = this;
        //backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
       // toolTipText = transform.Find("ToolTipText").GetComponent<TextMeshProUGUI>();


    }


    private void ShowToolTip(string tooltipString)
    {
        gameObject.SetActive(true);

        toolTipText.text = tooltipString;
        //float textPaddingSize = 4f;

        //Vector2 backgroundSize = new Vector2(toolTipText.preferredWidth + textPaddingSize * 2f, toolTipText.preferredHeight + textPaddingSize * 2f);
        //backgroundRectTransform.sizeDelta = backgroundSize;

    }

    private void HideToolTip()
    {
        gameObject.SetActive(false);
    }


    public static void ShowToolTip_Static(string toolTipString)
    {
        instance.ShowToolTip(toolTipString);
    }

    public static void HideToolTip_Static()
    {
        instance.HideToolTip();
    }

}
