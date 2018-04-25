using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDMachadoScript : MonoBehaviour {

    #region SINGLETON
    public static HUDMachadoScript _instance;
    public static HUDMachadoScript Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<HUDMachadoScript>();
                if (_instance == null) {
                    GameObject container = new GameObject("HUDMachadoScript");
                    _instance = container.AddComponent<HUDMachadoScript>();
                }
            }
            return _instance;
        }
    }
    #endregion

    public int points = 0;
    public int numMachados = 10;
    public float timer;
    public bool timerActive = false;
    public Text finalPointsText;
    public TextMesh pointsText;
    public TextMesh machadosText;
    public GameObject panel;

    void Update() {
        
        
    }

    public void RefreshHuds() {
        pointsText.text = "Pontos: " + points;
        machadosText.text = "Machados: " + numMachados;
        if (numMachados < 1) {
            timerActive = false;
            panel.SetActive(true);
            GameObject[] machados = GameObject.FindGameObjectsWithTag("AimTotem");
            foreach(GameObject machado in machados){
                Destroy(machado);
            }
        }
    }

    public void Reset() {
        if (!Application.isEditor) {
            SimpleAddedFirebase.Instance.Read();
            SimpleAddedFirebase.Instance.CadastrarPontos(points);
        }
        timerActive = true;
        points = 0;
        numMachados = 10;
        RefreshHuds();
    }

    public void Exit() {
        if (!Application.isEditor) {
            SimpleAddedFirebase.Instance.Read();
            SimpleAddedFirebase.Instance.CadastrarPontos(points);
        }
        SceneManager.LoadScene("Games_Scene", LoadSceneMode.Single);
    }
}
