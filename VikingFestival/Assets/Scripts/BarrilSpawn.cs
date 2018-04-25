using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrilSpawn : MonoBehaviour {

    public float timeBetweenBarril;
    public Vector3 clamp;
    public GameObject barrilPrefab;
    

    Vector3 randPos;
    float timer;

    void Start () {
        
    }

    void Update () {
        if (HUDMachadoScript.Instance.timerActive) {
            timer += Time.deltaTime;
            if (timer >= timeBetweenBarril) {

                randPos = new Vector3(
                Random.Range(-clamp.x, clamp.x),
                clamp.y,
                Random.Range(-clamp.z, clamp.z));
                var barril = Instantiate(barrilPrefab, randPos, transform.rotation);
                Destroy(barril, 25f);
                timer = 0;
            }
        }
    }
}
