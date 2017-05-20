using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Winning : NetworkBehaviour {
    public ObjectsToGoal[] objectives;

    public void OnStart()
    {
        objectives = GetComponents<ObjectsToGoal>();
        Debug.Log(objectives.Length);
    }

    public int Winner()
    {
        for (int i = 0; i < objectives.Length; i++)
        {
            if (objectives[i].Completed())
            {
                Debug.Log("Winner is team " + i + "!");
                return i;
            }
        }
        return -1;
    }
}
