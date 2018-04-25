using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingFirstScene : MonoBehaviour {

    public Slider slider;

	void Start () {
        StartCoroutine(Load());
    }

    IEnumerator Load () {
        yield return new WaitForSeconds(.2f);
        print("teste");
        if (PlayerPrefs.HasKey("email") && PlayerPrefs.HasKey("senha")) {
            SimpleAddedFirebase.Instance.Login(PlayerPrefs.GetString("senha"), PlayerPrefs.GetString("email"), slider);
        } else {
            StartCoroutine(SimpleAddedFirebase.Instance.AsynchronousLoad("EmailPassword", slider));
        }
    }
}
