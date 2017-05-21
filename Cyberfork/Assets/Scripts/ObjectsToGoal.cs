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
            Debug.Log((goal.transform.position));
            if (Distance2D(goal.transform, obj.transform) < dist*dist)
            {
                removable.Add(obj);
            }
        }
        foreach (var obj in removable)
        {
            objectsToGoal.Remove(obj);
        }
    }

    float Distance2D(Transform t1, Transform t2)
    {
        var v1 = new Vector3(t1.position.x, t1.position.y);
        var v2 = new Vector3(t2.position.x, t2.position.y);
        return (v1 - v2).sqrMagnitude;
    }

    public bool Completed()
    {
        return objectsToGoal.Count == 0;
    }
}
