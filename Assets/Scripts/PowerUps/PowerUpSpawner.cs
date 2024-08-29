using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{


    [SerializeField] private GameObject[] _powerUps;
    [SerializeField] private float _spawnInterval = 30f; // Time interval between spawns
    [SerializeField] private float _maxSpawnChance = 0.5f; // Maximum chance of spawning a power-up
    [SerializeField] private float _spawnChanceIncreaseRate = 0.1f; // Rate at which spawn chance increases per second
    private float _timeSinceLastSpawn = 0f;
    private float _currentSpawnChance = 0f;
    PowerUpBase _powerUpParent;

    // Start is called before the first frame update
    void Start()
    {
        ResetSpawnChance();
    }

    void FixedUpdate()
    {
        _timeSinceLastSpawn += Time.fixedDeltaTime;

        // Increase spawn chance over time
        _currentSpawnChance = Mathf.Min(_maxSpawnChance, _currentSpawnChance + _spawnChanceIncreaseRate * Time.fixedDeltaTime);

        // Check if it's time to spawn a power-up
        if (_timeSinceLastSpawn >= _spawnInterval)
        {
            if (Random.value < _currentSpawnChance)
            {
                SpawnPowerUp();
                _timeSinceLastSpawn = 0f;
                ResetSpawnChance(); // Reset spawn chance after successful spawn
            }
        }

    }

    private void SpawnPowerUp()
    {
        if (_powerUps.Length == 0)
        {
            Debug.LogWarning("No power-up prefabs assigned to the PowerUpSpawner!");
            return;
        }

        // Choose a random power-up prefab
        int randomIndex = Random.Range(0, _powerUps.Length);
        GameObject powerUpPrefab = _powerUps[randomIndex];

        float randomX = Random.value < 0.5f ? -2f : 2f;
        float randomY = Random.value < 0.5f ? 1f : 3f;
        Vector3 spawnPosition = transform.position + new Vector3(randomX, randomY, 0f);

        Instantiate(powerUpPrefab, spawnPosition, powerUpPrefab.transform.rotation);

       // GameObject uiPrefab = powerUpPrefab.GetComponent<PowerUpParent>().UI_powerUpPrefab;

        //if (uiPrefab != null)
       //     FindAnyObjectByType<UI_PowerUps>().SpawnUIindicator(uiPrefab);
    }

    private void ResetSpawnChance()
    {
        _currentSpawnChance = 0f;
    }
}
