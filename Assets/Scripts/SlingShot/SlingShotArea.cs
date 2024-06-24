using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlingShotArea : MonoBehaviour
{
    [SerializeField] private LayerMask slingShotAreaMask;

    public bool isWithinSlingshotArea()
    {
        Vector2 worldPosition;

        
         Touch touch = Input.GetTouch(0);
         worldPosition = Camera.main.ScreenToWorldPoint(touch.position);
        
       

        if (Physics2D.OverlapPoint(worldPosition, slingShotAreaMask))
        {
            return true;
        }
        return false;
    }
}

