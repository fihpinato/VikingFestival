using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginHandler : MonoBehaviour {

    public Button LoginButton;

    public InputField email;
    public InputField senha;

    void Start () {
        if (LoginButton != null)
            LoginButton.onClick.AddListener(() => SimpleAddedFirebase.Instance.Login(senha.text, email.text));
		
    }

	public void BotaoCadastro(){
		SceneManager.LoadScene("TelaCadastro", LoadSceneMode.Single);
	}
}
