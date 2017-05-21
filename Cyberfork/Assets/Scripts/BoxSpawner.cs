using UnityEngine;
using UnityEngine.Networking;

public class BoxSpawner : NetworkBehaviour {

	public GameObject Box;
	public int numBoxes;
    public float distFromWalls;
    public Transform corner1;
    public Transform corner2;

    float x1 { get { return corner1.position.x; } }
    float y1 { get { return corner1.position.y; } }
    float x2 { get { return corner2.position.x; } }
    float y2 { get { return corner2.position.y; } }
    public override void OnStartServer()
	{
		for (int i=0; i < numBoxes; i++)
		{
            Vector3 pos = new Vector3(Random.Range(x1, x2),
                                        Random.Range(y1, y2), 0);
            float rotation = Random.Range(0, 360);
            GameObject box = Instantiate(Box, pos, Quaternion.identity);
            box.transform.Rotate(0, 0, rotation);
            NetworkServer.Spawn(box);
			//var pos = new Vector3(
			//	Random.Range(1.0f, 8.0f),
			//	Random.Range(4.0f, 7.0f),
			//	Random.Range(1.0f, 8.0f)
			//);

			////var rotation = Quaternion.Euler( Random.Range(0,180), Random.Range(0,180), Random.Range(0,180));
			//var rotation = Quaternion.Euler(0, 0, 0);

			//var boxi = (GameObject)Instantiate(Box, pos, rotation);
			//NetworkServer.Spawn(boxi);
		}
	}
}