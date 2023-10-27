using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;



[System.Serializable]
public class VFX_Controller
{

    public string Name;
    public AudioClip clip;

    [Range(0f, 1f)] public float volume;
    [Range(.1f, 3f)] public float pitch;

    public bool Loop;

    [HideInInspector]
    public AudioSource source;
}

//AudioManager.Manager.Play("PlayerMove");
//AudioManager.Manager.Play("PlayerAttack");
//AudioManager.Manager.Play("PlayerFeed");
//AudioManager.Manager.Play("PlayerTakeDamage");
//AudioManager.Manager.Play("PlayerDeath");
//AudioManager.Manager.Play("PlayerHeal");
//AudioManager.Manager.Play("PlayerDodge");
//AudioManager.Manager.Play("PlayerLowHealth");
//AudioManager.Manager.Play("NPC_TakeDamage");
//AudioManager.Manager.Play("NPC_Death");
//AudioManager.Manager.Play("NPC_MeleeAttack");
//AudioManager.Manager.Play("NPC_RangedAttack");
//AudioManager.Manager.Play("NPC_Downed");
//AudioManager.Manager.Play("NPC_Scream");
//AudioManager.Manager.Play("NPC_Alerted");
//AudioManager.Manager.Play("UI_Click");
//AudioManager.Manager.Play("UI_Loading");
//AudioManager.Manager.Play("EndGame_Victory");

