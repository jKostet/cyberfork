using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectsToGoal : MonoBehaviour {
    public GameObject goal;
    public List<GameObject> objectsToGoal;
    public float dist = 5;

    private void Update()
    {
        List<GameObject> removable = new List<GameObject>();
        foreach (var obj in objectsToGoal)
        {
            Debug.Log((goal.transform.position - obj.transform.position).sqrMagnitude);
            if ((goal.transform.position - obj.transform.position).sqrMagnitude < dist*dist)
            {
                removable.Add(obj);
            }
        }
        foreach (var obj in removable)
        {
            objectsToGoal.Remove(obj);
        }
    }

    public bool Completed()
    {
        return objectsToGoal.Count == 0;
    }
}
