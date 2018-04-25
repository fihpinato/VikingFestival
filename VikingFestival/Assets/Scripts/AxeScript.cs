using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeScript : MonoBehaviour {

	void OnCollisionEnter (Collision c) {
		if (c.collider.CompareTag ("Barrel")) {
            Destroy(c.gameObject);
            Destroy(gameObject);
		} else {
            Destroy(gameObject);
        }
	}
}
