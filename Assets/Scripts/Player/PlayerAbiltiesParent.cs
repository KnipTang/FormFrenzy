using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbiltiesParent : MonoBehaviour
{
    [SerializeField] private float _indicatorTime = 30f;

    public float IndicatorTime
    { 
        get { return _indicatorTime; }
    }
}
