using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	[SyncVar]
    public float health = 100;

    Destroyable dest;

    public override void OnStartServer()
    {
        dest = GetComponent<Destroyable>();
    }

    public void Damage(float amount)
    {
        health -= amount;
        if (health < 0)
        {
            dest.DestroyThis();
            enabled = false;
        }
    }

}
