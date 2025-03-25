using UnityEngine;

public class TestCameraShake : MonoBehaviour
{
    private CameraShake cameraShake;

    void Start()
    {
        // Get the CameraShake component from the Main Camera
        cameraShake = Camera.main.GetComponent<CameraShake>();

        if (cameraShake == null)
        {
            Debug.LogError("CameraShake script not found on the Main Camera!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Trigger the camera shake manually when the Space bar is pressed
            cameraShake.ShakeCamera();
        }
    }
}
