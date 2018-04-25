using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase;
using Firebase.Analytics;
using Firebase.Auth;
using Facebook.Unity;

public class SimpleAddedFirebase : MonoBehaviour {

    #region SINGLETON
    public static SimpleAddedFirebase _instance;
    public static SimpleAddedFirebase Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<SimpleAddedFirebase>();
                if (_instance == null) {
                     GameObject container = new GameObject("SimpleAddedFirebase");
                    _instance = container.AddComponent<SimpleAddedFirebase>();
                }
            }
            return _instance;
        }
    }
    #endregion

    public string SceneToLoad;
    public string SceneToLogout;

    public Text errorText = null;
    public Slider slider = null;
    public GameObject panel;

    public AudioSource audioSource;

    public string emailAddress;
    public string nome;
    public string jsonData;
    public int gols;
    public int pontosA;
	public Text SenhaConfirmada;
	public string SenhaConf;
    string userIDFacebook;
    string IdUsuario;
    DatabaseReference mDatabaseRef;
    DatabaseReference _counterRef;
    FirebaseAuth auth;

    void Awake () {
        DontDestroyOnLoad(gameObject);
        if (!FB.IsInitialized) {
            FB.Init(InitCallBack, OnHideUnity);
        }
    }

    void Start () {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://vhhikingfestival.firebaseio.com/");

		auth = FirebaseAuth.DefaultInstance;

		mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;

        if (PlayerPrefs.HasKey("email") && PlayerPrefs.HasKey("senha")) {
            Login(PlayerPrefs.GetString("senha"), PlayerPrefs.GetString("email"), slider);
        } else if (panel != null && audioSource != null) {
            panel.SetActive(false);
            audioSource.Play();
        }

        IdUsuario = PlayerPrefs.GetString("UserID");
    }

    #region Google
	public void Signup(string senha, string senhac,string email, string nome ,string dataNascimento ,Slider slider = null)
	{
		
		if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha)  )
			{
			errorText.text = "Campo não esta completo";

			return;
		}

		auth.CreateUserWithEmailAndPasswordAsync(email, senha).ContinueWith(task => {
            if (task.IsCanceled) {
                errorText.text = "CreateUserWithEmailAndPasswordAsync was canceled.";
                return;
            }
            if (task.IsFaulted) {
                errorText.text = "CreateUserWithEmailAndPasswordAsync error: " + task.Exception;
                if (task.Exception.InnerExceptions.Count > 0)
                    Debug.LogWarning(task.Exception.InnerExceptions[0].Message);
                return;
            }
			if(senha == senhac){
		            if (task.IsCompleted) { 
		                FirebaseUser newUser = task.Result; // Firebase user has been created.
		                Debug.LogFormat("Firebase user created successfully: {0} ({1})",
		                    newUser.DisplayName, newUser.UserId);
		                Debug.Log("Signup Success");
				User user = new User( senha,  senhac, email,  nome , dataNascimento);
		                string json = JsonUtility.ToJson(user);

				PlayerPrefs.SetString("nome", nome);
				PlayerPrefs.SetString("email", email);
				PlayerPrefs.SetString("senha", senha);
				PlayerPrefs.SetString("Data Nascimento", dataNascimento);
				PlayerPrefs.SetString("UserID", newUser.UserId);

		                mDatabaseRef.Child("Usuario").Child(newUser.UserId).SetRawJsonValueAsync(json);
						
						Read();

						ReadNome();


		                SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Single);
						}
				else if(senha != senhac){
					errorText.text = " senha errada";

					}
			}
		
		});
	}

	public void Login(string senha, string email, Slider slider = null)
	{
		auth.SignInWithEmailAndPasswordAsync(email, senha).ContinueWith(task =>
			{
				if (task.IsCanceled)
				{
                    errorText.text = "SignInWithEmailAndPasswordAsync canceled.";
					return;
				}
				if (task.IsFaulted)
				{
                    errorText.text = "SignInWithEmailAndPasswordAsync error: " + task.Exception;
					if (task.Exception.InnerExceptions.Count > 0)
						Debug.LogWarning(task.Exception.InnerExceptions[0].Message);
					return;
				}

                if (task.IsCompleted) {
                    FirebaseUser user = task.Result;

                    errorText.text = "User signed in successfully: " +
                        user.DisplayName + user.UserId;

                    GetFirebaseName(errorText);

                    PlayerPrefs.SetString("UserID", user.UserId);
                    IdUsuario = user.UserId;
                    PlayerPrefs.SetString("LoginUser", user != null ? user.Email : "Unknown");
                    PlayerPrefs.SetString("nome", nome);
                    PlayerPrefs.SetString("email", email);
                    PlayerPrefs.SetString("senha", senha);
                    Read();
					ReadNome();

                    //SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Single);
                    StartCoroutine(AsynchronousLoad(SceneToLoad, slider));
                }
			});

	}

    public void CadastrarPontos(int pontos) {
        _counterRef = FirebaseDatabase.DefaultInstance.GetReference("Usuario" + "/" + IdUsuario + "/" + "pontos");
        _counterRef.RunTransaction(data => {
            data.Value = pontosA + pontos;
            return TransactionResult.Success(data);
        }).ContinueWith(task => {
            if (task.Exception != null)
                Debug.Log(task.Exception.ToString());
        });
    }

	public void CadastrarQR(){
		string key = mDatabaseRef.Child("Usuario").Child(IdUsuario).Child("Lista").Push().Key;
		ReadQrCode.GpsData tData = new ReadQrCode.GpsData();
		tData.latitude = Input.location.lastData.latitude;
		tData.longitude = Input.location.lastData.longitude;
		tData.altitude = Input.location.lastData.altitude;
		tData.horizontal = Input.location.lastData.horizontalAccuracy;
		tData.time = Input.location.lastData.timestamp;
        tData.product = ReadQrCode.product;

		string json = JsonUtility.ToJson(tData);
		//mDatabaseRef.Child("Usuario").Child(IdUsuario).Child("Lista").Child(key).SetRawJsonValueAsync(json);

		if(ReadQrCode.product == "Guaraná"){
            string tipo = "guarana A";
            int lt = 1;
            string litros = "600 ML";


            mDatabaseRef.Child("Usuario").Child(IdUsuario).Child("Lista").Child(key).SetRawJsonValueAsync(json);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("Refrigerante_Guarana", new[] 
            {
                new Parameter ("Tipo", lt),new Parameter ("Quantidade", lt),new Parameter ("Litros", litros)
            });

		
		}
		else if(ReadQrCode.product == "Guaraná2L"){
            string tipo = "guarana B";
            int lt = 1;
            string litros = "2 LT";


            mDatabaseRef.Child("Usuario").Child(IdUsuario).Child("Lista").Child(key).SetRawJsonValueAsync(json);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("Refrigerante_Guarana", new[]
        {
                new Parameter ("Tipo", tipo), new Parameter ("Quantidade", lt),new Parameter ("Litros", litros)});
        }

        else if(ReadQrCode.product == "Pepsi600ML"){
            string tipo = "Pepsi A";
            int lt = 1;
            string litros = "600 ML";


            mDatabaseRef.Child("Usuario").Child(IdUsuario).Child("Lista").Child(key).SetRawJsonValueAsync(json);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("Refrigerante_Pepsi", new[]
        {
                new Parameter ("Tipo", tipo), new Parameter ("Quantidade", lt),new Parameter ("Litros", litros)});
        }

		else if(ReadQrCode.product == "Pepsi2L"){
            string tipo = "Pepsi B";
            int lt = 1;
            string litros = "2 LT";

            mDatabaseRef.Child("Usuario").Child(IdUsuario).Child("Lista").Child(key).SetRawJsonValueAsync(json);

            Firebase.Analytics.FirebaseAnalytics.LogEvent("Refrigerante_Pepsi", new[]
        {
                new Parameter ("Tipo", tipo), new Parameter ("Quantidade", lt),new Parameter ("Litros", litros)});
        }
        else if (ReadQrCode.product == "")
        {

            

        }
        ReadQrCode.product = "";
	}
    public void Read(Text pointsText = null) {
        FirebaseDatabase.DefaultInstance
            .GetReference("Usuario").Child(IdUsuario).Child("pontos").GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted) {
                    Debug.Log("Read Error!");
                } else if (task.IsCompleted) {
                    DataSnapshot snapshot = task.Result;
                    jsonData = snapshot.GetRawJsonValue();
                    pontosA = int.Parse(jsonData);
                    pointsText.text = jsonData;
                }
            });
    }
	public void ReadNome(Text PerfilText = null) {
		FirebaseDatabase.DefaultInstance
			.GetReference("Usuario").Child(IdUsuario).Child("nome").GetValueAsync().ContinueWith(task => {
				if (task.IsFaulted) {
					Debug.Log("Read Error!");
				} else if (task.IsCompleted) {
					DataSnapshot snapshot = task.Result;
					jsonData = snapshot.GetRawJsonValue();

					PerfilText.text = jsonData;
				}
			});
	}

    public void RedefinirSenha() {
        auth.SendPasswordResetEmailAsync(emailAddress).ContinueWith(task => {
            if (task.IsCanceled) {
                Debug.LogError("SendPasswordResetEmailAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("SendPasswordResetEmailAsync encountered an error: " + task.Exception);
                return;
            }

            Debug.Log("Password reset email sent successfully.");
        });
    }

    public void GetFirebaseName(Text nameText) {
            FirebaseDatabase.DefaultInstance
                .GetReference("Usuario").Child(IdUsuario).Child("nome").GetValueAsync().ContinueWith(task => {
                    if (task.IsFaulted) {
                        Debug.Log("Read Error!");
                    } else if (task.IsCompleted) {
                        DataSnapshot snapshot = task.Result;
                        jsonData = snapshot.GetRawJsonValue();
                        nameText.text = jsonData;
                        PlayerPrefs.SetString("nome", jsonData);
                    }
                });

        if (nameText != null) {
            nameText.text = PlayerPrefs.GetString("nome");
        }
    }
    #endregion

    #region Facebook
    private void OnHideUnity(bool isUnityShown) {
        if (!isUnityShown) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }

    private void InitCallBack() {
        if (FB.IsInitialized) {
            FB.ActivateApp();
        } else {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    public void LoginFacebook() {
        var perms = new List<string>() { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    private void AuthCallback(ILoginResult result) {
        if (FB.IsLoggedIn) {
            FB.API("me?fields=id,email,name", HttpMethod.GET, GetDataCallback);
        } else {
            Debug.Log("User cancelled login");
        }
    }

    private void GetDataCallback(IGraphResult result) {
		Signup(result.ResultDictionary["id"].ToString(),result.ResultDictionary["id"].ToString(), result.ResultDictionary["email"].ToString(), result.ResultDictionary["name"].ToString(), result.ResultDictionary["birthday"].ToString());
        Login(result.ResultDictionary["id"].ToString(), result.ResultDictionary["email"].ToString());
    }
    #endregion

    public void LogOut() {
        FB.LogOut();
        auth.SignOut();
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneToLogout, LoadSceneMode.Single);
    }

    public IEnumerator AsynchronousLoad(string sceneName, Slider slider = null) {

        yield return null;

        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);

        while (!ao.isDone) {
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            if (slider != null) {
                slider.value = progress;
            }
            Debug.Log("Loading progerss: " + (progress * 100) + "%");
            yield return null;
        }
    }
}

public class User {
    public string senha;
	public string senhac;

    public string email;
	public string nome;

	public string dataNascimento;
	public User(string senha, string senhac,string email, string nome ,string dataNascimento) {
        this.senha = senha;
		this.senhac = senhac;
        this.email = email;
        this.nome = nome;
		this.dataNascimento = dataNascimento;
    }


}
