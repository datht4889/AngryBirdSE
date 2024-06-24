using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private SlingShotArea slingShotArea;
    [SerializeField] private CinemachineVirtualCamera idleCam;
    [SerializeField] private CinemachineVirtualCamera followCam;
    [SerializeField] private float maxDragDistance = 1000.0f;
    [SerializeField] private float dragSensitivity = 0.01f;
    private Vector3 initialPosition;
    private Vector3 dragStartPosition;
    private bool isDragging = false;

    private void Awake()
    {
        SwitchToIdleCam();
        initialPosition = idleCam.transform.position;
    }

    private void Update()
    {   if (!SlingShotHandler.instances.isShooting)
        {
            // Handle touch input if on mobile or mouse input if on PC
            if (Input.touchCount > 0)
            {
                HandleTouchCameraDragging();
            }
            else
            {
                HandleMouseCameraDragging();
            }
        }
    }

    public void SwitchToIdleCam()
    {
        idleCam.enabled = true;
        followCam.enabled = false;
    }

    public void SwitchToFollowCam(Transform followTransform)
    {
        followCam.Follow = followTransform;
        followCam.enabled = true;
        idleCam.enabled = false;
    }

    private void HandleMouseCameraDragging()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            dragStartPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 dragCurrentPosition = Input.mousePosition;
            Vector3 dragOffset = -dragCurrentPosition + dragStartPosition;
            dragOffset = new Vector3(dragOffset.x, 0f, 0f); 
            dragOffset = Vector3.ClampMagnitude(dragOffset, maxDragDistance);

            idleCam.transform.position = initialPosition - dragOffset * dragSensitivity; // Adjust the multiplier for sensitivity
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            StartCoroutine(ResetCameraPosition());
        }
    }

    private void HandleTouchCameraDragging()
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            isDragging = true;
            dragStartPosition = touch.position;
        }

        if (touch.phase == TouchPhase.Moved && isDragging)
        {
            Vector3 dragCurrentPosition = touch.position;
            Vector3 dragOffset = dragCurrentPosition - dragStartPosition;
            dragOffset = new Vector3(dragOffset.x, 0f, 0f);
            dragOffset = Vector3.ClampMagnitude(dragOffset, maxDragDistance);

            idleCam.transform.position = initialPosition - dragOffset * dragSensitivity; // Adjust the multiplier for sensitivity
        }

        if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            isDragging = false;
            StartCoroutine(ResetCameraPosition());
        }
    }

    private IEnumerator ResetCameraPosition()
    {
        float duration = 0.5f; // Time to move back to the initial position
        float elapsedTime = 0.0f;

        Vector3 startPosition = idleCam.transform.position;

        while (elapsedTime < duration)
        {
            idleCam.transform.position = Vector3.Lerp(startPosition, initialPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        idleCam.transform.position = initialPosition;
    }
}
