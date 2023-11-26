using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider MasterSlider;
    [SerializeField] Slider MusicSlider;
    [SerializeField] Slider SFXSlider;

    public const string MIXER_MASTER = "MasterVolume";
    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_SFX = "SFXVolume";

    private void Start()
    {
        MasterSlider.value = PlayerPrefs.GetFloat(AudioManager.MASTER_KEY, 1f);
        MusicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
        SFXSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f);
    }

    private void Awake()
    {
        MasterSlider.onValueChanged.AddListener(SetMasterVol);
        MusicSlider.onValueChanged.AddListener(SetMusicVol);
        SFXSlider.onValueChanged.AddListener(SetSFXVol);
    }

    void SetMasterVol(float value)
    {
        mixer.SetFloat(MIXER_MASTER, Mathf.Log10(value) *20);
    }

    void SetMusicVol(float value)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    }

    void SetSFXVol(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
       
    }

    
    private void OnDisable()
    {

        PlayerPrefs.SetFloat(AudioManager.MASTER_KEY, MasterSlider.value);
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, MusicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, SFXSlider.value);
    }
}
