using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase;
using Firebase.Auth;
public class EmailPassword : MonoBehaviour
{
	DatabaseReference mDatabaseRef;
    public Text MA1Sensor1;

    private FirebaseAuth auth;
    public InputField Nome, Email,Senha,Idade,Estado,Cidade;
    public Button SignupButton, LoginButton;
    public Text ErrorText,Resultadot;
	public class Usuario {
		public string senha;
		public string email;
		public string nome;

		public Usuario(string senha, string email, string nome) {
			this.senha = senha;
			this.email = email;
			this.nome = nome;
		}

	}
    void Start()
    {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://vhhikingfestival.firebaseio.com/");

        auth = FirebaseAuth.DefaultInstance;

		//SignupButton.onClick.AddListener(() => Signup(Email.text, Senha.text,Nome.text));
		//LoginButton.onClick.AddListener(() => Login(Email.text, Senha.text,Nome.text));

		mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

   
    }


    void ReadQuestData(int userId){
		FirebaseDatabase.DefaultInstance
			.GetReference("Usuario")
			.GetValueAsync().ContinueWith(task=>{
				if(task.IsFaulted){
					Debug.LogError("Deu errado");
				}
				else if(task.IsCompleted){
					DataSnapshot snapshot = task.Result;
					string json = snapshot.Child(userId.ToString())
						.GetRawJsonValue();
					Debug.Log("Read: "+ json);
					Resultadot.text = json;
				}
			});
	}


	private void writeNewUser(string userId,string senha, string senhac,string email, string nome ,string dataNascimento) {
		User user = new User( senha,  senhac, email,  nome , dataNascimento);
		string json = JsonUtility.ToJson(user);

		mDatabaseRef.Child("Usuario").Child(userId).SetRawJsonValueAsync(json);
	}
	/*
	public void Sigsnup(string senha, string email, string nome)
    {
		if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha)
			|| string.IsNullOrEmpty(nome))
        {
            //Error handling
            return;
        }

		auth.CreateUserWithEmailAndPasswordAsync(email, senha).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync error: " + task.Exception);
                if (task.Exception.InnerExceptions.Count > 0)
                    UpdateErrorMessage(task.Exception.InnerExceptions[0].Message);
                return;
            }

				//       FirebaseUser newUser = task.Result; // Firebase user has been created.
            //Debug.LogFormat("Firebase user created successfully: {0} ({1})",
              //  newUser.DisplayName, newUser.UserId);
            //UpdateErrorMessage("Signup Success");

        });
		
    }*/

    private void UpdateErrorMessage(string message)
    {
        ErrorText.text = message;
        Invoke("ClearErrorMessage", 3);
    }

    void ClearErrorMessage()
    {
        ErrorText.text = "";
    }
	public void Login(string senha, string email, string nome)
    {
		auth.SignInWithEmailAndPasswordAsync(email, senha).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync error: " + task.Exception);
                if (task.Exception.InnerExceptions.Count > 0)
                    UpdateErrorMessage(task.Exception.InnerExceptions[0].Message);
                return;
            }

            FirebaseUser user = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                user.DisplayName, user.UserId);
				string userId;
				userId = user.UserId;
           // LerDados3.useridD = userId;
                //Usuario usuario = new Usuario(email, senha,nome);
				string json = JsonUtility.ToJson(user);
				mDatabaseRef.Child("Usuario").Child(userId).SetRawJsonValueAsync(json);

           // LerDados3.useridD = userId;

				PlayerPrefs.SetString("LoginUser", user != null ? user.Email : "Unknown");
				PlayerPrefs.SetString("UserID", user.UserId);

            	SceneManager.LoadScene("GPSQRCode_Scene");


        });
		
    }

    
}
