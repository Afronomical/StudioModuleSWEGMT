using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadBar : MonoBehaviour
{
    public Slider slider;
    public GameObject boss;

    public float currentTime;
    public float reloadTime = 3f;

    Vector3 offset = new Vector3(0f, 11f);

    private void Start()
    {
        slider = GetComponent<Slider>();
        currentTime = reloadTime;
        boss = GameObject.Find("Van Helsing");
    }

    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(boss.transform.position) + offset;

        slider.value = currentTime/reloadTime;
        if(currentTime <= 0.01)
        {
            Destroy(gameObject);
        }
        else
        {
            currentTime -= Time.deltaTime;
        }
    }
}
