using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Timeline.Actions;
using UnityEngine;

public class Bomb2D : MonoBehaviour
{
    public float fieldofImpact;
    public float force;
    public float eplosion_threshold = 3f;
    public LayerMask LayerToHit;
    public GameObject ExplosionEffect;

    public AudioSource audioSource;
    public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        // audioSource = GetComponent<AudioSource>();
        // audioSource.clip = audioClip;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void boom(){
        
        // audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
        ScoreScript.scoreValue += 50;
        // Debug.Log("booooooom");
    }
 
    void explode() {
        boom();

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

    private void OnCollisionEnter2D(Collision2D collision){
        float impactVelocity = collision.relativeVelocity.magnitude;
        if (impactVelocity >= eplosion_threshold){
            explode();
        }
    }


}
