using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_PowerUps : MonoBehaviour
{
    [SerializeField] private Transform _UI_PowerUpPosition;

    private GameObject _indicator;

    public GameObject Indicator
    {
        get { return _indicator; }
    }

    private float _StartTime;
    private float _CurrentTime;
    private float _SliderValue;
    public void SpawnUIindicator(GameObject UI_powerUpPrefab)
    {
        if (UI_powerUpPrefab != null)
        {
            _indicator = Instantiate(UI_powerUpPrefab, _UI_PowerUpPosition);
            _indicator.transform.position = _UI_PowerUpPosition.position;

            _StartTime = FindAnyObjectByType<PlayerAbiltiesParent>().IndicatorTime;
            _CurrentTime = _StartTime;

            _SliderValue = _indicator.GetComponent<Slider>().value;
        }
        else
        {
            Debug.LogWarning("UI prefab is not assigned!");
        }
    }
    private void FixedUpdate()
    {
        if(_indicator != null )
        {
            _CurrentTime -= Time.deltaTime;
            _SliderValue = _CurrentTime / _StartTime;

            Debug.Log(_CurrentTime / _StartTime);

            if (_SliderValue < 0)
            {
                Destroy(_indicator);
                return;
            }

            _indicator.GetComponent<Slider>().value = _SliderValue;
        }
    }
}
