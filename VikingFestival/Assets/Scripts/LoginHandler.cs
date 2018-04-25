using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginHandler : MonoBehaviour {
	public Button CadatroB;

    public Button SignupButton;
    public Button LoginButton;
    public InputField email;
	public InputField senhaConfirma;

    public InputField senha;
	public InputField dataNascimento;

	public InputField nomeInputField;


    void Start () {
        if (SignupButton != null)
			SignupButton.onClick.AddListener(() => SimpleAddedFirebase.Instance.Signup(senha.text,senhaConfirma.text, email.text, nomeInputField.text, dataNascimento.text));

        if (LoginButton != null)
            LoginButton.onClick.AddListener(() => SimpleAddedFirebase.Instance.Login(senha.text, email.text));
		
    }
	public void BotaoCadastro(){
		SceneManager.LoadScene("TelaCadastro", LoadSceneMode.Single);
	}
}
