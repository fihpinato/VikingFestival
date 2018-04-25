using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworksSpawn : MonoBehaviour {

    public float clampX = 29f;
    public float minClampY = 5f;
    public float maxClampY = 10f;
    public float minClampZ = 0f;
    public float maxClampZ = 29f;
    
    public float timeBetweenFirework = 2f;
    public GameObject[] fireworks;

	void Start () {
        StartCoroutine(FireworkCoroutine(timeBetweenFirework));
	}

    IEnumerator FireworkCoroutine (float t) {
        Vector3 randPos = new Vector3(Random.Range(-clampX, clampX),
            Random.Range(minClampY, maxClampY),
            Random.Range(minClampZ, maxClampZ));

        int randFirework = Random.Range(0, fireworks.Length);
        GameObject firework = Instantiate(fireworks[randFirework], randPos, fireworks[randFirework].transform.rotation);
        Destroy(firework, 3f);
        yield return new WaitForSeconds(t);
        StartCoroutine(FireworkCoroutine(timeBetweenFirework));
    }
}
