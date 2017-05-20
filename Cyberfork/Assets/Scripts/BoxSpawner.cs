using UnityEngine;
using UnityEngine.Networking;

public class BoxSpawner : NetworkBehaviour {

	public GameObject Box;
	public int numBoxes;

	public override void OnStartServer()
	{
		for (int i=0; i < numBoxes; i++)
		{
			var pos = new Vector3(
				Random.Range(1.0f, 8.0f),
				Random.Range(4.0f, 7.0f),
				Random.Range(1.0f, 8.0f)
			);

			//var rotation = Quaternion.Euler( Random.Range(0,180), Random.Range(0,180), Random.Range(0,180));
			var rotation = Quaternion.Euler(0, 0, 0);

			var boxi = (GameObject)Instantiate(Box, pos, rotation);
			NetworkServer.Spawn(boxi);
		}
	}
}