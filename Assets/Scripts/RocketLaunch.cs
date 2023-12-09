using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLaunch : MonoBehaviour
{
    DebugModelMatrix matrixScript;

    public GameObject rocketIsland;
    public ParticleSystem rocketTrail;
    public ParticleSystem cloud;

    public int launchDelay = 25;
    public float speedUpDuration = 3f;
    public float maxSpeed = 5f;
    public float flightSpeed = 0f;

    private bool launched = false;

    // Start is called before the first frame update
    void Start()
    {
        matrixScript = this.GetComponent<DebugModelMatrix>();

        // Invoke the Launch method after a delay
        Invoke("Launch", launchDelay);
    }

    // initiate the rocket launch
    void Launch()
    {
        // Check if both rocketTrail and cloud ParticleSystem components are assigned
        if (rocketTrail != null && cloud != null)
        {
            // Play the rocketTrail and cloud particle effects
            rocketTrail.Play();
            cloud.Play();

            // Set the launched flag to true
            launched = true;

            // Start coroutines to handle island destruction, rocket destruction, and rocket speed-up
            StartCoroutine(DestroyIsland());
            StartCoroutine(DestroyRocket());
            StartCoroutine(SpeedUpRocket());
        }
    }

    // Coroutine to destroy the rocket island after a delay
    IEnumerator DestroyIsland()
    {
        yield return new WaitForSeconds(6f);
        rocketIsland.SetActive(false);
    }

    // Coroutine to destroy the rocket after a delay
    IEnumerator DestroyRocket()
    {
        yield return new WaitForSeconds(40f);

        Destroy(gameObject);
    }

    // Coroutine to gradually increase the flight speed of the rocket
    IEnumerator SpeedUpRocket()
    {
        float time = 0f;
        float initialSpeed = 0f;

        // Gradually increase the flight speed over the specified duration
        while (time < speedUpDuration)
        {
            flightSpeed = Mathf.Lerp(initialSpeed, maxSpeed, time / speedUpDuration);
            yield return null;
            time += Time.deltaTime;
        }

        // Set the flight speed to the maximum speed
        flightSpeed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the rocket has been launched
        if (launched)
        {
            // Update the Y position of the rocket based on the flight speed
            matrixScript.modelMat[1, 3] += (Time.deltaTime * flightSpeed);
        }
    }
}
