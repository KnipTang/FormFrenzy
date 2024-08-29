using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer2 : MovementParent
{
    protected override bool RightSwitchValue
    {
        get
        {
            return true;
        }
        set
        {
            _indicator.SetRightLeg(value);

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
            _indicator.SetLeftLeg(value);

            base.LeftSwitchValue = value;
        }
    }
    protected override GameObject BendObject
    {
        get { return _bendObject; }
        set
        {
            _bendObject = value;

            //Clamp pos
            Vector3 currentPosition = _bendObject.transform.position;
            currentPosition.x = Mathf.Clamp(currentPosition.x, _MinAngleBendObject, _MaxAngleBendObject);
            _bendObject.transform.position = currentPosition;
        }
    }
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

        Vector3 pos = BendObject.transform.position;
        pos.x -= BendValue * _speedBend;

        GameObject newObject = BendObject;
        newObject.transform.position = pos;
        BendObject = newObject;
    }
}
