using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowAxeScript : MonoBehaviour {

    public float timeBetweenShots = 1f;
    public float timeToClick = 1f;
    public Transform lookAt;
    public Transform origin;
    public Transform player;
    public GameObject axePrefab;

    Button selectedButton;
    RaycastHit hit;
    float timer;
    float clickTimer;
    Ray ray;

    void Start() {

    }

    void Update() {
        if (Physics.Linecast(origin.position, lookAt.position, out hit)) {
            if (hit.collider.CompareTag("Button")) {
                clickTimer += Time.deltaTime;
                selectedButton = hit.transform.GetComponent<Button>();
                if (clickTimer >= timeToClick) {
                    selectedButton.onClick.Invoke();
                    selectedButton = null;
                }
            }
        }
        if (Physics.Linecast(origin.position, lookAt.position, out hit)) {
            timer += Time.deltaTime;
            if (hit.collider.CompareTag("AimTotem")) {
                if (timer >= timeBetweenShots && HUDMachadoScript.Instance.numMachados > 0) {
                    var axe = Instantiate(axePrefab, origin.transform.position, transform.rotation);
                    HUDMachadoScript.Instance.numMachados--;
                    Destroy(axe, 10f);
                    timer = 0;
                }
            }
        }
    }

    public void Reset() {
        GvrCardboardHelpers.Recenter();
    }
}
