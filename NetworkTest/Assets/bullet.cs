using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

	void OnCollisionEnter(Collision collision) {
		// Store ref to the GameObject you're colliding with
		var hit = collision.gameObject;
		// Check if the GameObject has the PlayerMove component
		var hitCombat = hit.GetComponent<Combat> ();
		if (hitCombat != null) {
			// Deal damage to the target
			hitCombat.TakeDamage(10);

			// Make this object (bullet) disappear
			Destroy (gameObject);
		}
	}
}
