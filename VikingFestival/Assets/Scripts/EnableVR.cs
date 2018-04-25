using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EnableVR : MonoBehaviour {

	void Start () {
		
		if (XRSettings.loadedDeviceName == "") {
			StartCoroutine (ToggleVR ("Cardboard"));
		}
	}

	IEnumerator ToggleVR (string deviceName) {
		XRSettings.LoadDeviceByName (deviceName);
		yield return null;
		XRSettings.enabled = true;
	}
}
