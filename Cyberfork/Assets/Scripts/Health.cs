using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	[SyncVar]
    public float health = 100;

    public void Damage(float amount)
    {
        health -= amount;
        if (health < 0) Destroy(gameObject);
    }

    private void Update()
    {
        if (health < 0) Destroy(gameObject);
    }

}
