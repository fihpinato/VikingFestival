using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateAccHandler : MonoBehaviour {

    public Button SignupButton;
    public InputField email;
    public InputField senhaConfirma;

    public InputField senha;
    public InputField dataNascimento;

    public InputField nomeInputField;

    void Start () {
        if (SignupButton != null)
            SignupButton.onClick.AddListener(() => SimpleAddedFirebase.Instance.Signup(senha.text, senhaConfirma.text, email.text, nomeInputField.text, dataNascimento.text));
    }
}
