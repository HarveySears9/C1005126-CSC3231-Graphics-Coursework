using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano : MonoBehaviour
{
    DebugModelMatrix matrixScript;

    public CameraController cameraController;

    public ParticleSystem ps;
    public int eruptionDelay = 40;
    public float raiseSpeed = 1f;

    public Light lavaLight;
    public float fadeInDuration = 3f;
    public float fadeOutDuration = 5f;
    public float targetIntensity = 15f;

    private bool isErupting = false;

    void Start()
    {
        matrixScript = this.GetComponent<DebugModelMatrix>();

        // Invoke the Eruption method repeatedly after a delay
        InvokeRepeating("Eruption", eruptionDelay, eruptionDelay);
    }

    void Eruption()
    {
        // Check if the particle system is assigned
        if (ps != null)
        {
            // Play the particle system
            ps.Play();

            // raising the volcano
            StartCoroutine(RaiseVolcano());

            // Shake the camera based on the duration of the particle system
            cameraController.Shake(ps.main.duration);
        }
    }

    IEnumerator RaiseVolcano()
    {
        // Set the flag to indicate that the volcano is erupting
        isErupting = true;

        // Fade in the light during the eruption
        StartCoroutine(FadeLight(true));

        // Wait for the duration of the particle system
        yield return new WaitForSeconds(ps.main.duration);

        // Fade out the light after the eruption
        StartCoroutine(FadeLight(false));

        // Reset the eruption flag
        isErupting = false;
    }

    IEnumerator FadeLight(bool fadeIn)
    {
        float time = 0f;
        float initialIntensity = lavaLight.intensity;

        // Fades in light
        if (fadeIn)
        {
            while (time < fadeInDuration)
            {
                lavaLight.intensity = Mathf.Lerp(initialIntensity, targetIntensity, time / fadeInDuration);
                yield return null;
                time += Time.deltaTime;
            }
            lavaLight.intensity = targetIntensity;
        }
        // Fades out light
        else if (!fadeIn)
        {
            while (time < fadeOutDuration)
            {
                lavaLight.intensity = Mathf.Lerp(initialIntensity, 0, time / fadeOutDuration);
                yield return null;
                time += Time.deltaTime;
            }
            lavaLight.intensity = 0;
        }
    }

    void Update()
    {
        // Check if the volcano is erupting and its Y position is below zero
        if (isErupting && matrixScript.modelMat[1, 3] < 0)
        {
            // Raise the volcano's Y position based on the raise speed
            matrixScript.modelMat[1, 3] += (Time.deltaTime * raiseSpeed);

            // Ensure the volcano's Y position doesn't go above zero
            if (matrixScript.modelMat[1, 3] > 0)
            {
                matrixScript.modelMat[1, 3] = 0;
            }
        }
    }
}
