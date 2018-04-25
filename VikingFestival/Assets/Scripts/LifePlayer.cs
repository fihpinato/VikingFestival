using UnityEngine;
using UnityEngine.UI;

public class LifePlayer : MonoBehaviour {

	void OnCollisionEnter (Collision c) {
		if (c.collider.CompareTag ("Barrel")) {
            HUDBarcoScript.Instance.timer += 10f;
            Destroy(c.gameObject);
		}
	}
}
