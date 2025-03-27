using UnityEngine;
using Cinemachine;

public class CameraShakeTest : MonoBehaviour
{
    private CinemachineImpulseSource impulseSource;

    void Start()
    {
        // Get Cinemachine Impulse Source from Main Camera
        impulseSource = Camera.main.GetComponent<CinemachineImpulseSource>();

        if (impulseSource == null)
        {
            Debug.LogError("CinemachineImpulseSource is missing on the Main Camera!");
        }
    }

    void Update()
    {
        // Trigger impulse when you press the 'Space' key (for manual testing)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (impulseSource != null)
            {
                impulseSource.GenerateImpulse(Vector3.one * 2f); // Generate stronger impulse
                Debug.Log("Impulse Generated!");
            }
        }
    }
}
