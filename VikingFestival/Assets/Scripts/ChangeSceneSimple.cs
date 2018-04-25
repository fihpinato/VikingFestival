using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneSimple : MonoBehaviour {

    public string levelName;

    public void LoadScene () {
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }
}
