using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;
    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _progressBar;
    private float _target; 

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async void LoadScene(string SceneName, float loadingDuration)
    {
        _target = 0; 
        _progressBar.fillAmount = 0;
        _loaderCanvas.SetActive(true);
        
        var scene = SceneManager.LoadSceneAsync(SceneName);
        scene.allowSceneActivation = false;

        float startTime = Time.time;


        do
        {
            if (_progressBar != null)
            {
                await Task.Delay(100);
                _target = scene.progress;
            }
            else
            {
                Debug.LogWarning("Progress Bar ref is null");
            }

            //_target = scene.progress;

        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        float remainingTime = loadingDuration - (Time.time - startTime);
        if(remainingTime > 0)
        {
            await Task.Delay((int)(remainingTime * 1000));
        }

        _loaderCanvas.SetActive(false);
    }
    private void Update()
    {
        _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _target, 3 * Time.deltaTime);
    }

}
