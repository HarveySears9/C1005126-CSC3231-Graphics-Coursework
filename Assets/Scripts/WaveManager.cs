using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // Singleton pattern: Ensures only one instance of WaveManager exists
    public static WaveManager instance;

    // Wave properties
    public float ampliture = 1.0f; // Amplitude of the wave
    public float length = 2f; // Length of the wave
    public float speed = 1f; // Speed at which the wave moves
    public float offset = 0f; // Offset to control the phase of the wave

    // Singleton setup in Awake method
    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // Set this instance as the singleton instance
        }
        else if (instance != this)
        {
            Destroy(this); // Destroy duplicate instances to maintain singleton pattern
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // Update the offset based on time and speed to create wave motion
        offset += Time.deltaTime * speed;
    }

    // Function to get the height of the wave at a given x position
    public float GetWaveHeight(float _x)
    {
        // Calculate the wave height using the sine function with amplitude, length, and offset
        return ampliture * Mathf.Sin(_x / length + offset);
    }
}

