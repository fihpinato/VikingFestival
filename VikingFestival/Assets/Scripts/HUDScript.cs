using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase;
using Firebase.Auth;

public class HUDScript : MonoBehaviour {

    #region SINGLETON
    public static HUDScript _instance;
    public static HUDScript Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<HUDScript>();
                if (_instance == null) {
                    GameObject container = new GameObject("HUDScript");
                    _instance = container.AddComponent<HUDScript>();
                }
            }
            return _instance;
        }
    }
    #endregion

    public Text golsText;
    public Text vidasText;
    public Text reinicioText;
    public Text textText;
    public Button chutarButton;
    public GameObject reinicio;
    public int gols;
    public int vidas = 3;
	public int TotalGol;
    public Slider slider;
    public float sliderValor;
    public float velocidadeSlider = 1f;
    public bool sliderAtivo;
    public bool chutarAtivo;
    LoadingOverlay loading;

    void Awake () {
        //loading = FindObjectOfType<LoadingOverlay>();
    }

    void Start () {
        //loading.FadeOut();
		SimpleAddedFirebase.Instance.Read ();
    }

    void Update () {
        if (sliderAtivo) {
            sliderValor = Mathf.Sin(Time.time * velocidadeSlider);
            slider.value = sliderValor;
        }
        chutarButton.interactable = chutarAtivo;
    }

	public void AtualizarHud () {
		golsText.text = "Gols: " + gols;
        vidasText.text = "Vidas: " + vidas;
    }

    public void Reiniciar () {
        SimpleAddedFirebase.Instance.Read();
        gols = 0;
        vidas = 3;
        AtualizarHud();
    }

    public void AtivarReinicio () {
        reinicioText.text = gols + " gols";
        SimpleAddedFirebase.Instance.CadastrarPontos (gols);
        reinicio.SetActive(true);
    }

    public void Voltar () {
        StartCoroutine(FadeAndLoad());
    }

    IEnumerator FadeAndLoad() {
        //loading.FadeIn();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Games_Scene", LoadSceneMode.Single);
    }
}
