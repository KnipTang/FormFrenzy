using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpColorIndicator : PowerUpBase
{
    protected override void PickedUp(Collision collision)
    {
        PlayerColorIndicator pci = FindObjectOfType<PlayerColorIndicator>();
        if (pci != null)
        {


            particlesSpawner.SpawnParticlePowerUp(collision,2);
            pci.ShowIndicator();

        }
    }
}
