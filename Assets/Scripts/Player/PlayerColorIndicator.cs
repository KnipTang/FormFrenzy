using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorIndicator : PlayerAbiltiesParent
{
    [SerializeField] private GameObject _GeoCharacter;
    private Material _material;
    private Color _startBaseColor;
    private Color _currentBaseColor;

    // Start is called before the first frame update
    void Start()
    {
        SkinnedMeshRenderer skinnedMeshRenderer = _GeoCharacter.GetComponent<SkinnedMeshRenderer>();

        if (skinnedMeshRenderer != null && skinnedMeshRenderer.material != null)
        {
            _material = skinnedMeshRenderer.material;

            if (_material.HasProperty("_BaseColor"))
            {
                _startBaseColor = _material.GetColor("_BaseColor");
                _currentBaseColor = _startBaseColor;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        RayCastWall[] rayCasts = FindObjectsOfType<RayCastWall>();
        int hitAmount = 0;

        foreach (var rayCast in rayCasts)
        {
            if (rayCast.HitsWall())
                hitAmount++;
        }

        _currentBaseColor = _startBaseColor;
        if (hitAmount == 0)
        {
            _currentBaseColor.g += 2 / 10f;
        }
        else
        {
            _currentBaseColor.r += ((float)hitAmount / (float)rayCasts.Length) *2;
        }

        _material.SetColor("_BaseColor", _currentBaseColor);
    }

    public void ShowIndicator()
    {
        enabled = true;

        StartCoroutine(DisableIndicator());
    }

    private IEnumerator DisableIndicator()
    {
        yield return new WaitForSeconds(IndicatorTime);
        _material.SetColor("_BaseColor", _startBaseColor);
        enabled = false;
    }
}
