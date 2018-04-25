using UnityEngine;

public class MoveClouds : MonoBehaviour {

    public float maxVelocity = 3f;
    public float minVelocity = 1f;
    public float maxZ = 200f;

    float velocity;

    void Start () {
        velocity = Random.Range(minVelocity, maxVelocity);
	}

	void Update () {
        transform.Translate(Vector3.forward * velocity * Time.deltaTime);
        if(transform.position.z > maxZ) {
            transform.position = new Vector3(transform.position.x, transform.position.y, -maxZ);
        }
	}
}
