using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour {

	void OnCollisionEnter (Collision c) {
		if (c.collider.CompareTag ("Axe")) {
			if (c.collider.CompareTag ("Player")) {
				Destroy (gameObject);
			}
		}
	}
}
