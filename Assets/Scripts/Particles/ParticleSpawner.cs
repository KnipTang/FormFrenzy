using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject[] particlesCombo, powerUpParticlesCombo;
    public Transform spawnPoint;

    public void SpawnParticleCombo(int comboProgress)
    {
        if (particlesCombo[Mathf.Clamp(comboProgress, 0, particlesCombo.Length - 1)] != null)
            Instantiate(particlesCombo[Mathf.Clamp(comboProgress, 0,particlesCombo.Length - 1)], spawnPoint.position, spawnPoint.rotation);
    }
    
    public void SpawnParticlePowerUp(Collision collision,int index)
    {
        if(powerUpParticlesCombo[index] != null)
            Instantiate(powerUpParticlesCombo[index], collision.GetContact(0).point, Quaternion.identity);
    } 
 




}