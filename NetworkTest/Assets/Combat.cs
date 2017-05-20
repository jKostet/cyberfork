﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Combat : NetworkBehaviour {

	public const int maxHealth = 100;
	public bool destroyOnDeath;

	[SyncVar]
	public int health = maxHealth;

	public void TakeDamage(int amount) {
		if (!isServer) {
			return;
		}
		health -= amount;
		if (health <= 0) {
			if (!destroyOnDeath) {
				health = maxHealth;
				RpcRespawn ();
			} else {
				Destroy (gameObject);
			}
		}
	}

	[ClientRpc]
	void RpcRespawn() {
		if (isLocalPlayer) {
			transform.position = Vector3.zero;
		}
	}

	public int getHealth() {
		return this.health;
	}
}
