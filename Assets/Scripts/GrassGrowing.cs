using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassGrowing : MonoBehaviour
{
    DebugModelMatrix matrixScript;

    private float growingSpeed;
    private float maxGrowth;
    private bool fullyGrown = false;

    void Start()
    {
        matrixScript = this.GetComponent<DebugModelMatrix>();

        // Initialize growing speed and max growth with random values
        growingSpeed = Random.Range(0.02f, 0.07f);
        maxGrowth = Random.Range(0.85f, 1.5f);
    }

    void Update()
    {
        // Check if the grass has reached its maximum growth
        if (matrixScript.modelMat[0, 0] >= maxGrowth)
        {
            // If not fully grown, set fullyGrown flag, and start coroutine to reset the grass
            if (!fullyGrown)
            {
                fullyGrown = true;
                StartCoroutine(ResetGrass());
            }
        }
        // If not fully grown, continue growing the grass
        else if (!fullyGrown)
        {
            // Increase scale in X, Y, and Z directions based on the growing speed
            matrixScript.modelMat[0, 0] += (Time.deltaTime * growingSpeed);
            matrixScript.modelMat[1, 1] += (Time.deltaTime * growingSpeed);
            matrixScript.modelMat[2, 2] += (Time.deltaTime * growingSpeed);
        }
    }

    // Coroutine to reset the grass after a delay
    IEnumerator ResetGrass()
    {
        yield return new WaitForSeconds(5f);

        // Reset the scale of the grass
        matrixScript.modelMat[0, 0] = 0f;
        matrixScript.modelMat[1, 1] = 0f;
        matrixScript.modelMat[2, 2] = 0f;

        // Generate new random growing speed and max growth values
        growingSpeed = Random.Range(0.02f, 0.07f);
        maxGrowth = Random.Range(0.85f, 1.5f);

        // Reset the fully grown flag
        fullyGrown = false;
    }
}

