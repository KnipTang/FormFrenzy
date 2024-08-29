using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource soundSource;
    public AudioClip[] cheeringSoundCombo;
    public AudioClip[] successfullyClearedaWallCombo;
    public AudioClip[] booSound; 
    public AudioClip[] HitWallSound;  


    public void PlayCheeringSound(int comboProgress)
    {
        soundSource.PlayOneShot(cheeringSoundCombo[Mathf.Min(2, comboProgress)]);
    }

    public void PlaySuccessSound(int comboProgress)
    {
        soundSource.PlayOneShot(successfullyClearedaWallCombo[Mathf.Min(2, comboProgress)]);
    }

    public void PlayBooSound(int index)
    {
        if (index >= 0 && index < booSound.Length)
            soundSource.PlayOneShot(booSound[index]);
    }

    public void PlayHitWallSound(int index)
    {
        if (index >= 0 && index < HitWallSound.Length)
            soundSource.PlayOneShot(HitWallSound[index]);



    }
    public void PlaySucces(int comboProgress)
    {
        soundSource.PlayOneShot(cheeringSoundCombo[Mathf.Min(2, comboProgress)]);
        soundSource.PlayOneShot(successfullyClearedaWallCombo[Mathf.Min(2, comboProgress)]);


    }

    public void PlayFailed(int index)
    {
        if (index >= 0 && index < HitWallSound.Length)

            soundSource.PlayOneShot(HitWallSound[index]);
            soundSource.PlayOneShot(booSound[index]);



    }




}