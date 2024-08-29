using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOV : MonoBehaviour
{
    [SerializeField] private float _StartFOV = 65.5f;
    [SerializeField] private float _EndFOV = 45f;
    [SerializeField] private float _Duration = 1.0f;

    private bool _IsZooming = false;

    private Camera _Camera;

    private void Start()
    {
        _Camera = GetComponent<Camera>();
        if (_Camera != null)
        {
            _Camera.fieldOfView = _StartFOV;
        }
    }

    public void StartFOVTransition()
    {
        if (_Camera != null)
        {
            _IsZooming = true;
        }
    }
    private float CalculateDurationBasedOnDistance()
    {
        if(FindAnyObjectByType<MoveWall>() != null && FindAnyObjectByType<SpawnWall>() != null)
        {
            return (FindAnyObjectByType<MoveWall>().DistanceBetweenPandW) / (FindAnyObjectByType<SpawnWall>().WallSpawnPosition.transform.position.z * -1)  * 100f;
        }
        return 0;
    }

    private void FixedUpdate()
    {
        if (_IsZooming && _Duration < 0f)
        {
            Debug.Log("DecactiveZoomin");
            _IsZooming = false;
            _Camera.fieldOfView = _StartFOV;
        }
        if(_IsZooming)
        {
            _Duration = CalculateDurationBasedOnDistance();
            _Camera.fieldOfView = _EndFOV + (_StartFOV - _EndFOV) * (_Duration / 100f);
        }
    }
}
