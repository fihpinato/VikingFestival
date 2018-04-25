using System.Collections;
using UnityEngine;
using ZXing;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReadQrCode : MonoBehaviour {
	public GameObject plane;
	WebCamTexture camTexture;
	IBarcodeReader barcodeReader;
	public string gpsString;
    public static string product;
    Result result;

	void Start () {
        if (!Application.isEditor) {
            barcodeReader = new BarcodeReader ();
		    camTexture = new WebCamTexture ();
		    plane.GetComponent<Renderer> ().material.mainTexture = camTexture;
		    camTexture.Play ();
        }
    }

    void Update() {
        if (!Application.isEditor) {
            result = barcodeReader.Decode(camTexture.GetPixels32(), camTexture.width, camTexture.height);
            if (result != null) {
                product = result.Text;
                ActivateGPS();
            }
        }

        if (Input.GetButtonDown("Cancel")) {
            SceneManager.LoadScene("Games_Scene", LoadSceneMode.Single);
        }
    }

    public void ActivateGPS() {
        StartCoroutine("GetGPSPoint");
    }

    IEnumerator GetGPSPoint() {
        if (!Input.location.isEnabledByUser) {
            gpsString = "No GPS Avaible";
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1) {
            print("Timed out");
            gpsString = "Timed out";
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed) {
            print("Unable to determine device location");
            gpsString = "Unable to determine device location";
            yield break;
        } else {
			SimpleAddedFirebase.Instance.CadastrarQR();
            SceneManager.LoadScene("Games_Scene", LoadSceneMode.Single);
        }

        Input.location.Stop();
    }

    public class GpsData {
        public double body;
        public double latitude;
        public double longitude;
        public double altitude;
        public double horizontal;
        public double time;
        public string product;
    }
}
