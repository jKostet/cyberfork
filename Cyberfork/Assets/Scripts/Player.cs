using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float thrust = 50;
    public float maxSpeed = 20;
    public float breakForce = 50;
    public float backSpeed = 10;
    public float rollingFriction = 5;
    public float turningSpeed = 100;

    Rigidbody2D rb;
    ForkManager leftFork;
    ForkManager rightFork;
    FixedJoint2D forkJoint;
    bool isLifting = false;
    float liftedFriction = 0;
    //GameObject lifted = null;
    
    float forwardFriction { get { return thrust / maxSpeed; } }
    float backwardFriction { get { return thrust / backSpeed; } }
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        forkJoint = GetComponent<FixedJoint2D>();
        forkJoint.enabled = false;
        rightFork = transform.GetChild(0).GetComponent<ForkManager>();
        leftFork = transform.GetChild(1).GetComponent<ForkManager>();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateInput();
    }

    void UpdateInput()
    {
        Movement();
        Lift();
    }

    void Movement()
    {

        if (Input.GetKey(KeyCode.W))
        {
            Thrust(rb, thrust, forwardFriction);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            if (Vector2.Dot(rb.velocity, transform.up) > 0)
            {
                rb.AddForce(-breakForce * transform.up);
            }
            else Thrust(rb, -thrust, -backSpeed);
        }

        else
        {
            rb.AddForce(-rollingFriction * rb.velocity);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.angularVelocity = turningSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.angularVelocity = -turningSpeed;
        }
        else rb.angularVelocity = 0;

        rb.velocity = transform.up * Vector2.Dot(transform.up, rb.velocity);
    }

    void Lift()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isLifting) {
                if (leftFork.objectsOnFork.Count > 0)
                {
                    GameObject leftObject = leftFork.objectsOnFork[0];
                    Rigidbody2D otherRb = leftObject.GetComponent<Rigidbody2D>();
                    if (otherRb != null && rightFork.objectsOnFork.Contains(leftObject))
                    {
                        forkJoint.connectedBody = otherRb;
                        liftedFriction = otherRb.drag;
                        otherRb.drag = 0;
                        if (forkJoint.connectedBody != null) forkJoint.enabled = true;
                    }
                }
                leftFork.GetComponent<BoxCollider2D>().isTrigger = false;
                rightFork.GetComponent<BoxCollider2D>().isTrigger = false;
            }
            else
            {
                forkJoint.enabled = false;
                Rigidbody2D otherRb = forkJoint.connectedBody;
                if (otherRb != null)
                {
                    otherRb.drag = liftedFriction;
                    forkJoint.connectedBody = null;
                }
                leftFork.GetComponent<BoxCollider2D>().isTrigger = true;
                rightFork.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            isLifting = !isLifting;
        }
    }

    void Thrust(Rigidbody2D rb, float thrust, float friction)
    {
        rb.AddForce((thrust - friction * rb.velocity.magnitude) * transform.up);
    }
}
