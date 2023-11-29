using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Music_Controller
{
    
    public string Name;
    public AudioClip clip;
    public AudioMixerGroup MixerGroup;

    [Range(0f, 1f)] public float volume;
    [Range(.1f, 3f)] public float pitch ;

    public bool Loop;

    [HideInInspector]
    public AudioSource source;
}
