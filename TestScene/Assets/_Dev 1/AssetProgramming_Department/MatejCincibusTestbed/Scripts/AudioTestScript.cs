using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTestScript : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            AudioManager.Instance.PlaySoundEffect(AudioManager.SoundEffects.PlayerAttack);
            Debug.Log("Sword slashed!");
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            AudioManager.Instance.PlaySoundEffect(AudioManager.SoundEffects.PlayerTakeDamage);
            Debug.Log("Damage taken!");
        }
    }
}
