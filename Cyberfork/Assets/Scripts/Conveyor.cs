using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour {

    public float friction = 10;
    public float speed = 5;
    
    List<GameObject> objectsOnConveyor;
	// Use this for initialization
	void Start () {
        objectsOnConveyor = new List<GameObject>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        foreach (var gobj in objectsOnConveyor)
        {
            Thrust(gobj.GetComponent<Rigidbody2D>(), friction, friction / speed);
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.GetComponent<Rigidbody2D>())
            objectsOnConveyor.Add(col.gameObject);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        objectsOnConveyor.Remove(col.gameObject);
    }

    void Thrust(Rigidbody2D rb, float thrust, float friction)
    {
        rb.velocity = Vector3.Lerp(rb.velocity, transform.up * speed, friction * Time.deltaTime);
    }
}
