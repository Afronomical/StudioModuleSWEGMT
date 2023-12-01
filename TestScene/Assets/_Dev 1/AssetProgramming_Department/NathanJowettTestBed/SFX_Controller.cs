using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;



[System.Serializable]
public class SFX_Controller
{
 
    public string Name;
    public AudioClip clip;
    public AudioMixerGroup MixerGroup;

    
    [Range(0f, 1f)] public float volume;
    [Range(.1f, 3f)] public float pitch;

    public bool Loop;

    [HideInInspector]
    public AudioSource source;
}

//AudioManager.Manager.PlaySFX("PlayerMove");
//AudioManager.Manager.PlaySFX("PlayerAttack");
//AudioManager.Manager.PlaySFX("PlayerFeed");
//AudioManager.Manager.PlaySFX("PlayerTakeDamage");
//AudioManager.Manager.PlaySFX("PlayerDeath");
//AudioManager.Manager.PlaySFX("PlayerHeal");
//AudioManager.Manager.PlaySFX("PlayerDodge");
//AudioManager.Manager.PlaySFX("PlayerLowHealth");
//AudioManager.Manager.PlaySFX("NPC_TakeDamage");
//AudioManager.Manager.PlaySFX("NPC_Death");
//AudioManager.Manager.PlaySFX("NPC_MeleeAttack");
//AudioManager.Manager.PlaySFX("NPC_RangedAttack");
//AudioManager.Manager.PlaySFX("NPC_Downed");
//AudioManager.Manager.PlaySFX("UI_Click");
//AudioManager.Manager.PlaySFX("UI_Loading");
//AudioManager.Manager.PlaySFX("EndGame_Victory");
//AudioManager.Manager.PlaySFX("CoffinOpen");

