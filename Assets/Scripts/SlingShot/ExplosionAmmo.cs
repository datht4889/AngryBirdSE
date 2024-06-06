using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExplosionAmmo : AmmoMechaism
{

    public float fieldofImpact;
    public float force;
    public LayerMask LayerToHit;
    public GameObject ExplosionEffect;

    public AudioSource audioSource;
    public AudioClip audioClip;
    // public override void PowerUp()
    // {
    //     base.PowerUp();
    //     circleCollider.radius = 1.5f;
    //     spriteRenderer.size = new Vector2(3, 3);
    // }

    // void boom(){
        
    //     // audioSource = GetComponent<AudioSource>();
    //     audioSource.clip = audioClip;
    //     audioSource.Play();
    //     ScoreScript.scoreValue += 50;
    //     // Debug.Log("booooooom");
    // }
    void explode() {
        // boom();

        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldofImpact, LayerToHit);
        
        foreach(Collider2D obj in objects) {
            Vector2 direction = obj.transform.position - transform.position;

            obj.GetComponent<Rigidbody2D>().AddForce(direction*force);
        }

        GameObject ExplosionEffectIns = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        Destroy(ExplosionEffectIns, 10);
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected(){
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, fieldofImpact);
    }

    public override void PowerUp(){
            explode();
    }
}
