using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;  
    private Vector3 originalPos;
    private bool isShaking = false;
    private float shakeMagnitude = 2f;
    private float shakeDuration = 0.5f;

   
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


    public void TriggerShake(float magnitude, float duration)
    {
        originalPos = transform.localPosition;
        shakeMagnitude = magnitude;
        shakeDuration = duration;
        isShaking = true;
    }

    void Update()
    {
        if (isShaking)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeMagnitude;


            shakeDuration -= Time.deltaTime;


            if (shakeDuration <= 0)
            {
                isShaking = false;
                transform.localPosition = originalPos;
            }
        }
    }
}