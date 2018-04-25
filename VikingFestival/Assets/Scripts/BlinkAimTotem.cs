using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkAimTotem : MonoBehaviour {

	public float timeBetweenBlink = 1f;
	public GameObject[] aimList;
    public float activeCheck = 1f;

	void Start () {
        
	}

    void Update () {
        if (HUDBarcoScript.Instance.timerActive) {
            if (activeCheck < 1f) {
                if (HUDBarcoScript.Instance.timer <= 10) {
                    HUDBarcoScript.Instance.points += 3;
                } else if (HUDBarcoScript.Instance.timer <= 20) {
                    HUDBarcoScript.Instance.points += 2;
                } else {
                    HUDBarcoScript.Instance.points++;
                }
                HUDBarcoScript.Instance.timer = 0;
                HUDBarcoScript.Instance.RefreshHuds();
                BlinkFunction();
            }
        }
    }

    public void BlinkFunction() {
        activeCheck = 0;
        for (int i = 0; i < aimList.Length; i++) {
            int rand = Random.Range(1, 10);
            if (rand > 5) {
                aimList[i].SetActive(true);
                activeCheck++;
            } else {
                aimList[i].SetActive(false);
            }
        }
    }
}
