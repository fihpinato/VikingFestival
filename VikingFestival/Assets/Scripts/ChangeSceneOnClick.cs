using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnClick : MonoBehaviour {

	public string levelName;
    LoadingOverlay loading;

    void Start () {
        loading = FindObjectOfType<LoadingOverlay>();
        loading.FadeOut();
    }

	void OnMouseDown () {
        StartCoroutine(FadeAndLoad());
	}

    IEnumerator FadeAndLoad () {
        loading.FadeIn();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }
}
