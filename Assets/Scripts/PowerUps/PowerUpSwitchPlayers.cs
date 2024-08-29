using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSwitchPlayers : PowerUpBase
{
    public GameObject _ParticleEffect;
    protected override void PickedUp(Collision collision)
    {
        MovementParent mp = FindObjectOfType<MovementParent>();
        if (mp)
        {
            particlesSpawner.SpawnParticlePowerUp(collision,0);

            mp.SwitchPlayerIndex();
        }
    }
}
