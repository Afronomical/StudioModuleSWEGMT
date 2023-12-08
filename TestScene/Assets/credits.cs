using UnityEngine;
using UnityEngine.SceneManagement;

public class credits : MonoBehaviour
{
    private Animator animator;
    
    // Start is called before the first frame update
    private void Start()
    {
        Time.timeScale = 1.0f;
        animator = GetComponent<Animator>();
        animator.Play("CreditAnimation");
        AudioManager.Manager.PlayMusic("Credits");
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //skip credits, return to menu 
            //FindFirstObjectByType<FadeTransitionController>().LoadNextLevel("Main Menu Animated");
            SceneManager.LoadScene("Main Menu Animated");
            AudioManager.Manager.StopMusic("Credits");
        }


        //when last thing in credits is in view, end them 
    }
}
