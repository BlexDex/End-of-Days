using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {get; set;}

    private AudioSource unitAttackChannel;
    public AudioClip meleeAttackClip;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        unitAttackChannel = gameObject.AddComponent<AudioSource>();
        unitAttackChannel.volume = 0.1f;
        unitAttackChannel.playOnAwake = false;
    }

    public void PlayMeleeAttackSound()
    {
        if(unitAttackChannel.isPlaying == false)
        {
            unitAttackChannel.PlayOneShot(meleeAttackClip);
        }
    }
}


