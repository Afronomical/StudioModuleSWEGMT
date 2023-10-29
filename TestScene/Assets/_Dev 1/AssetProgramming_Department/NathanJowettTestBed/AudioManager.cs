using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
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
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.Loop;
        }

        foreach (Music_Controller m in Music)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;
            m.source.volume = m.volume;
            m.source.pitch = m.pitch;
            m.source.loop = m.Loop;
            
        }
    }

    public void PlaySFX(string name)
    {
        SFX_Controller s = Array.Find(Sounds, SFX_Controller => SFX_Controller.Name == name);
        s.source.Play();
        
        
       // m.source.Play();

        if (s == null )
        {
            Debug.Log("Sound: " + name + " not found!");
            return;
        }
        

    }

    public void PlayMusic(String  name)
    {
        Music_Controller m = Array.Find(Music, Music_Controller => Music_Controller.Name == name);

        m.source.Play();
        if (m == null)
        {
            Debug.Log("Music: " + name + " not found!");
            return;
        }

    }
    public void StopAudio(string name)
    {
        Music_Controller m = Array.Find(Music, music=> music.Name== name);
        if(m != null)
        {
            m.source.Stop();
        }
    }
    public void StopSoundEffect(string name)
    {
        SFX_Controller s = Array.Find(Sounds,sound=> sound.Name == name);
        if(s != null)
        {
            s.source.Stop();
        }
    }
    


}











//Old Manager
/*public class AudioManager : MonoBehaviour
{
    public enum SoundEffects
    {
        //Player Effects
        PlayerTakeDamage,
        PlayerDeath,
        PlayerAttack,
        PlayerFeed,
        PlayerDodge,
        PlayerHeal,
        PlayerMoving,

        //NPC Sounds
        NPC_TakeDamage,
        NPC_Death,
        NPC_Downed,
        NPC_AttackMelee,
        NPC_AttackRanged,
        NPC_Scream,
        NPC_Alterted,
        NPC_VilToHunter,

        //UI SFX
        ButtonClick,
        *//*UpgradeAchieved,

        //*
    }

    // INFO: The order of sound effects and audio clips should be exactly the same, otherwise the incorrect sound will be played
    [SerializeField] private List<SoundEffects> soundEffectsList = new();
    [SerializeField] private List<AudioClip> audioClipsList = new();
    [SerializeField] private Dictionary<SoundEffects, AudioClip> soundEffectsDictionary = new();

    [SerializeField] private GameObject soundEffectsPlayerPrefab;

    private float soundVolume = 1f;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    private void Start()
    {
        for (int i = 0; i < soundEffectsList.Count; i++)
        {
            soundEffectsDictionary.Add(soundEffectsList[i], audioClipsList[i]);
        }
    }

    public void PlaySoundEffect(SoundEffects soundEffect)
    {
        if (soundEffectsDictionary.ContainsKey(soundEffect))
        {
            GameObject GO = Instantiate(soundEffectsPlayerPrefab);
            GO.GetComponent<AudioSource>().PlayOneShot(soundEffectsDictionary[soundEffect], soundVolume);
            Destroy(GO, soundEffectsDictionary[soundEffect].length);
        }
        else
        {
            Debug.Log("Sound not found!");
        }
    }

    public void SoundEffectVolume(float volume)
    {
        // INFO: Changed via slider for sound effect volume bar inside of settings menu
        soundVolume = volume;
    }
}*/





























/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum SoundEffects
    {
        //Player Effects
        PlayerTakeDamage,
        PlayerDeath,
        PlayerAttack,
        PlayerFeed,
        PlayerDodge,
        PlayerHeal,
        PlayerMoving,

        //NPC Sounds
        NPC_TakeDamage,
        NPC_Death,
        NPC_Downed,
        NPC_AttackMelee,
        NPC_AttackRanged,
        NPC_Scream,
        NPC_Alterted,
        NPC_VilToHunter,

        //UI SFX
        ButtonClick,
        *//*UpgradeAchieved,

        //Background Music Types
        MenuMusic,
        InLevelMusic,
        PlayerHuntedMusic,
        BossMusic,
        DeathEndMusic,
        BossVictoryMusic*//*
    }

    // INFO: The order of sound effects and audio clips should be exactly the same, otherwise the incorrect sound will be played
    [SerializeField] private List<SoundEffects> soundEffectsList = new();
    [SerializeField] private List<AudioClip> audioClipsList = new();
    [SerializeField] private Dictionary<SoundEffects, AudioClip> soundEffectsDictionary = new();

    [SerializeField] private GameObject soundEffectsPlayerPrefab;

    private float soundVolume = 1f;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    private void Start()
    {
        for (int i = 0; i < soundEffectsList.Count; i++)
        {
            soundEffectsDictionary.Add(soundEffectsList[i], audioClipsList[i]);
        }
    }

    public void PlaySoundEffect(SoundEffects soundEffect)
    {
        if (soundEffectsDictionary.ContainsKey(soundEffect))
        {
            GameObject GO = Instantiate(soundEffectsPlayerPrefab);
            GO.GetComponent<AudioSource>().PlayOneShot(soundEffectsDictionary[soundEffect], soundVolume);
            Destroy(GO, soundEffectsDictionary[soundEffect].length);
        }
        else
        {
            Debug.Log("Sound not found!");
        }
    }

    public void SoundEffectVolume(float volume)
    {
        // INFO: Changed via slider for sound effect volume bar inside of settings menu
        soundVolume = volume;
    }
}
*/