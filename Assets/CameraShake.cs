using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;  // Singleton for easy access
    private Vector3 originalPos;
    private bool isShaking = false;
    private float shakeMagnitude = 2f;
    private float shakeDuration = 0.5f;

    // Singleton setup
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Trigger the shake with desired magnitude and duration
    public void TriggerShake(float magnitude, float duration)
    {
        originalPos = transform.localPosition;  // Store the initial position of the camera
        shakeMagnitude = magnitude;
        shakeDuration = duration;
        isShaking = true;
    }

    void Update()
    {
        // If the camera is shaking, apply the shake
        if (isShaking)
        {
            // Apply shake by modifying the camera position randomly around the original position
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeMagnitude;

            // Reduce the shake duration over time
            shakeDuration -= Time.deltaTime;

            // If the shake duration ends, stop the shake and reset the camera position
            if (shakeDuration <= 0)
            {
                isShaking = false;
                transform.localPosition = originalPos;  // Reset position to the original
            }
        }
    }
}
