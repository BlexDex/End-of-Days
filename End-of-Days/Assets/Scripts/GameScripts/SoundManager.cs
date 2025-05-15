using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    public AudioMixerGroup masterMixer;

    [Header("Units : Hummanoid")]
    public AudioClip unitSelectedClip;
    public AudioClip unitCommandedClip;
    public AudioClip meleeAttackClip;
    public AudioClip rangedAttackClip;
    public AudioClip unitDeathClip;

    [Header("Units : Other")]
    public AudioClip dogAttackClip;
    public AudioClip dogSelectedClip;
    public AudioClip dogCommandedClip;
    public AudioClip dogDeathClip;

    [Header("Units : Enemies")]
    public AudioClip zombieAttackClip;
    public AudioClip zombieDeathClip;
    public AudioClip zombieSpawnClip;

    private AudioSource unitAttackChannel;
    private AudioSource unitDeathChannel;
    private AudioSource unitOtherChannel;
    private AudioSource zombieChannel;
    private AudioSource ambientChannel;
    [Header("Buildings")]
    public AudioClip sellSound;
    public AudioClip buildingSound;
    public AudioClip destructionSound;

    private AudioSource constructionBuildingChannel;
    private AudioSource destructionBuildingChannel;
    private AudioSource otherBuildingChannel;


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

        //Unit Sounds
        unitAttackChannel = gameObject.AddComponent<AudioSource>();
        unitAttackChannel.volume = 1f;
        unitAttackChannel.playOnAwake = false;
        unitAttackChannel.outputAudioMixerGroup = masterMixer;

        unitOtherChannel = gameObject.AddComponent<AudioSource>();
        unitOtherChannel.volume = 1f;
        unitOtherChannel.playOnAwake = false;
        unitOtherChannel.outputAudioMixerGroup = masterMixer;

        unitDeathChannel = gameObject.AddComponent<AudioSource>();
        unitDeathChannel.volume = 1f;
        unitDeathChannel.playOnAwake = false;
        unitDeathChannel.outputAudioMixerGroup = masterMixer;

        zombieChannel = gameObject.AddComponent<AudioSource>();
        zombieChannel.volume = 1f;
        zombieChannel.playOnAwake = false;
        zombieChannel.outputAudioMixerGroup = masterMixer;

        ambientChannel = gameObject.AddComponent<AudioSource>();
        ambientChannel.volume = 1f;
        ambientChannel.playOnAwake = false;
        ambientChannel.outputAudioMixerGroup = masterMixer;


        //Building Sounds

        constructionBuildingChannel = gameObject.AddComponent<AudioSource>();
        constructionBuildingChannel.volume = 1f;
        constructionBuildingChannel.playOnAwake = false;
        constructionBuildingChannel.outputAudioMixerGroup = masterMixer;

        destructionBuildingChannel = gameObject.AddComponent<AudioSource>();
        destructionBuildingChannel.volume = 1f;
        destructionBuildingChannel.playOnAwake = false;
        destructionBuildingChannel.outputAudioMixerGroup = masterMixer;

        otherBuildingChannel = gameObject.AddComponent<AudioSource>();
        otherBuildingChannel.volume = 1f;
        otherBuildingChannel.playOnAwake = false;
        otherBuildingChannel.outputAudioMixerGroup = masterMixer;

    }

    public void PlayMeleeAttackSound()
    {
        if (unitAttackChannel.isPlaying == false)
        {
            unitAttackChannel.PlayOneShot(meleeAttackClip);
        }
    }
    public void PlayZombieAttackSound()
    {
        if (zombieChannel.isPlaying == false)
        {
            zombieChannel.PlayOneShot(zombieAttackClip);
        }
    }
    public void PlayDogAttackSound()
    {
        if (unitAttackChannel.isPlaying == false)
        {
            unitAttackChannel.PlayOneShot(dogAttackClip);
        }
    }
    public void PlayRangedAttackSound()
    {
        if (unitAttackChannel.isPlaying == false)
        {
            unitAttackChannel.PlayOneShot(rangedAttackClip);
        }
    }
    public void PlaySellingBuildinSound()
    {
        if (otherBuildingChannel.isPlaying == false)
        {
            otherBuildingChannel.PlayOneShot(sellSound);
        }
    }
    public void PlayConstructBuildingSound()
    {
        if (constructionBuildingChannel.isPlaying == false)
        {
            constructionBuildingChannel.PlayOneShot(buildingSound);
        }
    }
    public void PlayDestroyBuildingSound()
    {
        if (destructionBuildingChannel.isPlaying == false)
        {
            destructionBuildingChannel.PlayOneShot(destructionSound);
        }
    }

    public void PlaySelectedSound()
    {
        if (unitOtherChannel.isPlaying == false)
        {
            unitOtherChannel.PlayOneShot(unitSelectedClip);
        }
    }

    public void PlayDogSelectedSound()
    {
        if (unitOtherChannel.isPlaying == false)
        {
            unitOtherChannel.PlayOneShot(dogSelectedClip);
        }
    }

    public void PlayCommandedSound()
    {
        if (unitOtherChannel.isPlaying == false)
        {
            unitOtherChannel.PlayOneShot(unitCommandedClip);
        }
    }
    public void PlayDogCommandedSound()
    {
        if (unitOtherChannel.isPlaying == false)
        {
            unitOtherChannel.PlayOneShot(dogCommandedClip);
        }
    }

    public void PlayDogDeathSound()
    {
        if (unitDeathChannel.isPlaying == false)
        {
            unitDeathChannel.PlayOneShot(dogDeathClip);
        }
    }

    public void PlayUnitDeathSound()
    {
        if (unitDeathChannel.isPlaying == false)
        {
            unitDeathChannel.PlayOneShot(unitDeathClip);
        }
    }

    public void PlayZombieDeathSound()
    {
        if (zombieChannel.isPlaying == false)
        {
            zombieChannel.PlayOneShot(zombieDeathClip);
        }
    }
    
    public void PlaySpawnSound()
    {
        if (ambientChannel.isPlaying == false)
        {
            ambientChannel.PlayOneShot(zombieSpawnClip);
        }
    }
}



