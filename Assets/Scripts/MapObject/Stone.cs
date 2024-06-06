using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public float MAX_HEALTH = 40f; 
    public float DAMAGE_THRESHOLD = 1f;

    private float current_health;

    // Start is called before the first frame update
    void Start()
    {
        current_health = MAX_HEALTH;
    }

    public void Damage(float damage){
        current_health -= damage;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision){
        float impactVelocity = collision.relativeVelocity.magnitude;
        if (impactVelocity >= DAMAGE_THRESHOLD){
            Damage(impactVelocity);
        }
        if (current_health<=0f) {
            Destroy(gameObject);
            ScoreScript.scoreValue += 50;
        }
    }
}
