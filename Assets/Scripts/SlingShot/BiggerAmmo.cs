using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BiggerAmmo : AmmoMechaism
{
    public override void PowerUp()
    {
        base.PowerUp();
        rb.mass = 5f;
        circleCollider.radius = 1.5f;
        spriteRenderer.size = new Vector2(3, 3);
    }
}
