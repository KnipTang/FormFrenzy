using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;

public class WallWithWeight
{
    public GameObject _wall;
    public float _weight = 0;
}

public class SpawnWall : MonoBehaviour
{
    [SerializeField] private GameObject _wallSpawnPosition;
    [SerializeField] private GameObject _cloth;
    private PlayableDirector _clothPlayableDirector;

    private int _randomIndex = 0;
    private int _lastSpawnedWallIndex = 0;

    [SerializeField] private GameObject[] _wallList;
    [SerializeField] public float _minCurrentWeight = 0;
    [SerializeField] public float _maxCurrentWeight = 1;
    [SerializeField] private float _weightIncrease = 0.1f;

    [SerializeField] private float _maxWeight = 10f;

    private WallWithWeight[] _wallListWithWeight;

    private List<GameObject> _currentSpawnableWalls; //walls that have a weight between min/max wight

    [SerializeField] private float _speedIncrease = 0.05f;

    private bool _ObstricleSpawned = false;

    [SerializeField] private GameObject _Button;
    [SerializeField] private int _BlockerChance = 5;

    public GameObject WallSpawnPosition
    {
        get { return _wallSpawnPosition; }
    }
    private Vector3 _PlayerPos;
    public Vector3 PlayerPos
    {
        get { return _PlayerPos; }
        set { _PlayerPos = value; }
    }
    public float CurrentWeightAverage
    {
        get
        {
            return (_minCurrentWeight + _maxCurrentWeight) / 2;
        }
    }
    void Start()
    {
        GameObject player = FindAnyObjectByType<HealthComponent>().gameObject;
        PlayerPos = player.transform.position;

        _wallListWithWeight = new WallWithWeight[_wallList.Length];

        for (int i = 0; i < _wallList.Length; i++)
        {
            _wallListWithWeight[i] = new WallWithWeight();
        }

        for (int i = 0; i < _wallList.Length; i++)
        {
            _wallListWithWeight[i]._wall = _wallList[i];
            _wallListWithWeight[i]._weight = _wallList[i].GetComponent<WallStats>().GetWeight();
        }

        _clothPlayableDirector = _cloth.GetComponent<PlayableDirector>();
        Spawn();
    }

    private void CheckWallsWithinWeightRange()
    {
        _currentSpawnableWalls = new List<GameObject>();

        for (int i = 0; i < _wallListWithWeight.Length; i++)
        {
            float wallWeight = _wallListWithWeight[i]._weight;
            if (wallWeight >= _minCurrentWeight && wallWeight <= _maxCurrentWeight)
            {
                _currentSpawnableWalls.Add(_wallListWithWeight[i]._wall);
            }
        }
    }

    public void Spawn()
    {
        _clothPlayableDirector.time = 0; 
        _clothPlayableDirector.Play();

        CheckWallsWithinWeightRange();

        int lengthCurrentWalls = _currentSpawnableWalls.Count;


        _randomIndex = UnityEngine.Random.Range(0, lengthCurrentWalls);

        Debug.Log("CurrentLength: " + lengthCurrentWalls);
        Debug.Log("Random: " + _randomIndex);
        Debug.Log("LastSpawned: " + _lastSpawnedWallIndex);

        if (lengthCurrentWalls > 1)
            while(_randomIndex == _lastSpawnedWallIndex)
                _randomIndex = UnityEngine.Random.Range(0, lengthCurrentWalls);
        else
            _randomIndex = 0;

        GameObject wall = Instantiate(_currentSpawnableWalls[_randomIndex], _wallSpawnPosition.transform.position, Quaternion.identity);

        if(wall == null)
            wall = Instantiate(_wallList[0], _wallSpawnPosition.transform.position, Quaternion.identity);

        if(_maxCurrentWeight < _maxWeight)
        {
            _minCurrentWeight += _weightIncrease;
            _maxCurrentWeight += _weightIncrease;
        }

        //SpawnObsticle
        if(CurrentWeightAverage > 1)
        {
            int rndChance = UnityEngine.Random.Range(0, 5);

            if(rndChance < 1)
            {
                FindAnyObjectByType<ObstacleSpawner>().SpawnObsticle(wall);
                _ObstricleSpawned = true;
            }
        }

        //SpawnBlocker
        if(CurrentWeightAverage > 1.5f && !_ObstricleSpawned)
        {
            int rndChance = UnityEngine.Random.Range(0, _BlockerChance);
           
            if( rndChance < 1)
            {
                GameObject blocker = wall.GetComponentInChildren<RotateBlocker>().gameObject;
                blocker.GetComponent<MeshRenderer>().enabled = true;
                blocker.GetComponent<BoxCollider>().enabled = true;

                float randomX = UnityEngine.Random.Range(-2f, 2f);
                float randomY = UnityEngine.Random.Range(1f, 3f);
                Instantiate(_Button, new Vector3(randomX, randomY, _wallSpawnPosition.transform.position.z), _Button.transform.rotation);
            }

        }

        if(FindAnyObjectByType<CameraFOV>() != null)
        {
            FindAnyObjectByType<CameraFOV>().StartFOVTransition();
        }

        _ObstricleSpawned = false;
        GameStats.Instance.WallSpeedIncrease += _speedIncrease;
        _lastSpawnedWallIndex = _randomIndex;
    }
}
