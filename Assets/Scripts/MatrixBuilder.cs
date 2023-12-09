using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixBuilder : MonoBehaviour
{
    [SerializeField]
    Matrix4x4[] matrices = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Matrix4x4 modelMat = Matrix4x4.identity;

        if (matrices != null)
        {
            foreach (Matrix4x4 m in matrices)
            { 
                modelMat = m * modelMat;
            }
        }

        //Let's turn the model matrix into a 'transform' for the editor!
        transform.localPosition = MatrixFunctions.ExtractTranslationFromMatrix(ref modelMat);
        transform.localScale    = MatrixFunctions.ExtractScaleFromMatrix(ref modelMat);
        transform.localRotation = MatrixFunctions.ExtractRotationFromMatrix(ref modelMat);
    }
}
