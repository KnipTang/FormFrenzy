using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DisableRagDoll();
    }

    public void DisableRagDoll()
    {
        Rigidbody[] rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in rigidbodies)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void EnableRagDoll()
    {
        Rigidbody[] rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in rigidbodies)
        {
            rb.constraints = RigidbodyConstraints.None;
        }

        if (GameStats.Instance.CurrentLives <= 0) return;

        Respawn respawnPlayer = FindAnyObjectByType<Respawn>();

        if (respawnPlayer != null)
        {
            StartCoroutine(respawnPlayer.RespawnPlayer());
        }
    }
}
