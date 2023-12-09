using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugModelMatrix : MonoBehaviour
{
    [SerializeField]
    public Matrix4x4 modelMat = Matrix4x4.identity;

    [SerializeField]
    bool writeValues;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(writeValues)
        {
            //Let's turn the model matrix into a 'transform' for the editor!
            transform.localPosition = MatrixFunctions.ExtractTranslationFromMatrix(ref modelMat);
            transform.localScale    = MatrixFunctions.ExtractScaleFromMatrix(ref modelMat);
            transform.localRotation = MatrixFunctions.ExtractRotationFromMatrix(ref modelMat);
        }
        else
        {
            modelMat = transform.localToWorldMatrix;
        }
    }
}
