using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTestScript : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            AudioManager.Instance.PlaySoundEffect(AudioManager.SoundEffects.SwordSlash);
            Debug.Log("Sword slashed!");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            AudioManager.Instance.PlaySoundEffect(AudioManager.SoundEffects.TakeDamage);
            Debug.Log("Damage taken!");
        }
    }
}
