using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreComponent : MonoBehaviour
{
    public void Add(float amount = 1)
    {
        if (FindObjectOfType<ParticlePlus1Wall>() != null)
            FindObjectOfType<ParticlePlus1Wall>().SpawnParticle();

        if (FindObjectOfType<ControllerShake>() != null)
            StartCoroutine(FindObjectOfType<ControllerShake>().VibrateController(0.5f, 0.1f, 0.1f));

        GameStats.Instance.CurrentScore += amount;
    }
}
