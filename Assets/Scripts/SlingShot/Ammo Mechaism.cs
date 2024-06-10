using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AmmoMechaism : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected CircleCollider2D circleCollider;

    protected EventTrigger eventTrigger;
    protected SpriteRenderer spriteRenderer;

    protected bool isPowered = false;
    protected bool isShooted;
    protected bool shouldFaceVelDirection;


    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.drawMode = SpriteDrawMode.Sliced;
    }

    protected void FixedUpdate()
    {   if (isShooted && shouldFaceVelDirection)
        {
            transform.right = rb.velocity;
        }    
            
    }
    protected void Start()
    {
        rb.isKinematic = true;
        circleCollider.enabled = false;
    }
    public void ShootAmmo( Vector2 direction, float force)
    {
        rb.isKinematic = false;
        circleCollider.enabled = true;

        //apply force
        rb.AddForce(direction * force, ForceMode2D.Impulse);

        isShooted = true;
        shouldFaceVelDirection = true;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision){
        shouldFaceVelDirection = false;

        float impactVelocity = collision.relativeVelocity.magnitude;
        if (impactVelocity >= 3f && isPowered == false){
            PowerUp();
            isPowered = true;
        }
    }

    // protected void OnMouseDown()
    // {
    //     if (isPowered == false){
    //         PowerUp();
    //         isPowered = true;
    //     }
    // }

    public virtual void PowerUp()
    {

    }
}
