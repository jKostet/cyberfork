using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public float thrust = 50;
    public float maxSpeed = 12;
    public float breakForce = 50;
    public float backSpeed = 10;
    public float rollingFriction = 5;
    public float turningSpeed = 200;

    public float boost = 50;
    public float boostTime = 3;
    public float recoverTime = 1;
    public float coolDown = 0.2f;

    Rigidbody2D rb;
    ForkManager leftFork;
    ForkManager rightFork;
    FixedJoint2D forkJoint;
    bool isLifting = false;
    float lastBoost = 0;
    float boostLeft;
    float boostPower;
    float liftedFriction = 0;
    //GameObject lifted = null;
    
    float forwardFriction { get { return thrust / maxSpeed; } }
    float backwardFriction { get { return thrust / backSpeed; } }
    // Use this for initialization
    void Start()
    {
        boostPower = boost;
        boostLeft = boostTime;
        rb = GetComponent<Rigidbody2D>();
        forkJoint = GetComponent<FixedJoint2D>();
        forkJoint.enabled = false;
        rightFork = transform.GetChild(0).GetComponent<ForkManager>();
        leftFork = transform.GetChild(1).GetComponent<ForkManager>();
    }
    // Update is called once per frame
    void Update()
    {
		if (!isLocalPlayer) {
			return;
		}
        UpdateInput();
    }

    void UpdateInput()
    {
        Movement();
        Lift();
        lastBoost += Time.deltaTime;
        if (lastBoost > coolDown)
        {
            boostLeft = Mathf.Min(boostLeft + boostTime * Time.deltaTime / recoverTime, boostTime);
        }
    }

    void Movement()
    {

        if (Input.GetKey(KeyCode.W))
        {
            Thrust(rb, thrust, forwardFriction);
            Debug.Log(boostTime);
            if (boostTime > 0 && Input.GetKey(KeyCode.LeftShift))
            {
                lastBoost = 0;
                Thrust(rb, boost, forwardFriction);
                boostTime -= Time.deltaTime;
            }
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
                    if(leftObject != null)
                    {
                        Rigidbody2D otherRb = leftObject.GetComponent<Rigidbody2D>();
                        if (otherRb != null && rightFork.objectsOnFork.Contains(leftObject))
                        {
                            forkJoint.connectedBody = otherRb;
                            liftedFriction = otherRb.drag;
                            otherRb.drag = 0;
                            Carriable car = leftObject.GetComponent<Carriable>();
                            if (car != null) car.PickUp();
                            if (forkJoint.connectedBody != null) forkJoint.enabled = true;
                        }

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
                    Carriable car = otherRb.gameObject.GetComponent<Carriable>();
                    if (car != null) car.PickDown();
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
