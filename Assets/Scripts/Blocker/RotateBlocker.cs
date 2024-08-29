using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBlocker : MonoBehaviour
{
    [SerializeField] private float _TargetRotation = 0f;
    private float _Speed = 3;
    public bool _IsRotating = false;

    private void Update()
    {
        if (_IsRotating)
        {
            RotateAway();
        }
    }

    private void RotateAway()
    {
        Quaternion targetRotation = Quaternion.Euler(0, _TargetRotation, 0);

        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, targetRotation, _Speed * Time.deltaTime);

        if (Quaternion.Angle(gameObject.transform.rotation, targetRotation) < 0.1f)
        {
            _IsRotating = false;
        }
    }

    public void StartRotation()
    {
        _IsRotating = true;
        GetComponent<BoxCollider>().enabled = false;
    }
}
