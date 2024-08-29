using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _Obstacles;
    
    public void SpawnObsticle(GameObject wall)
    {
        if (_Obstacles.Length == 0)
        {
            Debug.LogWarning("No obstacles found!");
            return;
        }

        GameObject obstacleToSpawn = _Obstacles[Random.Range(0, _Obstacles.Length)];

        float randomX = Random.Range(-2f, 2f);
        float randomY = Random.Range(1f, 3f);
        GameObject obsticle = Instantiate(obstacleToSpawn, new Vector3(randomX, randomY, gameObject.transform.position.z), Quaternion.identity);

        obsticle.GetComponent<ObstacleMove>().SetWall(wall);
    }
}
