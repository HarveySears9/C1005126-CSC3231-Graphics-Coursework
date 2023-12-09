using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    DebugModelMatrix matrixScript;
    public Matrix4x4 scaleMatrix;
    public float rotationSpeed = 10f;
    public float rotationAngle = 0f;
    public float scale = 1f;


    void Start()
    {
        matrixScript = this.GetComponent<DebugModelMatrix>();

        //set scale
        scaleMatrix = new Matrix4x4(
                new Vector4(scale, 0, 0, 0),
                new Vector4(0, scale, 0, 0),
                new Vector4(0, 0, scale, 0),
                new Vector4(0, 0, 0, 1f)
        );
    }

    void Update()
    {
        // Rotate the object around the Y-axis based on time and rotation speed
        rotationAngle += rotationSpeed * Time.deltaTime;

        //Debug.Log(rotationAngle);

        matrixScript.modelMat[0, 0] = Mathf.Cos(rotationAngle);
        matrixScript.modelMat[0, 2] = Mathf.Sin(rotationAngle);
        matrixScript.modelMat[1, 1] = 1;
        matrixScript.modelMat[2, 0] = -Mathf.Sin(rotationAngle);
        matrixScript.modelMat[2, 2] = Mathf.Cos(rotationAngle);
        matrixScript.modelMat[3, 3] = 1;

        matrixScript.modelMat *= scaleMatrix;
    }
}
