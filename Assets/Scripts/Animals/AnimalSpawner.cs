using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _Animals;
    [SerializeField] private float spawnInterval = 30f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnAnimal), spawnInterval, spawnInterval);
    }
    public void SpawnAnimal()
    {
        int rndAnimalIndex = Random.Range(0, _Animals.Length);

        float randomX = Random.Range(-5f, 5f);
        float randomZ = Random.Range(-10f, -5f);
        Vector3 randomPosition = new Vector3(randomX, 0f, randomZ);

        float randomY = Random.Range(0f, 360f);
        Quaternion randomRotation = Quaternion.Euler(0f, randomY, 0f);

        GameObject spawnedAnimal = Instantiate(_Animals[rndAnimalIndex], randomPosition, randomRotation);

        Animator animator = spawnedAnimal.GetComponent<Animator>();

        if (animator != null)
        {
            AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            if (clipInfo.Length > 0)
            {
                float animationLength = clipInfo[0].clip.length;
                Destroy(spawnedAnimal, animationLength);
            }
        }
    }


}
