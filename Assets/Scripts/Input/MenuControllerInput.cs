using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuControllerInput : MonoBehaviour
{
    private PlayerInput _playerInput;
    private int _playerIndex;

    private float _southButton;

    public int PlayerIndex
    {
        get
        {
            return _playerIndex;
        }
        set
        {
            _playerIndex = value;
        }
    }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        Debug.Log(_playerInput.playerIndex);
        PlayerIndex = _playerInput.playerIndex;

        MenuControllerInput[] controllerInputs = FindObjectsOfType<MenuControllerInput>();
        foreach (MenuControllerInput controllerInput in controllerInputs)
        {
            if (controllerInput.gameObject != gameObject && PlayerIndex == controllerInput.PlayerIndex)
            {
                PlayerIndex++;
                if (PlayerIndex > 1)
                    PlayerIndex = 0;
            }
        }
    }
   
    public void SetInputButtonSouth(InputAction.CallbackContext context)
    {
        _southButton = context.ReadValue<float>();
        MenuStartGame.StartGame();
    }
}
