using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VROnOff : MonoBehaviour {

	public float timeBetweenTouch;
	public GameObject gvr;
	public GameObject mainCamera;
	float newTime;
	int tapCount;
	bool state;

	void Start () {
		mainCamera.SetActive (true);
		gvr.SetActive (false);
	}

	void Update () {
		if (Input.touchCount > 0) {
			if (Input.GetTouch (0).phase == TouchPhase.Ended) {
				tapCount++;
			}
		}

		if (tapCount == 1) {
			newTime = Time.time + timeBetweenTouch;
		} 
		if (tapCount == 2 && Time.time <= newTime) {
			print ("double");
			state = !state;
			mainCamera.SetActive (state);
			gvr.SetActive (!state);

			tapCount = 0;
		}
		if (Time.time > newTime) {
			tapCount = 0;
		}
	}
}
