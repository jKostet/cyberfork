using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour {

	public GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
		
	}

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer> ().material.color = Color.red;
	}

	// Update is called once per frame
	void Update () {

		if (!isLocalPlayer) {
			return;
		}

		var x = Input.GetAxis ("Horizontal") * 0.1f;
		var z = Input.GetAxis ("Vertical") * 0.1f;

		transform.Translate (x, 0, z);

		if (Input.GetKeyDown(KeyCode.Space)) {
			CmdFire();
		}
	}

	[Command]
	void CmdFire() {
		// create new bullet object
		var bullet = (GameObject)Instantiate(bulletPrefab, transform.position - transform.forward, Quaternion.identity);

		// shoot the bullet forward
		bullet.GetComponent<Rigidbody>().velocity = -transform.forward*4;

		// Spawn the bullet on the server
		NetworkServer.Spawn(bullet);

		// bullet self-destruct after 2 sec
		Destroy(bullet, 2.0f);
	}
}
