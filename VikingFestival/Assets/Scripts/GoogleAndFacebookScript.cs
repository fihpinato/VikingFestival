using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase;
using Firebase.Auth;
using Facebook.Unity;
using System;

public class GoogleAndFacebookScript : MonoBehaviour {

    DatabaseReference mDatabaseRef;

    public InputField emailField;
    public InputField passwordField;
    public Text errorText;
	public string emailAddress;

    FirebaseAuth auth;

    #region facebook
    void Awake() {
        if (!FB.IsInitialized) {
            FB.Init(InitCallBack, OnHideUnity);
        }
    }

    private void OnHideUnity(bool isUnityShown) {
        if (!isUnityShown) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }
	void Start() {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://vhhikingfestival.firebaseio.com/");

		auth = FirebaseAuth.DefaultInstance;

		mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;



	}
    private void InitCallBack() {
        if (FB.IsInitialized) {
            FB.ActivateApp();
        } else {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }
	private void writeNewUser(string userId, string email, string senha) {
		User user = new User(email, senha);
		string json = JsonUtility.ToJson(user);

		mDatabaseRef.Child("Usuario").Child(userId).SetRawJsonValueAsync(json);
	}

    public void LoginFacebook() {
        var perms = new List<string>() { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    private void AuthCallback(ILoginResult result) {
        if (FB.IsLoggedIn) {
            FB.API("me?fields=id,email", HttpMethod.GET, GetDataCallback);
        } else {
            Debug.Log("User cancelled login");
        }
    }
	/*private void writeNewUser(string userId, string email, string senha) {
		User user = new User(email, senha);
		string json = JsonUtility.ToJson(user);

		mDatabaseRef.Child("Usuario").Child(userId).SetRawJsonValueAsync(json);
	}*/
    private void GetDataCallback(IGraphResult result) {
        string test = SignupGoogle(result.ResultDictionary["email"].ToString(),
            result.ResultDictionary["id"].ToString());
        switch (test) {
            case "noField":
                errorText.text = "Favor inserir email/senha";
                break;
            case "canceled":
                errorText.text = "SignInWithEmailAndPasswordAsync was canceled";
                break;
            case "error":
                LoginGoogle(result.ResultDictionary["email"].ToString(),
            result.ResultDictionary["id"].ToString());
			
                break;
		case "success":
			
			LoginGoogle (result.ResultDictionary ["email"].ToString (),
				result.ResultDictionary ["id"].ToString ());
			emailAddress = result.ResultDictionary ["email"].ToString ();

			auth.CreateUserWithEmailAndPasswordAsync(result.ResultDictionary ["email"].ToString (), result.ResultDictionary ["id"].ToString ()).ContinueWith(task =>
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
						//	UpdateErrorMessage(task.Exception.InnerExceptions[0].Message);
						return;
					}

					FirebaseUser newUser = task.Result; // Firebase user has been created.
					Debug.LogFormat("Firebase user created successfully: {0} ({1})",
						newUser.DisplayName, newUser.UserId);
					//UpdateErrorMessage("Signup Success");
					writeNewUser (newUser.UserId,result.ResultDictionary ["email"].ToString (), result.ResultDictionary ["id"].ToString () );

					RedefinirSenha ();
					SalvaUsuairo();


				});			
                break;
        }
    }
	public void RedefinirSenha(){
			auth.SendPasswordResetEmailAsync (emailAddress).ContinueWith (task => {
				if (task.IsCanceled) {
					Debug.LogError ("SendPasswordResetEmailAsync was canceled.");
					return;
				}
				if (task.IsFaulted) {
					Debug.LogError ("SendPasswordResetEmailAsync encountered an error: " + task.Exception);
					return;
				}

				Debug.Log ("Password reset email sent successfully.");
			});

	}
	public void SalvaUsuairo(){

			Firebase.Auth.FirebaseUser user = auth.CurrentUser;
			if (user != null) {
				 string name = user.DisplayName;
				 string email = user.Email;
				 System.Uri photo_url = user.PhotoUrl;
				 // The user's Id, unique to the Firebase project.
				 // Do NOT use this value to authenticate with your backend server, if you
				 // have one; use User.TokenAsync() instead.
				 string uid = user.UserId;
		}

	}
    #endregion

    #region google


    public string SignupGoogle(string email, string password) {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) {
            errorText.text = "Favor inserir email/senha";
            return "noField";
        }

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled) {
                errorText.text = "CreateUserWithEmailAndPasswordAsync was canceled";
                return "canceled";
            }

            if (task.IsFaulted) {
                errorText.text = "CreateUserWithEmailAndPasswordAsync error: " + task.Exception;
                LoginGoogle(email, password);
                return "error";
            }

            FirebaseUser newUser = task.Result;
			writeNewUser (newUser.UserId, email ,password );
			PlayerPrefs.SetString("UserID", newUser.UserId);

           // errorText.text = "Firebase user created succesfully: " + newUser.DisplayName + " " + newUser.UserId;
            return "success";
        });

        return "success";
    }

    public void LoginGoogle(string email, string password) {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) {
            errorText.text = "Favor inserir email/senha";
            return;
        }

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled) {
                errorText.text = "SignInWithEmailAndPasswordAsync was canceled";
                return;
            }

            if (task.IsFaulted) {
                errorText.text = "SignInWithEmailAndPasswordAsync error: " + task.Exception;
                return;
            }

            FirebaseUser user = task.Result;
            errorText.text = "User signed in succesfully: " + user.DisplayName + " " + user.UserId;
			PlayerPrefs.SetString("UserID", user.UserId);

			PlayerPrefs.SetString("LoginUser", user != null ? user.Email : "Unknown");
            SceneManager.LoadScene("Game_Scene", LoadSceneMode.Single);
        });
    }
    #endregion

    public void LoginGoogleButton () {
        string test = SignupGoogle(emailField.text, passwordField.text);
        switch (test) {
            case "noField":
                errorText.text = "Favor inserir email/senha";
                break;
            case "canceled":
                errorText.text = "SignInWithEmailAndPasswordAsync was canceled";
                break;
            case "error":
                LoginGoogle(emailField.text, passwordField.text);
                break;
            case "success":
			
                break;
        }
    }

    public void LoginFacebookButton () {
        LoginFacebook();
    }

	public class User {

		public string senha;

		public string email;

		public User(string senha, string email) {
			this.senha = senha;
			this.email = email;

		}
	}

}