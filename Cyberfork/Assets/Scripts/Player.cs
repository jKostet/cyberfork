using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject player;
    public float thrust;
    public float maxSpeed;
    public float breakForce;
    public float backSpeed;
    public float rollingFriction;

    public float turnPower = 10;
    public float turningSpeed = 100;

    Rigidbody2D rb;

    private float forwardFriction { get { return thrust / maxSpeed; } }
    private float backwardFriction { get { return thrust / backSpeed; } }
    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent <Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateInput();
    }

    void UpdateInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Thrust(rb, thrust, forwardFriction);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            if (Vector2.Dot(rb.velocity, transform.right) > 0)
            {
                rb.AddForce(-breakForce * transform.right);
            }
            else Thrust(rb, -thrust, -backSpeed);
        }

        else
        {
            rb.AddForce(-rollingFriction * rb.velocity);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.angularVelocity = turnPower;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.angularVelocity = -turnPower;
        }
        else rb.angularVelocity = 0;

        rb.velocity = transform.right * Vector2.Dot(transform.right, rb.velocity);
    }

    void Thrust(Rigidbody2D rb, float thrust, float friction)
    {
        rb.AddForce((thrust - friction * rb.velocity.magnitude) * transform.right);
    }
}
