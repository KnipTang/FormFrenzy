using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : MonoBehaviour
{
    public float _distanceFromWall = 20f;

    public ParticleSpawner particlesSpawner;  //this is new 


    private GameObject _wallInfront;

    private bool _powerUpPickedUp = false;

    [SerializeField] private GameObject _UI_powerUpPrefab;

    public GameObject UI_powerUpPrefab
    {
        get { return _UI_powerUpPrefab; }
    }
    private void Start()
    {
        if (FindAnyObjectByType<MoveWall>() != null)
            _wallInfront = FindAnyObjectByType<MoveWall>().gameObject;
        if(FindObjectOfType<ParticleSpawner>() != null)
            particlesSpawner = FindObjectOfType<ParticleSpawner>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (_powerUpPickedUp) return;
        if(collision.gameObject.CompareTag("Player"))
        {
            _powerUpPickedUp = true;
            FindAnyObjectByType<UI_PowerUps>().SpawnUIindicator(UI_powerUpPrefab);

            FindAnyObjectByType<CameraAnimations>().PlayAnimationClipPickUpShake();

            PickedUp(collision);
            Destroy(gameObject);
        }
    }

    protected virtual void PickedUp(Collision collision) {}    

    private void FixedUpdate()
    {
        if (_wallInfront != null)
            transform.position = new Vector3(transform.position.x, transform.position.y, _wallInfront.transform.position.z - _distanceFromWall);
        else
            transform.position += Vector3.forward * GameStats.Instance.PowerUpSpeed * Time.deltaTime;
    }
}
