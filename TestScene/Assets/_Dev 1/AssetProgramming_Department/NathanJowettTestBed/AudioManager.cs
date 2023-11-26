using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    public const string MASTER_KEY = "masterVolume";
    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";
    public SFX_Controller[] Sounds;
    public Music_Controller[] Music;

    public static AudioManager Manager;


    private void Awake()
    {
        if (AudioManager.Manager == null)
            AudioManager.Manager = this;
        else
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(gameObject);



        foreach (SFX_Controller s in Sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.MixerGroup;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.Loop;
        }

        foreach (Music_Controller m in Music)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;
            m.source.outputAudioMixerGroup = m.MixerGroup;
            m.source.volume = m.volume;
            m.source.pitch = m.pitch;
            m.source.loop = m.Loop;
        }

        LoadVolume();
    }

    void LoadVolume()
    {
        float masterVolume = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        mixer.SetFloat(VolumeSettings.MIXER_MASTER, Mathf.Log10(masterVolume) * 20);
        mixer.SetFloat(VolumeSettings.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(VolumeSettings.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
    }
    public void PlaySFX(string name)
    {
        SFX_Controller s = Array.Find(Sounds, SFX_Controller => SFX_Controller.Name == name);
        s.source.Play();

        if (s == null)
        {
            Debug.Log("Sound: " + name + " not found!");
            return;
        }
    }

    public void PlayMusic(String name)
    {
        Music_Controller m = Array.Find(Music, Music_Controller => Music_Controller.Name == name);

        m.source.Play();
        if (m == null)
        {
            Debug.Log("Music: " + name + " not found!");
            return;
        }
    }
    public void StopMusic(string name)
    {
        Music_Controller m = Array.Find(Music, music => music.Name == name);
        if (m != null)
        {
            m.source.Stop();
        }
    }
    public void StopSFX(string name)
    {
        SFX_Controller s = Array.Find(Sounds, sound => sound.Name == name);
        if (s != null)
        {
            s.source.Stop();
        }
    }
    public void stopAllInGameSFX()
    {
        AudioManager.Manager.StopSFX("PlayerAttack");
        AudioManager.Manager.StopSFX("PlayerDodge");
        AudioManager.Manager.StopSFX("PlayerHeavyAttack");
        AudioManager.Manager.StopSFX("BossMelee");
        AudioManager.Manager.StopSFX("NPC_RangedAttack");
        AudioManager.Manager.StopSFX("Bossdash");
        AudioManager.Manager.StopSFX("Parry");
        AudioManager.Manager.StopSFX("NPC_TakeDamage");

    }
    public void OnMouseUp()
    {
        AudioManager.Manager.PlaySFX("UI_Click");
        
    }
}