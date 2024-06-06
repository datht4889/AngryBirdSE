using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

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

    [Header("Ammo")]
    [SerializeField] private AmmoMechaism ammoPrefab;
    [SerializeField] private float AmmoPosOffset = 0.22f;
    [SerializeField] private float TimeBetweenAmmoRespawn = 2f;


    private Vector2 slingShotLinesPosition;

    private bool clickWithinArea;
    private bool ammoOnSlingShot;
    
    private AmmoMechaism spawnedAmmo;


    private Vector2 direction;
    private Vector2 directionNorm;


    private void Awake()
    {
        
        leftLR.enabled = false;
        rightLR.enabled = false;
        spawnAmmo();

    }


    // Update is called once per frame
    private void Update()
    {
        if (Mouse.current.leftButton.isPressed && slingShotArea.isWithinSlingshotArea())
        {
            clickWithinArea = true;
        }

        if (Mouse.current.leftButton.isPressed && clickWithinArea && ammoOnSlingShot)
        {
            DrawSlingShot();
            PositionAndRotateAmmo();
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame && ammoOnSlingShot)
        {
            if (GameManager.instances.HasEnoughAmmos())
            {
                clickWithinArea = false;

                spawnedAmmo.ShootAmmo(direction, shotForce);

                GameManager.instances.UseAmmo();

                ammoOnSlingShot = false;
                SetLines(centerPosition.position);

                if (GameManager.instances.HasEnoughAmmos())
                {
                StartCoroutine(SpawnAmmoAfterTime());
                }
            }
        }


    }

    #region Slingshot methods


    private void DrawSlingShot()
    {   
        

        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        slingShotLinesPosition = (Vector2)centerPosition.position + Vector2.ClampMagnitude((touchPosition - centerPosition.position), maxDistance) ;// new Vector2(centerPosition.position.x, centerPosition.position.y);
        
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
    }    

    #endregion

}
