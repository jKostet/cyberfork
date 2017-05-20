using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Explosive : NetworkBehaviour, Carriable{

    public float time;
    public GameObject explosion;

    public bool triggered = false;

    float timeTriggered = 0;

    public void PickUp() { }
    public void PickDown()
    {
        triggered = true;
    }

    private void Update()
    {
        if (triggered)
        {
            timeTriggered += Time.deltaTime;
            if(timeTriggered > time)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
