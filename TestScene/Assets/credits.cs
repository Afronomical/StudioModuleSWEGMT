using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class credits : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Manager.PlayMusic("Credits");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //skip credits, return to menu 
            FindFirstObjectByType<FadeTransitionController>().LoadNextLevel("Main Menu Animated");
            //SceneManager.LoadScene("Main Menu Animated");
            AudioManager.Manager.StopMusic("Credits");
        }


        //when last thing in credits is in view, end them 
    }
}
