using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemLookAt : MonoBehaviour {

	public float speed = 1f;
    public float increaseSpeed = 0.05f;
    public float timeToIncrease = 20f;
	public Transform player;
    float timer;


	void Update () {
        if (HUDBarcoScript.Instance.timerActive) {
            timer += Time.deltaTime;
            var rotatio = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotatio, Time.deltaTime * speed);
            if (timer > timeToIncrease) {
                speed += increaseSpeed;
                timer = 0;
            }
        }
	}

    public void Reset () {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
