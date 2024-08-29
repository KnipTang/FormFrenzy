using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _RespawnParticle;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(3f);

        Ragdoll rd = FindAnyObjectByType<Ragdoll>();
        rd.DisableRagDoll();

        StartCoroutine(Particle());

        StartCoroutine(AnimatorToggle());

        //if(FindAnyObjectByType<MovementParent>() != null)
        //    RestartGame();
    }
    public IEnumerator AnimatorToggle()
    {
        _animator.enabled = true;

        yield return new WaitForSeconds(0.01f);

        _animator.enabled = false;
    }
    private IEnumerator Particle()
    {
        GameObject prt = new GameObject { };
        if (_RespawnParticle != null)
            prt = Instantiate(_RespawnParticle, gameObject.transform.position, gameObject.transform.rotation);

        yield return new WaitForSeconds(3f);

        Destroy(prt);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        GameStats.Instance.ResetStats();
        // Reload the scene by loading the current scene index
        SceneManager.LoadScene(currentSceneIndex);
    }
}
