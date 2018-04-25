using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemShot : MonoBehaviour {

	public float timeBetweenShots = 1f;
	public GameObject barrelPrefab;
	public Transform origin;
	public Transform aim;

	RaycastHit hit;
	Ray ray;
	float timer;

	void Start () {
		origin.LookAt (aim.position);
	}

	void Update () {
        if (HUDBarcoScript.Instance.timerActive) {
            timer += Time.deltaTime;
            if (Physics.Linecast(origin.position, aim.position, out hit)) {
                if (hit.collider.CompareTag("Player") && timer >= timeBetweenShots) {
                    var barrel = Instantiate(barrelPrefab, origin.transform.position, origin.transform.rotation);
                    Destroy(barrel, 3f);
                    timer = 0;
                }
            }
        }
	}
}
