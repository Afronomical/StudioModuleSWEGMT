
using UnityEngine;
using UnityEngine.UI;

public class BlinkEffect : MonoBehaviour
{
    public Image image;
    public Color startColour;
    public Color endColour = Color.cyan;
    [Range(0,10)]
    public float speed = 1;


    private void Awake()
    {
        image = GetComponent<Image>();
        startColour = image.color;
    }



    private void Update()
    {
        image.color = Color.Lerp(startColour, endColour, Mathf.PingPong(Time.time * speed, 1));
    }











}
