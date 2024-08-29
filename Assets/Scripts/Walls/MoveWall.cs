using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    private float _speed;
    private float _speedIncrease;
    private float _slowDistance = 15;
    private float _speedUpDistance = -0.5f;
    private float _slowSpeed = 1.0f;
    private Vector3 playerPos;

    public AnimationCurve movementCurve;
    public AnimationCurve fadingCurve;
    private float _time;
    private float _fadingTime;

    public float _fadeValue;

    private List<Material> _materials;

    private bool _PassedPlayer;

    private float _DistanceBetweenPandW = 30f;
    public float DistanceBetweenPandW
    {
        get { return _DistanceBetweenPandW; }
        set { _DistanceBetweenPandW = value; }
    }

    private void Start()
    {
        _speed = GameStats.Instance.WallSpeed;
        _speedIncrease = GameStats.Instance.WallSpeedIncrease;
        playerPos = FindAnyObjectByType<SpawnWall>().PlayerPos;

        _materials = new List<Material>();

        Renderer rendererParent = GetComponent<Renderer>();
        if (rendererParent != null)
        {
            _materials.Add(rendererParent.material);
        }
        else
        {
            Debug.LogError("No Renderer component found.");
        }

        Renderer[] childRenderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in childRenderers)
        {
            _materials.Add(renderer.material);
        }

        DistanceBetweenPandW = Mathf.Abs(playerPos.z - gameObject.transform.position.z);
    }
    void FixedUpdate()
    {
        transform.position += Vector3.forward * (_speed + _speedIncrease) * Time.deltaTime;

        DistanceBetweenPandW = Mathf.Abs(playerPos.z - gameObject.transform.position.z);
      
        if (DistanceBetweenPandW < _slowDistance)
        {
            _speed = movementCurve.Evaluate(_time);
            _time += Time.deltaTime;

        }
        if(playerPos.z - gameObject.transform.position.z < _speedUpDistance)
        {
            if(!_PassedPlayer)
            {
                _PassedPlayer = true;
            }

            _fadeValue = fadingCurve.Evaluate(_fadingTime);
            _fadingTime += Time.deltaTime;

            foreach (Material material in _materials)
            {
                if (material.HasProperty("_Transparency"))
                    material.SetFloat("_Transparency", _fadeValue);
            }
            _speed = GameStats.Instance.WallSpeed;
        }
    }
}
