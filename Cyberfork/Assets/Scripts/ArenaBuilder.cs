using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArenaBuilder : MonoBehaviour {

    public int width = 30;
    public int height = 10;
    public GameObject wall;

	// Use this for initialization
	void Awake() {
        CreateWalls();
    }

    void CreateWalls()
    {
        GameObject arena = new GameObject("ArenaBorders");
        for (int x = 0; x <= width + 2; x++)
        {
            GameObject wall1 = Instantiate(wall, new Vector3(x, 0, 0), Quaternion.identity);
            GameObject wall2 = Instantiate(wall, new Vector3(x, height + 2, 0), Quaternion.identity);
            wall1.transform.parent = arena.transform;
            wall2.transform.parent = arena.transform;
        }

        for (int y = 1; y <= height + 1; y++)
        {
            GameObject wall1 = Instantiate(wall, new Vector3(0, y, 0), Quaternion.identity);
            GameObject wall2 = Instantiate(wall, new Vector3(width + 2, y, 0), Quaternion.identity);
            wall1.transform.parent = arena.transform;
            wall2.transform.parent = arena.transform;
        }
    }
}
