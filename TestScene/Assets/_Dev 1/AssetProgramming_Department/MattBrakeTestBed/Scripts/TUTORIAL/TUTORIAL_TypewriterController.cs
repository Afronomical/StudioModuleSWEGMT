using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TUTORIAL_TypewriterController : MonoBehaviour
{
    // Typewriter System Variables:
    [SerializeField] private float typewriterDelay = 0.075f;
    [SerializeField] private TextMeshProUGUI CanvasText;
    private bool isDisplaying = false;
    private string textToDisplay;
    private Coroutine coroutine;

    private readonly List<GameObject> triggersList = new();

    public void SetIsDisplaying(bool isDisplaying, string text) 
    {
        if (coroutine != null)
        {
            this.isDisplaying = false;
            StopCoroutine(coroutine);
            CanvasText.text = "";
        }

        if (!this.isDisplaying)
        {
            this.isDisplaying = isDisplaying;
            textToDisplay = text;
            coroutine = StartCoroutine(TypewriterEffect(typewriterDelay));
        }
    }

    private void Start()
    {
        foreach (Transform child in transform)
        {
            triggersList.Add(child.gameObject);
        }
    }

    private IEnumerator TypewriterEffect(float delay)
    {

        bool characterDisplayed = false;
        int j = -1;

        for (int i = 0; i < textToDisplay.Length; i++)
        {
            if (j != i)
            {
                characterDisplayed = false;
                j = i;
            }

            if (!characterDisplayed)
            {
                CanvasText.text += textToDisplay[i];
                characterDisplayed = true;
            }
            yield return new WaitForSeconds(delay);
        }

        isDisplaying = false;
        PlayerController.Instance.GetComponent<PlayerController>().enabled = true; 
    }
}
