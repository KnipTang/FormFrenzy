using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUpHeart : PowerUpBase
{
public GameObject _ParticleEffect;
    protected override void PickedUp(Collision collision)

    {
       

        HealthComponent hc = FindObjectOfType<HealthComponent>();
        if(hc)
        {

            particlesSpawner.SpawnParticlePowerUp(collision,1);



            hc.Heal();
        }
    }
}
