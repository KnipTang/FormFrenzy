using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer1 : MovementParent
{
    protected override bool RightSwitchValue
    {
        get
        {
            return true;
        }
        set
        {
            _indicator.SetRightArm(value);

            base.RightSwitchValue = value;
        }
    }
    protected override bool LeftSwitchValue
    {
        get
        {
            return true;
        }
        set
        {
            _indicator.SetLeftArm(value);

            base.LeftSwitchValue = value;
        }
    }
    protected override GameObject BendObject
    {
        get { return _bendObject; }
        set
        {
            _bendObject = value;
            //Clamp rotation
            ClampValues(_bendObject, _MinAngleBendObject, _MaxAngleBendObject);
        }
    }

    // Calculate the maximum amount to rotate this frame
    private float targetAngle;
    protected override void UpdateLeftLimb()
    {
        // Calculate movement direction from stick coordinates
        Vector2 stickInput = new Vector2(LeftLimbValue.y, LeftLimbValue.x).normalized;
        // Calculate angle based on movement direction
        targetAngle = Mathf.Atan2(stickInput.y, stickInput.x) * Mathf.Rad2Deg;

        if (base.LeftSwitchValue)
        {
            LeftUpper = SetLimbAngle(LeftUpper, targetAngle);
        }
        else
        {
            LeftLower = SetLimbAngle(LeftLower, targetAngle);
        }
    }
    protected override void UpdateRightLimb()
    {
        // Calculate movement direction from stick coordinates
        Vector2 stickInput = new Vector2(RightLimbValue.y, RightLimbValue.x).normalized;
        // Calculate angle based on movement direction
        targetAngle = Mathf.Atan2(stickInput.y, stickInput.x) * Mathf.Rad2Deg;

        if (base.RightSwitchValue)
        {
            RightUpper = SetLimbAngle(RightUpper, targetAngle);
        }
        else
        {
            RightLower = SetLimbAngle(RightLower, targetAngle);
        }
    }
    protected override void UpdateBend()
    {
        base.UpdateBend();

        // Rotate the bendObject based on the bend amount
        Vector3 eulerAngles = BendObject.transform.localEulerAngles;
        //  eulerAngles.z += bendAmount;
        eulerAngles.z += BendValue * _speedBend;

        GameObject newObject = BendObject;
        newObject.transform.localEulerAngles = eulerAngles;
        BendObject = newObject;
    }
}
