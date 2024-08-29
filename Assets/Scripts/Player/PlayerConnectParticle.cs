using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConnectParticle : MonoBehaviour
{
    [SerializeField] private GameObject _ParticlePrefab;
    [SerializeField] private Transform _ParticlePos;

    public IEnumerator SpawnParticle()
    {
        GameObject spawnedParticle = Instantiate(_ParticlePrefab, _ParticlePos.position, _ParticlePrefab.transform.rotation);

        yield return new WaitForSeconds(1f);

        Destroy(spawnedParticle);
    }
}
