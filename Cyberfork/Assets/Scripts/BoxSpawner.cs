using UnityEngine;
using UnityEngine.Networking;

public class BoxSpawner : NetworkBehaviour {

	public GameObject Box;
	public int numBoxes;
    public float distFromWalls;

    float width;
    float height;
    public override void OnStartServer()
	{
        ArenaBuilder arenaBuilder = FindObjectOfType<ArenaBuilder>();
        width = arenaBuilder.width;
        height = arenaBuilder.height;
		for (int i=0; i < numBoxes; i++)
		{
            Vector3 pos = new Vector3(Random.Range(distFromWalls+1, width-distFromWalls+1),
                                        Random.Range(distFromWalls+1, height-distFromWalls+1), 0);
            float rotation = Random.Range(0, 360);
            GameObject box = Instantiate(Box, pos, Quaternion.identity);
            box.transform.Rotate(0, 0, rotation);
            Debug.Log("moi");
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