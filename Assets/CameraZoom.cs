using UnityEngine;
using System.Collections;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private Coroutine zoomRoutine;

    public float defaultZoom = 5f;
    public float zoomedInSize = 3.5f;
    public float zoomSpeed = 5f;

    void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        if (vcam == null)
        {
            Debug.LogError("CameraZoom: No CinemachineVirtualCamera found!");
        }
    }

    public void ZoomIn(float duration)
    {
        Debug.Log("ZoomIn called via Cinemachine");

        if (zoomRoutine != null)
            StopCoroutine(zoomRoutine);

        zoomRoutine = StartCoroutine(ZoomInOut(duration));
    }

    private IEnumerator ZoomInOut(float duration)
    {
        float current = vcam.m_Lens.OrthographicSize;

        // Zoom in
        while (Mathf.Abs(vcam.m_Lens.OrthographicSize - zoomedInSize) > 0.01f)
        {
            vcam.m_Lens.OrthographicSize = Mathf.MoveTowards(
                vcam.m_Lens.OrthographicSize,
                zoomedInSize,
                Time.deltaTime * zoomSpeed
            );
            yield return null;
        }

        vcam.m_Lens.OrthographicSize = zoomedInSize;

        yield return new WaitForSeconds(duration);

        // Zoom out
        while (Mathf.Abs(vcam.m_Lens.OrthographicSize - defaultZoom) > 0.01f)
        {
            vcam.m_Lens.OrthographicSize = Mathf.MoveTowards(
                vcam.m_Lens.OrthographicSize,
                defaultZoom,
                Time.deltaTime * zoomSpeed
            );
            yield return null;
        }

        vcam.m_Lens.OrthographicSize = defaultZoom;
    }
}
