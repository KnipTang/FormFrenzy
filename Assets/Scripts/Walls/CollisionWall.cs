using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudienceAnimation))]
public class CollisionWall : MonoBehaviour
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
    public void CollisionWithPlayer(GameObject gameObject ,Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_collidedParts.Contains(collision.gameObject))
        {
            if (!HitPlayer)
            {
                PlayerShield ps = FindAnyObjectByType<PlayerShield>();
                if (ps != null && !ps.enabled)
                {
                    Ragdoll rd = FindAnyObjectByType<Ragdoll>();
                    if (rd != null)
                        rd.EnableRagDoll();

                    HealthComponent hc = FindAnyObjectByType<HealthComponent>();
                    if (hc != null)
                    {
                        hc.Damage();
                    }
                }
                else if (ps != null)
                {
                    Destroy(gameObject);
                    SpawnWall spawnWallScript = FindObjectOfType<SpawnWall>();
                    spawnWallScript.Spawn();
                    ps.DisableShield();
                }

                HitPlayer = true;

                Instantiate(_efectsPrefab, collision.GetContact(0).point, Quaternion.identity);

                PlaySound();



                if (FindAnyObjectByType<AudienceAnimation>() != null)
                    StartCoroutine(FindAnyObjectByType<AudienceAnimation>().PlayNegativeAudienceAnimation());



                FindAnyObjectByType<DestroyWall>()._comboProgress = 0; 
            }
            _collidedParts.Add(collision.gameObject);
        }
    }


    void PlaySound()
    {

        if (SoundManager != null)
        {
       
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


    }




}
