using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

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
            //DontDestroyOnLoad(_loaderCanvas);  crashes unity 
        }
        else
        {
            
            Destroy(gameObject);
        }

       
    }

    public void Start()
    {
        //_loaderCanvas.SetActive(false);
        //DontDestroyOnLoad(_loaderCanvas);
    }

    public async void LoadScene(string SceneName)
    {
 
        _target = 1f; 
        _progressBar.fillAmount = 0.1f;
        
        var scene = SceneManager.LoadSceneAsync(SceneName);
        scene.allowSceneActivation = false;
        
        
        _loaderCanvas.SetActive(true);


       
        do
        {
            if (_progressBar != null)
            {
                await Task.Delay(500);
                _target = scene.progress;
            }
            else
            {
                Debug.LogWarning("Progress Bar ref is null");
            }

            _target = scene.progress;

        } while (_progressBar.fillAmount < 0.9f);

        

      
        scene.allowSceneActivation = true;
       
        _loaderCanvas.SetActive(false);

        

    }
    private void Update()
    {
       if(_progressBar != null)
        {
            _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _target, 3 * Time.deltaTime);
        }
        
        
    }

}
