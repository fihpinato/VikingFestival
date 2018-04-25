using UnityEngine;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour {

    public string sceneBack;
    public bool close = false;

    void Update () {
        if (Input.GetButtonDown("Cancel")) {
            if (close) {
                Application.Quit();
            } else {
                SceneManager.LoadScene(sceneBack, LoadSceneMode.Single);
            }
        }
    }
	
}
