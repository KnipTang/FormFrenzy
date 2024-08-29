using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerShake : MonoBehaviour
{
    public IEnumerator VibrateController(float duration, float lowFreq, float highFreq)
    {
        if (Gamepad.current != null)
        {
            var gamepads = Gamepad.all;

            foreach (var gamepad in gamepads)
            {
                gamepad.SetMotorSpeeds(lowFreq, highFreq);
            }

            yield return new WaitForSeconds(duration);

            foreach (var gamepad in gamepads)
            {
                gamepad.SetMotorSpeeds(0, 0);
            }
        }
    }
}
