using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class UI_HighScoreBanner : MonoBehaviour
{
    [SerializeField] private GameObject _banner;
    private PlayableDirector _bannerPlayableDirector;

    public GameObject _effectsPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _bannerPlayableDirector = _banner.GetComponent<PlayableDirector>();
    }

    public void PlayerBanner()
    {
        if (_bannerPlayableDirector != null)
            _bannerPlayableDirector.Play();

        StartCoroutine(Particle());
    }

    private IEnumerator Particle()
    {
        GameObject prt = new GameObject { };
        if (_effectsPrefab != null)
            prt = Instantiate(_effectsPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        yield return new WaitForSeconds(2f);

        Destroy(prt);
    }
}
