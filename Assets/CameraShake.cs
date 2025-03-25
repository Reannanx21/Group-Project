using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;
    private Vector3 originalPosition;
    private CinemachineVirtualCamera virtualCamera;

    // Store the original position of the camera's follow target
    private Transform followTarget;

    private void Start()
    {
        // Get the Cinemachine Virtual Camera component
        virtualCamera = Camera.main.GetComponentInChildren<CinemachineVirtualCamera>();

        if (virtualCamera == null)
        {
            Debug.LogError("CinemachineVirtualCamera is missing from the Main Camera!");
            return;
        }

        // Get the camera's follow target (should be assigned in the inspector)
        followTarget = virtualCamera.Follow;
    }

    public void ShakeCamera()
    {
        StopAllCoroutines(); // Stop any ongoing shake
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0f;

        // Store the original position of the camera
        Vector3 originalFollowPosition = followTarget.position;

        while (elapsed < shakeDuration)
        {
            // Generate random shake offsets
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            // Apply the shake to the camera's follow target
            followTarget.position = originalFollowPosition + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Reset the position of the follow target after shake
        followTarget.position = originalFollowPosition;
    }
}
