using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkManager : MonoBehaviour {

    [HideInInspector]  public List<GameObject> objectsOnFork;
	// Use this for initialization
	void Start () {
        objectsOnFork = new List<GameObject>();
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        {
            objectsOnFork.Add(col.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        objectsOnFork.Remove(col.gameObject);
    }

}
