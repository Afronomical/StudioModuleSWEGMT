using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColourChangeButton : MonoBehaviour
{
    private Button button;
    private bool isGreen = false;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ToggleColor);
    }

    void ToggleColor()
    {
        isGreen = !isGreen;

        if (isGreen)
        {
            // Change to green color (you can customize the color as needed)
            button.image.color = Color.green;
        }
        else
        {
            // Change back to white color
            button.image.color = Color.white;
        }
    }
}
