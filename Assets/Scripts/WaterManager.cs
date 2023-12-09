using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour
{
    private MeshFilter meshFilter; // Reference to the MeshFilter component

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>(); // Assign mesh filter
    }

    private void Update()
    {
        // Get the current vertices of the mesh
        Vector3[] vertices = meshFilter.mesh.vertices;

        // Iterate through each vertex and update its Y coordinate based on the wave height from WaveManager
        for (int i = 0; i < vertices.Length; i++)
        {
            // Update the Y coordinate of the vertex using the wave height from WaveManager
            vertices[i].y = WaveManager.instance.GetWaveHeight(transform.position.x + vertices[i].x);
        }

        // Apply the updated vertices back to the mesh
        meshFilter.mesh.vertices = vertices;

        // Recalculate normals to ensure lighting is updated based on the modified mesh
        meshFilter.mesh.RecalculateNormals();
    }
}

