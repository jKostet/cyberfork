using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Gamemode : NetworkBehaviour {

    public GameObject goal;
    public GameObject itemToGoal;
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
        Transform bombPos = transform.GetChild(0);
        Transform goal1Pos = transform.GetChild(1);
        Transform goal2Pos = transform.GetChild(2);

        GameObject bomb = Instantiate(itemToGoal, bombPos);
        GameObject goal1 = Instantiate(goal, goal1Pos);
        GameObject goal2 = Instantiate(goal, goal2Pos);
        objectives[0].goal = goal1;
        objectives[0].objectsToGoal.Add(bomb);
        objectives[1].goal = goal2;
        objectives[1].objectsToGoal.Add(bomb);
        NetworkServer.Spawn(bomb);
        NetworkServer.Spawn(goal1);
        NetworkServer.Spawn(goal2);
        //foreach (var obj in itemsToGoal)
        //{
        //    for (int i = 0; i < numOfItems; i++)
        //    {
        //        Vector3 pos = new Vector3(Random.Range(distFromWalls+1, width-distFromWalls+1),
        //                                  Random.Range(distFromWalls+1, height-distFromWalls+1), 0);
        //        float rotation = Random.Range(0, 360);
        //        GameObject instObj = Instantiate(obj, pos, Quaternion.identity);
        //        instObj.transform.Rotate(0, 0, rotation);
        //        objectives[0].objectsToGoal.Add(instObj);
        //        objectives[1].objectsToGoal.Add(instObj);
        //    }
        //}

        winning.OnStart();
        winning.enabled = true;
    }

}
