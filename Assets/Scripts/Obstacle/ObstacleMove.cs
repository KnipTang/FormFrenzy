using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    [SerializeField] private float _distanceFromWall = 20f;
    private GameObject _wallInfront;
    // Start is called before the first frame update
    void Start()
    {
        _wallInfront = GameObject.FindWithTag("Wall");
    }
    
    public void SetWall(GameObject wall)
    {
        _wallInfront = wall;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_wallInfront != null)
            transform.position = new Vector3(transform.position.x, transform.position.y, _wallInfront.transform.position.z);
        else
            transform.position += Vector3.forward * GameStats.Instance.PowerUpSpeed * Time.deltaTime;
    }
}
