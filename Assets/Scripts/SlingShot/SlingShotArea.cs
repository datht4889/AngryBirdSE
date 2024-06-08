using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine;
using UnityEngine.InputSystem; // Ensure you have the Input System package if using the new Input System

public class SlingShotArea : MonoBehaviour
{
    [SerializeField] private LayerMask slingShotAreaMask;

    public bool isWithinSlingshotArea()
    {
        Vector2 worldPosition;

        // Check if we're running on a touch device
        if (Input.touchCount > 0)
        {
            // Get the position of the first touch
            Touch touch = Input.GetTouch(0);
            worldPosition = Camera.main.ScreenToWorldPoint(touch.position);
        }
        else
        {
            // Fallback to mouse position if no touch input is detected
            worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }

        if (Physics2D.OverlapPoint(worldPosition, slingShotAreaMask))
        {
            return true;
        }
        return false;
    }
}

