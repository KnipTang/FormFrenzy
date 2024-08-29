using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShield : PowerUpBase
{
    public GameObject _ParticleEffect;
    protected override void PickedUp(Collision collision)
    {
        PlayerShield ps = FindObjectOfType<PlayerShield>();
        if (ps)
        {
            //particlr spawnver is on the parent version 
            particlesSpawner.SpawnParticlePowerUp(collision,0);

            ps.EnableShield();

        }
    }
}
