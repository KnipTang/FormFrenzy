using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ControllerScheme : MonoBehaviour
{
    [SerializeField] private GameObject _ControllerSchemeUI;

    private void Start()
    {
        ToggleControllerScheme(false);
    }
    public void ToggleControllerScheme(bool value)
    {
        if (_ControllerSchemeUI != null)
        {
            _ControllerSchemeUI.SetActive(value);
        }
    }
}
