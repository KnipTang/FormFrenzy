using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStats : MonoBehaviour
{
    [SerializeField] private float _weight;

    public float GetWeight()
    {
        return _weight;
    }
}
