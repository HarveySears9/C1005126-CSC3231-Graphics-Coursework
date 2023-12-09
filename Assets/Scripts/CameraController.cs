using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Movement speed of the camera
    public float moveSpeed = 5f;

    // Sensitivity for mouse rotation
    public float sensitivity = 2f;

    // Intensity of camera shake
    public float shakeIntensity = 0.1f;

    // Original position of the camera before the shake
    private Vector3 originalPosition;

    // Function to initiate camera shake with a specified duration
    public void Shake(float shakeDuration)
    {
        // Store the original position before the shake
        originalPosition = transform.localPosition;

        // Start the coroutine to handle the camera shake
        // shakeDuration is passed from the Volcano script
        StartCoroutine(ShakeCoroutine(shakeDuration));
    }

    // Coroutine for camera shake
    IEnumerator ShakeCoroutine(float shakeDuration)
    {
        float elapsed = 0.0f;

        // Shake the camera for the specified duration
        while (elapsed < shakeDuration)
        {
            float x = originalPosition.x + Random.Range(-1f, 1f) * shakeIntensity;
            float y = originalPosition.y + Random.Range(-1f, 1f) * shakeIntensity;

            // Update the camera position with random offsets
            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        // Reset the camera position to the original position after the shake
        transform.localPosition = originalPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // Camera movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Camera rotation
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up * mouseX * sensitivity);

        float currentRotationX = transform.rotation.eulerAngles.x;
        float newRotationX = currentRotationX - mouseY * sensitivity;

        // Apply the new rotation to the camera, with a limit to avoid flipping
        transform.rotation = Quaternion.Euler(newRotationX, transform.rotation.eulerAngles.y, 0f);
    }
}

