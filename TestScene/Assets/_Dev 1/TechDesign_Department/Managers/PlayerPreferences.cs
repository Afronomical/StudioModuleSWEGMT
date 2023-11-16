using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPreferences : MonoBehaviour
{

    public static PlayerPreferences instance;

    /*[Header("Audio")]
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;

    public Slider masterVolume_slider;
    public Slider musicVolume_slider;
    public Slider sfxVolume_slider;
*/
    [Header("Video")]
    public float brightness;
    
    private void Awake()
    {
       // DontDestroyOnLoad(this);

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
      /* masterVolume = PlayerPrefs.GetFloat("masterVolume");
       musicVolume = PlayerPrefs.GetFloat("musicVolume");
       sfxVolume = PlayerPrefs.GetFloat("sfxVolume");*/
        brightness = PlayerPrefs.GetFloat("brightness");
    }


    public float GetSetting(SettingType setting)
    {
        switch (setting)
        {
            default:
            /*case SettingType._MasterVolume:
                return PlayerPrefs.GetFloat("masterVolume");

            case SettingType._MusicVolume:
                return PlayerPrefs.GetFloat("musicVolume");

            case SettingType._SFXVolume:
                return PlayerPrefs.GetFloat("sfxVolume");*/

            case SettingType._Brightness:
                return PlayerPrefs.GetFloat("brightness");    
        }
    }

    public void SetSetting(SettingType setting, float newValue)
    {
        
            switch (setting)
        {
            default:
            /*case SettingType._MasterVolume:
                PlayerPrefs.SetFloat("masterVolume", newValue);
                break;

            case SettingType._MusicVolume:
                PlayerPrefs.SetFloat("musicVolume", newValue);
                break;

            case SettingType._SFXVolume:
                PlayerPrefs.SetFloat("sfxVolume", newValue);
                break;*/

            case SettingType._Brightness:
                PlayerPrefs.SetFloat("brightness", newValue);
                break;
        }
    
    }

    public enum SettingType
    {
       /* _MasterVolume, _MusicVolume, _SFXVolume,*/ _Brightness
    }


}
