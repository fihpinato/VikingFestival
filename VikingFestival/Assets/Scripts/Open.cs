using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class Open : MonoBehaviour {

	public void OpenAR () {
		SceneManager.LoadScene ("AR");
	}

	public void OpenVR () {
		SceneManager.LoadScene ("VR");
	}

	public void OpenVRAR () {
		SceneManager.LoadScene ("VRAR");
	}
}
