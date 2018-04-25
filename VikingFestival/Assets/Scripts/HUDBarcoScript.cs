using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase.Auth;
public class HUDBarcoScript : MonoBehaviour {

    #region SINGLETON
    public static HUDBarcoScript _instance;
    public static HUDBarcoScript Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<HUDBarcoScript>();
                if (_instance == null) {
                    GameObject container = new GameObject("HUDScript");
                    _instance = container.AddComponent<HUDBarcoScript>();
                }
            }
            return _instance;
        }
    }
    #endregion

    public int points = 0;
	public float timer;
    public float endOfTimer = 30f;
    public bool timerActive = true;
    public Text finalPointsText;
    public TextMesh pointsText;
    public TextMesh timerText;
    public GameObject panel;
    public Transform player;
	float rotationSpeed = 20f;

	void Update () {
        if(Vector3.Distance(panel.transform.position, player.position) <= 7.8f) {
            rotationSpeed = 0f;
        } else {
            rotationSpeed = 20f;
        }
        panel.transform.RotateAround(transform.position, Vector3.up, -rotationSpeed * Time.deltaTime);

        if(timerActive)
            timer += Time.deltaTime;
        if(timer <= 9f) {
            timerText.text = "00:0" + Mathf.Round(timer);
        } else {
            timerText.text = "00:" + Mathf.Round(timer);
        }

        if (timer >= endOfTimer) {
            timerActive = false;
            finalPointsText.text = "Você fez " + points + " pontos";
            //Passar o numero de gols pra pontuação aqui
            panel.SetActive(true);
        }

        pointsText.transform.LookAt(-player.position);
        timerText.transform.LookAt(-player.position);
	} 
    public void RefreshHuds () {
        pointsText.text = "Pontos: " + points;
    }

    public void Reset () {
        if (!Application.isEditor) {
            SimpleAddedFirebase.Instance.Read();
            SimpleAddedFirebase.Instance.CadastrarPontos(points);
        }
        points = 0;
        timer = 0;
        RefreshHuds();
        timerActive = true;
    }

    public void Exit() {
        if (!Application.isEditor) {
            SimpleAddedFirebase.Instance.Read();
            SimpleAddedFirebase.Instance.CadastrarPontos(points);
        }
        SceneManager.LoadScene("Games_Scene", LoadSceneMode.Single);
    }
}

