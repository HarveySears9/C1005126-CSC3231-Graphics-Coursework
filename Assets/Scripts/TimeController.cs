using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeController : MonoBehaviour
{
    [SerializeField] // Can be edited in the inspector
    private float timeMultiplier;
    [SerializeField]
    private float startTime; // Initial time in hours
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private Light sunLight;
    [SerializeField]
    private Light moonLight;
    [SerializeField]
    private float sunRiseTime;
    [SerializeField]
    private float sunSetTime;
    [SerializeField]
    private Color dayAmbientLight;
    [SerializeField]
    private Color nightAmbientLight;
    [SerializeField]
    private AnimationCurve lightChangeCurve; // Curve to control light intensity changes
    [SerializeField]
    private float maxSunlightIntensity;
    [SerializeField]
    private float maxMoonlightIntensity;

    private DateTime currentTime; // Current time in the simulation
    private TimeSpan sunSet; // Time when the sun sets
    private TimeSpan sunRise; // Time when the sun rises

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the current time using the start time
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startTime);

        // Convert sun rise and sun set times to TimeSpan
        sunRise = TimeSpan.FromHours(sunRiseTime);
        sunSet = TimeSpan.FromHours(sunSetTime);
    }

    // Update is called once per frame
    void Update()
    {
        // Update the time of day, sun rotation, and light settings
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
    }

    // Update the current time based on the time multiplier
    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);

        if (timeText != null)
        {
            timeText.text = currentTime.ToString("HH:mm");
        }
    }

    // Rotate the sun based on the current time and sun rise/set times
    private void RotateSun()
    {
        float sunLightRotation;

        // Check if the current time is between sun rise and sun set
        if (currentTime.TimeOfDay > sunRise && currentTime.TimeOfDay < sunSet)
        {
            // Calculate the percentage of time passed since sunrise to sunset
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunRise, sunSet);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunRise, currentTime.TimeOfDay);
            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            // Calculate the percentage of time passed since sunset to sunrise
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunSet, sunRise);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunSet, currentTime.TimeOfDay);
            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        // Set the sun's rotation based on the calculated angle
        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }

    // Calculate the time difference considering the wrap-around at midnight
    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan diff = toTime - fromTime;

        // Handle wrap-around at midnight
        if (diff.TotalSeconds < 0)
        {
            diff += TimeSpan.FromHours(24);
        }

        return diff;
    }

    // Update the light settings based on the sun's rotation
    private void UpdateLightSettings()
    {
        // Calculate the dot product of the sun's forward vector and the down vector
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);

        sunLight.intensity = Mathf.Lerp(0, maxSunlightIntensity, lightChangeCurve.Evaluate(dotProduct));
        moonLight.intensity = Mathf.Lerp(maxSunlightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
    }
}

