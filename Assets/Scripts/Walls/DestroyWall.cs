using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoundManager))]
[RequireComponent(typeof(ParticleSpawner))]
public class DestroyWall : MonoBehaviour
{

    public GameObject[] _particlesCombo;
    public int _comboProgress = 0;
    public Transform _ParticlesSpawnPoint;

    public SoundManager _SoundManager;



    public AudioSource _PublicSoundSource;
    public ParticleSpawner particleSpawner; 
    public AudioClip[] _CheeeringSoundCombo;
    public AudioClip[] _SuccesfullyClearedaWallCombo;  


    private void Start()
    {
        _SoundManager.cheeringSoundCombo = _CheeeringSoundCombo;
        _SoundManager.successfullyClearedaWallCombo = _SuccesfullyClearedaWallCombo;
        particleSpawner.spawnPoint = _ParticlesSpawnPoint;
        particleSpawner.particlesCombo = _particlesCombo;
        

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            SpawnWall spawnWallScript = FindObjectOfType<SpawnWall>();
            spawnWallScript.Spawn();

            CollisionPlayer cp = collision.gameObject.GetComponent<CollisionPlayer>();
            if (cp != null)
            {
                if (!cp.HitPlayer)
                {
                    if (GameStats.Instance.CurrentLives <= 0)
                        return;

                    ScoreComponent pc = FindAnyObjectByType<ScoreComponent>();
                    if (pc != null)
                    {
                        pc.Add();
                        for (int i = 0; i < _comboProgress; i++)
                        {
                            pc.Add();
                        }

                    
                   }


                    PlayParticleEffects();
                    PlaySoundEffects();

                    _comboProgress++;

                    if (FindAnyObjectByType<AudienceAnimation>() != null)
                        StartCoroutine(FindAnyObjectByType<AudienceAnimation>().PlayPositiveAudienceAnimation());
                }

            }

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("PowerUp"))
        {
            Destroy(collision.gameObject);
        } 
        else if(collision.gameObject.CompareTag("Destroy"))
        {
            Destroy(collision.gameObject);
        }
  
    }
void PlayParticleEffects()
{
        ParticleSpawner Particle = GetComponent<ParticleSpawner>();
        if (Particle != null)
        {

            Particle.SpawnParticleCombo(_comboProgress);


        }

    }
void PlaySoundEffects()
{


        SoundManager soundmanager = FindAnyObjectByType<SoundManager>();
        if (soundmanager != null)
        {
            soundmanager.PlaySucces(_comboProgress);

        }


    }




}


