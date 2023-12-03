using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeTransitionController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float transitionDuration = 1.5f;

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
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionDuration);

        SceneManager.LoadScene(levelName);
    }
}