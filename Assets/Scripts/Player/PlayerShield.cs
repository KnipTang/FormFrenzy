using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : PlayerAbiltiesParent
{
    [SerializeField] private GameObject _GeoCharacter;
    private SkinnedMeshRenderer _SkinnedMeshRenderer;
   // private Material _material;
   // private Color _startBaseColor;
    //private Color _currentBaseColor;

    private void Start()
    {
        _SkinnedMeshRenderer = _GeoCharacter.GetComponent<SkinnedMeshRenderer>();

       //if (skinnedMeshRenderer != null && skinnedMeshRenderer.material != null)
       //{
       //    //_material = skinnedMeshRenderer.material;
       //    //Debug.Log(_material);
       //    //if (_material.HasProperty("_BaseColor"))
       //    //{
       //    //    _startBaseColor = _material.GetColor("_BaseColor");
       //    //    _currentBaseColor = _startBaseColor;
       //    //}
       //}

        DisableShield();
    }
    public void EnableShield()
    {
        enabled = true;
       // _currentBaseColor = Color.blue;
        //_material.SetColor("_BaseColor", _currentBaseColor);
        _SkinnedMeshRenderer.enabled = true;
        StartCoroutine(ShieldTimer());
    }

    private IEnumerator ShieldTimer()
    {
        yield return new WaitForSeconds(IndicatorTime);
        DisableShield();
    }
    public void DisableShield()
    {
        //_material.SetColor("_BaseColor", _startBaseColor);
        _SkinnedMeshRenderer.enabled = false;
        Destroy(FindAnyObjectByType<UI_PowerUps>().Indicator);
        enabled = false;
    }
}
