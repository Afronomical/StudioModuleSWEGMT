using TMPro;
using UnityEngine;

public class TUTORIAL_TRIGGERS : MonoBehaviour
{
    
    //public TextMeshProUGUI TriggerText;
    public string TutorialText;
    public GameObject TutorialCanvas;
    public TextMeshProUGUI CanvasText;
    public GameObject ArrowToSpawn;

    private TUTORIAL_TypewriterController controller;
    private BoxCollider2D boxCollider;


    private void Start()
    {
        controller = transform.parent.GetComponent<TUTORIAL_TypewriterController>();
        CanvasText.text = "";
        TutorialCanvas.SetActive(false);

        if (ArrowToSpawn != null)
            ArrowToSpawn.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, boxCollider.size);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) //&& !isDisplaying
        {
            TutorialCanvas.SetActive(true);
            controller.SetIsDisplaying(true, TutorialText); 

            if (ArrowToSpawn != null)
                ArrowToSpawn.SetActive(true);        
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            CanvasText.text = "";
            TutorialCanvas.SetActive(false);

            if (ArrowToSpawn != null)
                ArrowToSpawn.SetActive(false);
        }
    }
}
