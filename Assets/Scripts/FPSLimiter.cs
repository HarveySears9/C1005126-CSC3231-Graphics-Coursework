using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    public int targetFPS = 60;

    void Start()
    {
        // Set the target frame rate
        Application.targetFrameRate = targetFPS;
    }
}
