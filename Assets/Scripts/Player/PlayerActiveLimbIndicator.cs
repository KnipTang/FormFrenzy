using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActiveLimbIndicator : MonoBehaviour
{
    [SerializeField] private GameObject _GeoCharacter;
    public Material _material;
    // Start is called before the first frame update
    void Start()
    {
        SkinnedMeshRenderer skinnedMeshRenderer = _GeoCharacter.GetComponent<SkinnedMeshRenderer>();

        if (skinnedMeshRenderer != null && skinnedMeshRenderer.material != null)
        {
            _material = skinnedMeshRenderer.material;
        }
    }

    public void SetRightArm(bool value)
    {
        if (_material.HasProperty("_Indicator_Arm_R"))
            _material.SetFloat("_Indicator_Arm_R", value ? 0 : 1);
    }
    public void SetLeftArm(bool value)
    {
        if (_material.HasProperty("_Indicator_Arm_L"))
            _material.SetFloat("_Indicator_Arm_L", value ? 0 : 1);
    }
    public void SetRightLeg(bool value)
    {
        if (_material.HasProperty("_Indicator_Leg_R"))
            _material.SetFloat("_Indicator_Leg_R", value ? 0 : 1);
    }
    public void SetLeftLeg(bool value)
    {
        if (_material.HasProperty("_Indicator_Leg_L"))
            _material.SetFloat("_Indicator_Leg_L", value ? 0 : 1);
    }
}
