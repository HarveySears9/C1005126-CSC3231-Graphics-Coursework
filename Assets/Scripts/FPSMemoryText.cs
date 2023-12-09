using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSMemoryText : MonoBehaviour
{
    public TextMeshProUGUI textMesh;

    private void Start()
    {
        // Set the initial text
        textMesh.text = "FPS: 0\nMemory: 0 MB";
    }

    private void Update()
    {
        // Calculate FPS
        float fps = 1.0f / Time.deltaTime;

        // Get total allocated memory from the Profiler
        long totalMemoryBytes = UnityEngine.Profiling.Profiler.GetTotalAllocatedMemoryLong();

        // Convert bytes to megabytes
        float totalMemoryMB = totalMemoryBytes / (1024f * 1024f);

        // Update the text
        textMesh.text = $"FPS: {fps:F1}\nMemory: {totalMemoryMB:F2} MB";
    }
}
