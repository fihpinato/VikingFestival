using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDGamesScript : MonoBehaviour {

    public Text perfilText;
    public Text pointsText;

    void Start () {
        SimpleAddedFirebase.Instance.Read(pointsText);
        SimpleAddedFirebase.Instance.GetFirebaseName(perfilText);
	}
	
	void Update () {
		
	}

    public void LogOut () {
        SceneManager.LoadScene("Settings", LoadSceneMode.Single);
    }
}
