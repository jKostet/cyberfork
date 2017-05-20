using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Gamemode : NetworkBehaviour {

    public GameObject goal;
    public GameObject[] itemsToGoal;
    public float numOfItems;
    public float distFromWalls;
    ArenaBuilder arenaBuilder;
    ObjectsToGoal[] objectives;

    public Winning winning;

    float width;
    float height;

    private void Start()
    {
        winning = GetComponent<Winning>();
    }

    public override void OnStartServer()
    {
        winning = GetComponent<Winning>();
        objectives = GetComponents<ObjectsToGoal>();
        arenaBuilder = FindObjectOfType<ArenaBuilder>();
        width = arenaBuilder.width;
        height = arenaBuilder.height;
        GameObject goal1 = Instantiate(goal, new Vector3(2, height / 2 + 1, 0), Quaternion.identity);
        GameObject goal2 = Instantiate(goal, new Vector3(width, height / 2 + 1, 0), Quaternion.identity);
        objectives[0].goal = goal1;
        objectives[1].goal = goal2;
        foreach (var obj in itemsToGoal)
        {
            for (int i = 0; i < numOfItems; i++)
            {
                Vector3 pos = new Vector3(Random.Range(distFromWalls+1, width-distFromWalls+1),
                                          Random.Range(distFromWalls+1, height-distFromWalls+1), 0);
                float rotation = Random.Range(0, 360);
                GameObject instObj = Instantiate(obj, pos, Quaternion.identity);
                instObj.transform.Rotate(0, 0, rotation);
                objectives[0].objectsToGoal.Add(instObj);
                objectives[1].objectsToGoal.Add(instObj);
            }
        }

        winning.OnStart();
        winning.enabled = true;
    }

}
