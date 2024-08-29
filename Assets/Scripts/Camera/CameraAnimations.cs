using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraAnimations : MonoBehaviour
{
    [SerializeField] private Animator _Animator;
    [SerializeField] private AnimationClip _AnimationClipShake;
    [SerializeField] private AnimationClip _AnimationClipPickUpShake;

    private ControllerShake _ControllerShake;

    private void Start()
    {
        _ControllerShake = FindAnyObjectByType<ControllerShake>();
    }

    public void PlayAnimationClipShake()
    {
        if (_AnimationClipShake == null)
        {
            Debug.LogError("Animation clip is null.");
            return;
        }

        StartCoroutine(_ControllerShake.VibrateController(1f, 1f, 1f));

        _Animator.Play("EmptyState");
        _Animator.Play(_AnimationClipShake.name, -1, 0f);
    }

    public void PlayAnimationClipPickUpShake()
    {
        if (_AnimationClipPickUpShake == null)
        {
            Debug.LogError("Animation clip is null.");
            return;
        }

        StartCoroutine(_ControllerShake.VibrateController(0.5f, 0.5f, 0.5f));

        _Animator.Play("EmptyState");
        _Animator.Play(_AnimationClipPickUpShake.name, -1, 0f);
    }
}
