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

        unitAttackChannel = gameObject.AddComponent<AudioSource>();
        unitAttackChannel.volume = 1f;
        unitAttackChannel.playOnAwake = false;

        unitAttackChannel = gameObject.AddComponent<AudioSource>();
        unitAttackChannel.volume = 1f;
        unitAttackChannel.playOnAwake = false;
        
        //Building Sounds
        unitOtherChannel = gameObject.AddComponent<AudioSource>();
        unitOtherChannel.volume = 1f;
        unitOtherChannel.playOnAwake = false;

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
        if(unitAttackChannel.isPlaying == false)
        {
            otherBuildingChannel.PlayOneShot(sellSound);
        }
    }
    public void PlayConstructBuildingSound()
    {
        if(unitAttackChannel.isPlaying == false)
        {
            constructionBuildingChannel.PlayOneShot(buildingSound);
        }
    }
    public void PlayDestroyBuildingSound()
    {
        if(destructionBuildingChannel.isPlaying == false)
        {
            unitAttackChannel.PlayOneShot(destructionSound);
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



