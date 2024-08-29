using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginGame : MonoBehaviour
{

    void Start()
    {
        GameStats.Instance.ResetStats();
    }
}
