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

    private void Start()
    {
        slider = GetComponent<Slider>();
        currentTime = reloadTime;
        boss = GameObject.Find("Van Helsing");
    }

    private void Update()
    {
        if(slider.value <= 0)
        {
            Destroy(gameObject);
            boss.GetComponent<AICharacter>().reloading = false;
        }
        slider.value = currentTime/reloadTime;
        if(currentTime <= 0)
        {
            currentTime = reloadTime;
        }
        else
        {
            currentTime -= Time.deltaTime;
        }
    }
}
