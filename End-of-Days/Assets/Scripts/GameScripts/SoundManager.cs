using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {get; set;}

    [Header("Units")]
    private AudioSource unitAttackChannel;
    private AudioSource unitOtherChannel;
    public AudioClip meleeAttackClip;
    public AudioClip unitSelectedClip;
    public AudioClip unitCommandedClip;
    [Header("Buildings")]
    private AudioSource constructionBuildingChannel;
    private AudioSource destructionBuildingChannel;
    private AudioSource otherBuildingChannel;

    public AudioClip sellSound;
    public AudioClip buildingSound;
    public AudioClip destructionSound;

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

        unitOtherChannel = gameObject.AddComponent<AudioSource>();
        unitOtherChannel.volume = 1f;
        unitOtherChannel.playOnAwake = false;
        
        //Building Sounds

        constructionBuildingChannel = gameObject.AddComponent<AudioSource>();
        constructionBuildingChannel.volume = 1f;
        constructionBuildingChannel.playOnAwake = false;

        destructionBuildingChannel = gameObject.AddComponent<AudioSource>();
        destructionBuildingChannel.volume = 1f;
        destructionBuildingChannel.playOnAwake = false;

        otherBuildingChannel = gameObject.AddComponent<AudioSource>();
        otherBuildingChannel.volume = 1f;
        otherBuildingChannel.playOnAwake = false;

    }

    public void PlayMeleeAttackSound()
    {
        if(unitAttackChannel.isPlaying == false)
        {
            unitAttackChannel.PlayOneShot(meleeAttackClip);
        }
    }
    public void PlaySellingBuildinSound()
    {
        if(otherBuildingChannel.isPlaying == false)
        {
            otherBuildingChannel.PlayOneShot(sellSound);
        }
    }
    public void PlayConstructBuildingSound()
    {
        if(constructionBuildingChannel.isPlaying == false)
        {
            constructionBuildingChannel.PlayOneShot(buildingSound);
        }
    }
    public void PlayDestroyBuildingSound()
    {
        if(destructionBuildingChannel.isPlaying == false)
        {
            destructionBuildingChannel.PlayOneShot(destructionSound);
        }
    }

        public void PlaySelectedSound()
    {
        if(unitOtherChannel.isPlaying == false)
        {
            unitOtherChannel.PlayOneShot(unitSelectedClip);
        }
    }
    public void PlayCommandedSound()
    {
        if(unitOtherChannel.isPlaying == false)
        {
            unitOtherChannel.PlayOneShot(unitCommandedClip);
        }
    }
}



