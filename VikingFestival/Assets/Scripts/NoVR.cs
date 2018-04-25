using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class NoVR : MonoBehaviour {

	void Start () {
		if (XRSettings.loadedDeviceName == "cardboard") {
			StartCoroutine (ToggleVR ("None"));
		}
	}

	IEnumerator ToggleVR (string deviceName) {
		XRSettings.LoadDeviceByName (deviceName);
		yield return null;
		XRSettings.enabled = false;
	}
}
