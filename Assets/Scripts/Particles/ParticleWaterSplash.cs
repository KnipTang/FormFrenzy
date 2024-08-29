using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleWaterSplash : MonoBehaviour
{
    [SerializeField] private GameObject _WaterParticle;
    private Vector3 _CollisionPos;
    private bool _WasHit;

    private void OnTriggerEnter(Collider other)
    {
        if (_WasHit) return;
        if(other.CompareTag("Player"))
        {
            _WasHit = true;

            _CollisionPos = other.gameObject.transform.position;
            StartCoroutine(SpawnParticle());
        }
    }

    public IEnumerator SpawnParticle()
    {
        GameObject spawnedParticle = Instantiate(_WaterParticle, _CollisionPos, _WaterParticle.transform.rotation);

        yield return new WaitForSeconds(1f);

        Destroy(spawnedParticle);

        yield return new WaitForSeconds(3f);

        _WasHit = false;
    }
}
