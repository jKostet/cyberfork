using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BombSpawner : NetworkBehaviour {

    public GameObject bomb;
    public Transform spawnPosition;
    public int numOfBombs = 1;
    public float spawnInterval = 4;

    List<GameObject> spawnedBombs;
    float lastSpawned = 0;
    bool hasStarted = false;
    // Update is called once per frame
    public override void OnStartServer()
    {
        spawnedBombs = new List<GameObject>();
        hasStarted = true;
    }
    void Update ()
    {
        if (!hasStarted) return;
        List<GameObject> destroyed = new List<GameObject>();

        foreach (var b in spawnedBombs)
        {
            if (b == null) destroyed.Add(b);
        }

        foreach (var dest in destroyed)
        {
            spawnedBombs.Remove(dest);
        }

        if(spawnedBombs.Count < numOfBombs)
        {
            lastSpawned += Time.deltaTime;

            if (lastSpawned > spawnInterval)
            {
                foreach (var b in spawnedBombs)
                {
                    if ((b.transform.position - spawnPosition.position).sqrMagnitude < 5) return;
                }
                Debug.Log(gameObject.name);
                GameObject bo = Instantiate(bomb, spawnPosition);
                NetworkServer.Spawn(bo);
                spawnedBombs.Add(bo);
                lastSpawned = 0;
            }
        }
	}
}
