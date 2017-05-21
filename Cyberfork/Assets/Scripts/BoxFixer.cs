using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BoxFixer : NetworkBehaviour {
    
    public GameObject box;
    public GameObject scrap;
    public Transform spawnPoint;
    public int neededAmount;

    float amount = 0;

    public override void OnStartServer()
    {
        if (spawnPoint == null) spawnPoint = transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == string.Format("{0}(Clone)", scrap.name))
        {
            Destroy(collision.gameObject);
            amount += 1;
            if(amount >= neededAmount)
            {
                NetworkServer.Spawn(Instantiate(box, spawnPoint));
                amount = 0;
            }
        }
    }

}
