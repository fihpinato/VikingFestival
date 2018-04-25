using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazeAndTeleport : MonoBehaviour {

	public float timeBetweenShots = 1f;
	public float transportTime = 1f;
    public float turnHeadVelocity = 1f;
    public float timeToClick = 1f;
    public Transform lookAt;
	public Transform origin;
	public Transform player;
    public Transform boat;
    public Transform monster;
	public GameObject axePrefab;

    Button selectedButton;
    Transform nextPoint;
    Transform powerUp;
    Vector3 firstPos;
    RaycastHit hit;
    float timer;
    float clickTimer;
    Ray ray;
    bool isWalking;

    void Start() {
        firstPos = player.position;
    }

    void Update () {
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

        if (HUDBarcoScript.Instance.timerActive) {
            if (Physics.Linecast(origin.position, lookAt.position, out hit)) {
                boat.LookAt(monster.position, Vector3.up);
                timer += Time.deltaTime;
                if (hit.collider.CompareTag("Respawn") && !isWalking) {
                    nextPoint = hit.transform;
                    isWalking = true;
                }
                if (hit.collider.CompareTag("AimTotem")) {
                    if (timer >= timeBetweenShots) {
                        var axe = Instantiate(axePrefab, origin.transform.position, transform.rotation);
                        Destroy(axe, 3f);
                        timer = 0;
                    }
                }
                if (hit.collider.CompareTag("PowerUp")) {
                    powerUp = hit.transform;
                }
            }

            if (nextPoint != null && isWalking) {
                player.position = Vector3.MoveTowards(player.position, nextPoint.position, transportTime);
                player.rotation = Quaternion.Slerp(player.rotation, Quaternion.LookRotation(-nextPoint.position - player.position), turnHeadVelocity * Time.deltaTime);
                Physics.IgnoreCollision(player.GetComponent<Collider>(), nextPoint.GetComponent<Collider>());
                if (Vector3.Distance(player.transform.position, nextPoint.position) < 0.1f) {
                    nextPoint = null;
                    isWalking = false;
                }
            }

            if (powerUp != null) {
                powerUp.position = Vector3.MoveTowards(powerUp.position, player.position, transportTime);
                if (Vector3.Distance(powerUp.position, player.position) < 0.2f) {
                    powerUp = null;
                }
            }
        }
    }

    public void Reset () {
        player.position = firstPos;
        GvrCardboardHelpers.Recenter();
    }
}
