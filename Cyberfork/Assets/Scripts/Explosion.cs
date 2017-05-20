using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public float radius = 10;
    public float expansionTime = 0.5f;
    public float sustain = 1;
    public float knockback = 0.5f;
    public float damage = 50;
    public bool isExploding = false;
    float time = 0;
    CircleCollider2D domain;

    void Start()
    {
        domain = GetComponent<CircleCollider2D>();
    }

    void Update () {
        if (isExploding)
        {
            float dt = Time.deltaTime;
            domain.radius = Mathf.Min(radius * time / expansionTime, radius);
            time += dt;
            if (time > expansionTime + sustain) Destroy(this);
        }
	}

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isExploding)
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(knockback * (other.transform.position - transform.position));
                Health health = other.GetComponent<Health>();
                if (health != null) health.Damage(damage * Time.deltaTime / (expansionTime + sustain));
            }
            
        }
    }
}
