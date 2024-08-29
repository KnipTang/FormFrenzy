using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField] private float _distanceFromWall = 20f;
    [SerializeField] private GameObject _CollisionParticle;
    private GameObject _Wall;

    private void Start()
    {
        _Wall = GameObject.FindWithTag("Wall");
    }
    private void OnCollisionEnter(Collision collision)
    {
        FindAnyObjectByType<RotateBlocker>().StartRotation();

        gameObject.GetComponent<MeshRenderer>().enabled = false;

        StartCoroutine(Particle());
    }

    private void FixedUpdate()
    {
        if(_Wall != null)
            transform.position = new Vector3(transform.position.x, transform.position.y, _Wall.transform.position.z + _distanceFromWall);
        else
            transform.position += Vector3.forward * GameStats.Instance.PowerUpSpeed * Time.deltaTime;

    }

    private IEnumerator Particle()
    {
        GameObject prt = new GameObject { };
        if (_CollisionParticle != null)
            prt = Instantiate(_CollisionParticle, gameObject.transform.position, gameObject.transform.rotation);

        yield return new WaitForSeconds(3f);

        Destroy(prt);
    }
}
