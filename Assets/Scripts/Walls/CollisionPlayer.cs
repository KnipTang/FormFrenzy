using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionPlayer : MonoBehaviour
{
    public AudioSource _PublicSoundSource;
    public AudioClip[] _booSound; 
    public AudioClip[] _HitWallSound; 

    public GameObject _efectsPrefab;  

    public SoundManager SoundManager;

    private bool _hitPlayer = false;
    private List<GameObject> _collidedParts = new List<GameObject>();

    private void Start()
    {
        SoundManager = FindObjectOfType<SoundManager>();
        SoundManager.booSound = _booSound;
        SoundManager.HitWallSound = _HitWallSound;
    }

    public bool HitPlayer
    {
        get
        {
            return _hitPlayer;
        }
        set
        {
            _hitPlayer = value;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_collidedParts.Contains(collision.gameObject))
        {
            if (!HitPlayer)
            {
                PlayerShield ps = FindAnyObjectByType<PlayerShield>();
                if (ps != null && !ps.enabled)
                {
                    HealthComponent hc = FindAnyObjectByType<HealthComponent>();
                    if (hc != null)
                    {
                        hc.Damage();
                    }

                    Ragdoll rd = FindAnyObjectByType<Ragdoll>();
                    if (rd != null)
                        rd.EnableRagDoll();

                    if(gameObject.GetComponent<ObstacleMove>() != null)
                    {
                        if (!gameObject.CompareTag("Wall"))
                        {
                            GameObject wall = FindObjectOfType<MoveWall>().gameObject;
                            if (wall != null)
                                wall.GetComponent<MeshRenderer>().enabled = false;
                            MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
                            foreach (MeshRenderer meshRenderer in meshRenderers)
                            {
                                meshRenderer.enabled = false;
                            }
                            Vector3 newPosition = wall.transform.position;
                            newPosition.z = collision.transform.position.z + 5;
                            wall.transform.position = newPosition;
                        }
                    }

                }
                else if (ps != null)
                {
                    if(gameObject.GetComponent<MeshRenderer>() != null)
                        gameObject.GetComponent<MeshRenderer>().enabled = false;
                    MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();

                    foreach (MeshRenderer meshRenderer in meshRenderers)
                    {
                        meshRenderer.enabled = false;
                    }
                    //Destroy(gameObject);
                    //SpawnWall spawnWallScript = FindObjectOfType<SpawnWall>();
                    //spawnWallScript.Spawn();
                    ps.DisableShield();
                }

                HitPlayer = true;

                Instantiate(_efectsPrefab, collision.GetContact(0).point, Quaternion.identity); 


                if (SoundManager != null)
                {
                    SoundManager.booSound = _booSound;
                    SoundManager.HitWallSound = _HitWallSound;

                    if (SoundManager.booSound.Length > 0)
                    {
                        int randomIndex = Random.Range(0, SoundManager.booSound.Length);               
                        SoundManager.PlayBooSound(randomIndex);
                    }

                    if (SoundManager.HitWallSound.Length > 0)
                    {
                        int randomIndex2 = Random.Range(0, SoundManager.HitWallSound.Length);
                        SoundManager.PlayHitWallSound(randomIndex2); 
                    }
                }

                if (FindAnyObjectByType<AudienceAnimation>() != null)
                    StartCoroutine(FindAnyObjectByType<AudienceAnimation>().PlayNegativeAudienceAnimation());

                if (FindAnyObjectByType<CameraAnimations>() != null)
                    FindAnyObjectByType<CameraAnimations>().PlayAnimationClipShake();

                FindAnyObjectByType<DestroyWall>()._comboProgress = 0; 
            }
            _collidedParts.Add(collision.gameObject);
        }
    }
}
