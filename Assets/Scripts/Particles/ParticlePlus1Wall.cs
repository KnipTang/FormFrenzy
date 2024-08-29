using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlus1Wall : MonoBehaviour
{
    [SerializeField] private GameObject _ParticlePrefab;
    [SerializeField] private Transform _ParticlePos;
    [SerializeField] private float _ParticleTime = 1f;

    public void SpawnParticle()
    {
        StartCoroutine(SpawnWithTimer());
    }

    private IEnumerator SpawnWithTimer()
    {
        Debug.Log("Plus1");
        GameObject spawnedParticle = Instantiate(_ParticlePrefab, _ParticlePos.position, _ParticlePrefab.transform.rotation);

        yield return new WaitForSeconds(_ParticleTime);

        Destroy(spawnedParticle);
    }
}
