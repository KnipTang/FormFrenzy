using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class MovementParent : MonoBehaviour
{
    [SerializeField] private GameObject _leftUpper;
    [SerializeField] private GameObject _leftLower;
    [SerializeField] private GameObject _rightUpper;
    [SerializeField] private GameObject _rightLower;

    [SerializeField] protected GameObject _bendObject;

    protected GameObject LeftUpper
    {
        get { return _leftUpper; }
        set 
        {
            _leftUpper = value;
            ClampValues(_leftUpper, _MinAngleLeftUpper, _MaxAngleLeftUpper);
        }
    }
    protected GameObject LeftLower
    {
        get { return _leftLower; }
        set 
        { 
            _leftLower = value;
            ClampValues(_leftLower, _MinAngleLeftLower, _MaxAngleLeftLower);
        }
    }
    protected GameObject RightUpper
    {
        get { return _rightUpper; }
        set 
        { 
            _rightUpper = value;
            ClampValues(_rightUpper, _MinAngleRightUpper, _MaxAngleRightUpper);
        }
    }
    protected GameObject RightLower
    {
        get { return _rightLower; }
        set 
        { 
            _rightLower = value;
            ClampValues(_rightLower, _MinAngleRightLower, _MaxAngleRightLower);
        }
    }

    protected virtual GameObject BendObject
    {
        get { return _bendObject; }
        set
        {
            _bendObject = value;
        }
    }

    private Vector2 _leftLimbValue;
    private Vector2 _rightLibValue;
    private float _bendValue;
    private bool _leftSwitchValue = true;
    private bool _rightSwitchValue = true;

    [SerializeField]
    public float _MaxAngleLeftLower;
    [SerializeField]
    public float _MinAngleLeftLower;
    [SerializeField]
    public float _MaxAngleLeftUpper;
    [SerializeField]
    public float _MinAngleLeftUpper;
    [SerializeField]
    public float _MaxAngleRightLower;
    [SerializeField]
    public float _MinAngleRightLower;
    [SerializeField]
    public float _MaxAngleRightUpper;
    [SerializeField]
    public float _MinAngleRightUpper;
    [SerializeField]
    public float _MaxAngleBendObject;
    [SerializeField]
    public float _MinAngleBendObject;

    protected Vector2 LeftLimbValue
    {
        get
        {
            return _leftLimbValue;
        }
        set
        {
            _leftLimbValue = value;
        }
    }
    protected Vector2 RightLimbValue
    {
        get
        {
            return _rightLibValue;
        }
        set
        {
            _rightLibValue = value;
        }
    }
    protected float BendValue
    {
        get
        {
            return _bendValue;
        }
        set
        {
            _bendValue = value;
            if(value < _deadzone && value > -_deadzone)
            {
                _time = 0;
            }
        }
    }
    protected virtual bool RightSwitchValue
    {
        get
        {
            return _rightSwitchValue;
        }
        set
        {
            _rightSwitchValue = value;
        }
    }
    protected virtual bool LeftSwitchValue
    {
        get
        {
            return _leftSwitchValue;
        }
        set
        {
            _leftSwitchValue = value;
        }
    }

    public delegate void LeftLimbChange();
    public event LeftLimbChange OnLeftLimbChanged;

    public delegate void RightLimbChange();
    public event RightLimbChange OnRightLimbChanged;

    public delegate void BendChange();
    public event BendChange OnBendChanged;

    [SerializeField] private int _playerIndex;

    [SerializeField] protected float _speed = 200;
    [SerializeField] protected float _speedBend = 1.5f;

    [SerializeField] private float _deadzone = 0.1f;

    private bool _leftStickNotNull;
    private bool _rightStickNotNull;
    private bool _bumperNotNull;

    private bool _leftStickClick;
    private bool _rightStickClick;
    private bool _buttonEast;

    private int _movementScriptsAmount;

    protected float _time;

    public AnimationCurve movementCurve;

    protected PlayerActiveLimbIndicator _indicator;
    private void Start()
    {
        _movementScriptsAmount = FindObjectsOfType<MovementParent>().Length;

        OnLeftLimbChanged += UpdateLeftLimb;
        OnRightLimbChanged += UpdateRightLimb;
        OnBendChanged += UpdateBend;

        _indicator = FindObjectOfType<PlayerActiveLimbIndicator>();
    }

    private void FixedUpdate()
    {
        if (_leftStickNotNull)
            OnLeftLimbChanged?.Invoke();
        if (_rightStickNotNull)
            OnRightLimbChanged?.Invoke();
        if(_bumperNotNull)
            OnBendChanged?.Invoke();
    }

    public int GetPlayerIndex()
    {
        return _playerIndex;
    }
    public void SetInputLeftLimb(Vector2 leftValue)
    {
        if (Mathf.Abs(leftValue.x) > _deadzone || Mathf.Abs(leftValue.y) > _deadzone)
        {
            _leftStickNotNull = true;
        }
        else
        {
            _leftStickNotNull = false;
        }

        LeftLimbValue = leftValue;
    }
    public void SetInputRightLimb(Vector2 rightValue)
    {
        if (Mathf.Abs(rightValue.x) > _deadzone || Mathf.Abs(rightValue.y) > _deadzone)
        {
            _rightStickNotNull = true;
        }
        else
        {
            _rightStickNotNull = false;
        }

        RightLimbValue = rightValue;
    }
    public void SetInputBendLeft(InputAction.CallbackContext context)
    {
        float bumperValue = context.ReadValue<float>();

        if (context.started)
        {
            _time = 0;
        }

        if (bumperValue < -_deadzone)
        _bumperNotNull = true;
        else
        {
            _bumperNotNull = false;
        }
        BendValue = bumperValue;
    }
    public void SetInputBendRight(InputAction.CallbackContext context)
    {
        float bumperValue = context.ReadValue<float>();

        if (context.started)
        {
            _time = 0;
        }

        if (bumperValue > _deadzone)
            _bumperNotNull = true;
        else
        {
            _bumperNotNull = false;
        }
        BendValue = bumperValue;
    }
    public void SetInputLeftSwitch(float leftTriggerValue)
    {
        if (leftTriggerValue < _deadzone)
            LeftSwitchValue = true;
        else
            LeftSwitchValue = false;
    }
    public void SetInputRightSwitch(float rightTriggerValue)
    {
        if (rightTriggerValue < _deadzone)
            RightSwitchValue = true;
        else
            RightSwitchValue = false;
    }
    public void SetInputLeftStickClick(float leftStickClick)
    {
        _leftStickClick = leftStickClick != 0f;
        SetTPose();
    }
    public void SetInputRightStickClick(float rightStickClick)
    {
        _rightStickClick = rightStickClick != 0f;
        SetTPose();
    }
    public void SetInputButtonEast(float buttonEast)
    {
        _buttonEast = buttonEast != 0f;

        if (_buttonEast)
            SwitchPlayerIndex();
    }

    protected virtual void UpdateLeftLimb()
    {
      


    }
    protected virtual void UpdateRightLimb()
    {


    }
    protected virtual void UpdateBend()
    {
        _speedBend = movementCurve.Evaluate(_time);
        _time += Time.deltaTime;
    }

    public void SwitchPlayerIndex()
    {
        ResetValues();

        ControllerInput[] controllerInputs = FindObjectsOfType<ControllerInput>();

        if (controllerInputs == null) return;

        foreach (var controllerInput in controllerInputs)
        {
            controllerInput.PlayerIndex++;
            if (controllerInput.PlayerIndex > _movementScriptsAmount - 1)
            {
                controllerInput.PlayerIndex = 0;
            }
            controllerInput.AssignMovementScriptToPlayerIndex();
        }
    }

    private void SetTPose()
    {
        if(_leftStickClick || _rightStickClick)
        {
            Respawn respawnPlayer = FindAnyObjectByType<Respawn>();

            if (respawnPlayer != null)
            {
                StartCoroutine(respawnPlayer.AnimatorToggle());
            }
        }
    }

    private void ResetValues()
    {
        LeftLimbValue = new Vector2(0f, 0f);
        RightLimbValue = new Vector2(0f, 0f);
        BendValue = 0;
    }

    protected GameObject SetLimbAngle(GameObject Limb, float targetAngle)
    {
        float maxRotation = _speed * Time.deltaTime;
        // Get the current angle of LeftUpper
        float currentAngle = Limb.transform.rotation.eulerAngles.z;
        // Calculate the difference between the target angle and the current angle
         float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);
        
        // Clamp the angle difference to the maximum rotation
        float clampedAngleDifference = Mathf.Clamp(angleDifference, -maxRotation, maxRotation);
        
        // Calculate the new angle
        float newAngle = currentAngle + clampedAngleDifference;
        // Rotate LeftUpper smoothly to the new angle
        GameObject newObject = Limb;
        newObject.transform.rotation = Quaternion.Euler(Limb.transform.rotation.eulerAngles.x, Limb.transform.rotation.eulerAngles.y, newAngle);
        return newObject;
        //Vector3 currentRotation = LeftUpper.transform.rotation.eulerAngles;
        //
        //// Add 5 to the y angle rotation
        //currentRotation.x += 5f;
        //
        //// Apply the new rotation
        //LeftUpper.transform.rotation = Quaternion.Euler(currentRotation);

    }
    protected void ClampValues(GameObject limb, float min, float max)
    {
        Vector3 eulerAngles = limb.transform.localEulerAngles;

        eulerAngles.z = Mathf.Repeat(eulerAngles.z, 360f);
        if (eulerAngles.z > 180f)
        {
            eulerAngles.z -= 360f;
        }

        eulerAngles.z = Mathf.Clamp(eulerAngles.z, min, max);
        limb.transform.localEulerAngles = eulerAngles;
    }

}
