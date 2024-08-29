using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastWall : MonoBehaviour
{
    private float _maxDistance = 40f;
    private RaycastHit _hitInfo;
    private Vector3 _origin;
    private Vector3 _direction;

    Rigidbody _rb;

    [SerializeField] private GameObject _endPosColliderObject;
    private Vector3 _endPosCollider;

    private float _collisionRadius;
    private Vector3 _gameobjectGlobalScale;

    private Collider _collider;
    private void Start()
    {
        _endPosCollider = _endPosColliderObject.transform.position;

        _rb = gameObject.GetComponent<Rigidbody>();
        _direction = Vector3.back;

        _gameobjectGlobalScale = gameObject.transform.lossyScale;

        _collider = _rb.GetComponent<Collider>();

        if(_collider != null)
        {
            if (_collider.GetType() == typeof(CapsuleCollider))
            {
                CapsuleCollider capsuleCollider = (CapsuleCollider)_collider;
                _collisionRadius = capsuleCollider.radius * _gameobjectGlobalScale.x;
            }
            else if (_collider.GetType() == typeof(SphereCollider))
            {
                SphereCollider spehereCollider = (SphereCollider)_collider;
                _collisionRadius = spehereCollider.radius * _gameobjectGlobalScale.x;
            }
            else if (_collider.GetType() == typeof(BoxCollider))
            {
                BoxCollider boxCollider = (BoxCollider)_collider;
                _collisionRadius = boxCollider.size.x * _gameobjectGlobalScale.x;
            }
        }
    }
    public bool HitsWall()
    {
        _origin = gameObject.transform.position;
        _endPosCollider = _endPosColliderObject.transform.position;
       
        return castRay(_origin);
    }
    
    private bool castRay(Vector3 origin)
    {
        bool raycast = false;

        if (_collider != null)
        {
            if (_collider.GetType() == typeof(CapsuleCollider))
            {
                raycast = Physics.CapsuleCast(_origin, _endPosCollider, _collisionRadius, _direction, out _hitInfo, _maxDistance);
            }
            else if (_collider.GetType() == typeof(SphereCollider))
            {
                _origin.y = origin.y + _collisionRadius;
                raycast = Physics.SphereCast(_origin, _collisionRadius, _direction, out _hitInfo, _maxDistance);
            }
            else if (_collider.GetType() == typeof(BoxCollider))
            {
                Debug.DrawRay(_origin, _direction * 10, Color.blue);
                Debug.DrawRay(_endPosCollider, _direction * 10, Color.green);
                Debug.DrawLine(_origin, _endPosCollider, Color.red);
                Debug.DrawLine(_origin, new Vector3(_origin.x + _collisionRadius, _origin.y, _origin.z), Color.red);
                raycast = Physics.CapsuleCast(_origin, _endPosCollider, _collisionRadius/2, _direction, out _hitInfo, _maxDistance);
            }
        }

        if (raycast)
        {
            if (_hitInfo.collider.CompareTag("Wall"))
            {
                return true;
            }
        }

        return false;
    }
}
