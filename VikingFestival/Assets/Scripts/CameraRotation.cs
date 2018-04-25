using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

	Vector3 firstPoint, secondPoint;
	float xAngle, xAngTemp;

	void Start () {
		xAngle = 0;
		transform.rotation = Quaternion.Euler (0, xAngle, 0);
	}

	void Update () {
		TouchCommand ();
	}

	void TouchCommand () {
		if (Input.touchCount > 0) {
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				firstPoint = Input.GetTouch (0).position;
				xAngTemp = xAngle;
			}

			if (Input.GetTouch (0).phase == TouchPhase.Moved) {
				secondPoint = Input.GetTouch (0).position;
				xAngle = xAngTemp - (secondPoint.x - firstPoint.x) * 180 / Screen.width;

				transform.rotation = Quaternion.Euler (0, xAngle, 0);
			}
		}
	}
}
