using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrilScript : MonoBehaviour {

    public GameObject baloon;

    Rigidbody rb;
    BoxCollider box;
    MeshRenderer rend;
    public Material[] ballonColor;
    int randColor;

    void Start() {
        rb = GetComponent<Rigidbody>();
        box = GetComponent<BoxCollider>();
        rend = baloon.GetComponent<MeshRenderer>();
        randColor = Random.Range(0, ballonColor.Length);
        rend.material = ballonColor[randColor];
    }

    void OnCollisionEnter(Collision c) {
        if (c.collider.CompareTag("Axe")) {
            Destroy(baloon);
            rb.drag = 0;
            box.center = Vector3.zero;
            box.size = new Vector3(.9f, .8f, .9f);
            switch (randColor) {
                case 0:
                    HUDMachadoScript.Instance.points++;
                    break;
                case 1:
                    HUDMachadoScript.Instance.points+=2;
                    break;
                case 2:
                    break;
                case 3:
                    HUDMachadoScript.Instance.numMachados += Random.Range(1, 10);
                    if(HUDMachadoScript.Instance.numMachados > 10) {
                        HUDMachadoScript.Instance.numMachados = 10;
                    }
                    break;

            }
            HUDMachadoScript.Instance.RefreshHuds();
        }

        if (c.collider.CompareTag("Ground")) {
            Destroy(gameObject, 1f);
            HUDMachadoScript.Instance.RefreshHuds();
        }
    }
}
