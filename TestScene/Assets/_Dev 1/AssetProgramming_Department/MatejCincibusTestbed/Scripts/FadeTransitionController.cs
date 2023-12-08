using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeTransitionController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float transitionDuration = 0f;

    private void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
    }

    public void LoadNextLevel(string levelName)
    {

        StartCoroutine(LoadLevel(levelName));
    }
        

    private IEnumerator LoadLevel(string levelName)
    {
       
        //Time.timeScale = 1.0f;
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionDuration);
        if(CanvasManager.Instance != null)
        {
            CanvasManager.Instance.deathScreenCanvas.GetComponent<Canvas>().enabled = false;
        }
       
        SceneManager.LoadScene(levelName);
        
    }
}