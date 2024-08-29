using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ControllerInput : MonoBehaviour
{
    private PlayerInput playerInput;

    private int _playerIndex;
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

    private Vector2 leftStickDir;
    private Vector2 rightStickDir;
    private float bumperValue;
    private float leftBumperValue;
    private float rightBumperValue;
    private float leftStickClickValue;
    private float rightStickClickValue;
    private MovementParent movementScript;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        Debug.Log(playerInput.playerIndex);
        PlayerIndex = playerInput.playerIndex;

        ControllerInput[] controllerInputs = FindObjectsOfType<ControllerInput>();
        foreach (ControllerInput controllerInput in controllerInputs)
        {
            if (controllerInput.gameObject != gameObject && PlayerIndex == controllerInput.PlayerIndex)
            {
                PlayerIndex++;
                if (PlayerIndex > 1)
                    PlayerIndex = 0;
            }
        }

        AssignMovementScriptToPlayerIndex();
    }
    public void AssignMovementScriptToPlayerIndex()
    {
        if (PlayerIndex > 1) return;

        MovementParent[] movementPlayersScripts = FindObjectsOfType<MovementParent>();

        if (movementPlayersScripts != null)
            foreach (MovementParent player in movementPlayersScripts)
            {
                if (player.GetPlayerIndex() == PlayerIndex)
                {
                    movementScript = player;

                    Debug.Log(movementScript.gameObject.name);

                    if (movementScript.gameObject.GetComponent<PlayerConnectParticle>() != null)
                        StartCoroutine(movementScript.gameObject.GetComponent<PlayerConnectParticle>().SpawnParticle());
                }
            }
    }

    public void SetInputLeftStick(InputAction.CallbackContext context)
    {
        leftStickDir = context.ReadValue<Vector2>();
        movementScript.SetInputLeftLimb(leftStickDir);
    }

    public void SetInputRightStick(InputAction.CallbackContext context)
    {
        rightStickDir = context.ReadValue<Vector2>();
        movementScript.SetInputRightLimb(rightStickDir);
    }
    public void SetInputBumpersLeft(InputAction.CallbackContext context)
    {
        movementScript.SetInputBendLeft(context);
    }
    public void SetInputBumpersRight(InputAction.CallbackContext context)
    {
        //bumperValue = context.ReadValue<float>();
        movementScript.SetInputBendRight(context);
    }
    public void SetInputLeftTrigger(InputAction.CallbackContext context)
    {
        leftBumperValue = context.ReadValue<float>();
        movementScript.SetInputLeftSwitch(leftBumperValue);
    }
    public void SetInputRightTrigger(InputAction.CallbackContext context)
    {
        rightBumperValue = context.ReadValue<float>();
        movementScript.SetInputRightSwitch(rightBumperValue);
    }
    public void SetInputLeftStickClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started || context.phase == InputActionPhase.Canceled)
        {
            leftStickClickValue = context.ReadValue<float>();
            movementScript.SetInputLeftStickClick(leftStickClickValue);
        }
    }
    public void SetInputRightStickClick(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started || context.phase == InputActionPhase.Canceled)
        {
            rightStickClickValue = context.ReadValue<float>();
            movementScript.SetInputRightStickClick(rightStickClickValue);
        }
    }
    public void SetInputButtonEast(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started || context.phase == InputActionPhase.Canceled)
        {
            rightStickClickValue = context.ReadValue<float>();
            movementScript.SetInputButtonEast(rightStickClickValue);
        }
    }
    public void SetInputButtonNorth(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            FindAnyObjectByType<HighscoreFileIO>().ResetHighScore();
        }
        if (context.phase == InputActionPhase.Performed)
        {
            if (FindAnyObjectByType<UI_ControllerScheme>() != null)
                FindAnyObjectByType<UI_ControllerScheme>().ToggleControllerScheme(true);
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            if (FindAnyObjectByType<UI_ControllerScheme>() != null)
                FindAnyObjectByType<UI_ControllerScheme>().ToggleControllerScheme(false);
        }
    }

    public void SetInputButtonSouth(InputAction.CallbackContext context)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name == "EndScene" && scene.isLoaded)
            {
                MenuStartGame.StartGame();
            }
        }
    }
    public void SetInputIncreaseWight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            FindObjectOfType<SpawnWall>()._minCurrentWeight -= 0.1f;
            FindObjectOfType<SpawnWall>()._maxCurrentWeight -= 0.1f;
        }
    }
    public void SetInputDecreaseWight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            FindObjectOfType<SpawnWall>()._minCurrentWeight += 0.1f;
            FindObjectOfType<SpawnWall>()._maxCurrentWeight += 0.1f;
        }
    }

    public void SetInputAddLife(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            FindObjectOfType<HealthComponent>().Heal();
        }
    }
}
