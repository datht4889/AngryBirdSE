using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class SlingShotHandler : MonoBehaviour
{
    [Header("Line Render")]
    [SerializeField] private LineRenderer leftLR;
    [SerializeField] private LineRenderer rightLR;

    [Header("Transform Render")]
    [SerializeField] private Transform leftStartPosition;
    [SerializeField] private Transform rightStartPosition;
    [SerializeField] private Transform centerPosition;
    [SerializeField] private Transform idlePosition;

    [Header("SlingShot Stats")]
    [SerializeField] private float maxDistance = 1.5f;
    [SerializeField] private float shotForce = 5f;

    [Header("Scripts")]
    [SerializeField] private SlingShotArea slingShotArea;
    [SerializeField] private CameraManager cameraManager;

    [Header("Ammo")]
    [SerializeField] private AmmoMechaism ammoPrefab;
    [SerializeField] private float AmmoPosOffset = 0.22f;
    [SerializeField] private float TimeBetweenAmmoRespawn = 2f;

    private Vector2 slingShotLinesPosition;
    private bool ammoOnSlingShot;
    private AmmoMechaism spawnedAmmo;
    private Vector2 direction;
    private Vector2 directionNorm;
    public bool isShooting;
    public static SlingShotHandler instances;
    private void Awake()
    {
        if (instances == null)
        {
            instances = this;
        }

        leftLR.enabled = false;
        rightLR.enabled = false;
        spawnAmmo();
    }

    private void Update()
    {
        bool isTouching = Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame;
        bool isClicking = Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;

        
        if ((isClicking || isTouching)  && slingShotArea.isWithinSlingshotArea())
        {
            isShooting = true;
            cameraManager.SwitchToFollowCam(spawnedAmmo.transform);
        }

        if (isShooting && ammoOnSlingShot)
        {
            DrawSlingShot();
            PositionAndRotateAmmo();
        }

        bool wasReleased = (Mouse.current != null && Mouse.current.leftButton.wasReleasedThisFrame) ||
                           (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasReleasedThisFrame);

        if (wasReleased && ammoOnSlingShot && isShooting)
        {
            if (GameManager.instances.HasEnoughAmmos())
            {
                isShooting = false;
                ammoOnSlingShot = false;
                spawnedAmmo.ShootAmmo(direction, shotForce);
                GameManager.instances.UseAmmo();
                
                SetLines(centerPosition.position);

                if (GameManager.instances.HasEnoughAmmos())
                {
                    StartCoroutine(SpawnAmmoAfterTime());
                }
                else { 
                    StartCoroutine(CheckLoseWin());
                }
               
            }
        }
    }

    #region Slingshot methods

    private void DrawSlingShot()
    {
        Vector2 touchPosition;

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else
        {
            touchPosition = Mouse.current.position.ReadValue();
        }

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
        slingShotLinesPosition = (Vector2)centerPosition.position + Vector2.ClampMagnitude((worldPosition - centerPosition.position), maxDistance);

        SetLines(slingShotLinesPosition);
        direction = (Vector2)centerPosition.position - slingShotLinesPosition;
        directionNorm = direction.normalized;
    }

    private void SetLines(Vector2 position)
    {
        if (!leftLR.enabled && !rightLR.enabled)
        {
            leftLR.enabled = true;
            rightLR.enabled = true;
        }

        leftLR.SetPosition(0, position);
        leftLR.SetPosition(1, leftStartPosition.position);
        rightLR.SetPosition(0, position);
        rightLR.SetPosition(1, rightStartPosition.position);
    }

    #endregion

    #region Ammo Methods

    private void spawnAmmo()
    {
        SetLines(idlePosition.position);
        Vector2 dir = (centerPosition.position - idlePosition.position).normalized;
        Vector2 spawnPosition = (Vector2)idlePosition.position + dir * AmmoPosOffset;
        spawnedAmmo = Instantiate(ammoPrefab, spawnPosition, Quaternion.identity);
        spawnedAmmo.transform.right = dir;
        ammoOnSlingShot = true;
    }

    private void PositionAndRotateAmmo()
    {
        spawnedAmmo.transform.position = slingShotLinesPosition + directionNorm * AmmoPosOffset;
        spawnedAmmo.transform.right = directionNorm;
    }

    private IEnumerator SpawnAmmoAfterTime()
    {
        yield return new WaitForSeconds(TimeBetweenAmmoRespawn);
        spawnAmmo();
        cameraManager.SwitchToIdleCam();
    }

    private IEnumerator CheckLoseWin()
    {
        
        bool allStopped = false;

        while (!allStopped)
        {   
            allStopped = true;
            Rigidbody2D[] allRigidbodies = FindObjectsOfType<Rigidbody2D>();
            foreach (Rigidbody2D rb in allRigidbodies)
            {
                if (rb.velocity.magnitude > 0.1f)
                {
                    allStopped = false;
                    break;
                }
            }

            yield return new WaitForSeconds(0.5f);
        }

        
        if (!GameManager.instances.CheckForEndGame())
        {

            GameManager.instances.LoseGame();
        }
    }
  
    #endregion
}
