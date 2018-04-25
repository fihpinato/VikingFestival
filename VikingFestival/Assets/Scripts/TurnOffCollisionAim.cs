using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffCollisionAim : MonoBehaviour {

    BlinkAimTotem blink;

    void Start () {
        blink = FindObjectOfType<BlinkAimTotem>();
    }

	void OnCollisionEnter (Collision c) {
		if (c.collider.CompareTag ("Axe")) {
            blink.activeCheck--;
			gameObject.SetActive (false);
		}
	}
}
