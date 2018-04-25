using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase;
using Firebase.Auth;

public class NovosDados : MonoBehaviour {
	public InputField longitudeF, latitudeF,bebidaF;
	public Button cadastro;

	//private FirebaseAuth auth;
	DatabaseReference mDatabaseRef;
	void Start()
	{
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://vhhikingfestival.firebaseio.com/");

		//auth = FirebaseAuth.DefaultInstance;

		cadastro.onClick.AddListener(() => writeNewUser(longitudeF.text, latitudeF.text,bebidaF.text));



		mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

	}




	public void writeNewUser( string latitude, string longitude, string bebida) {
		//DadosAr dadoAR = new DadosAr (latitude, longitude, bebida);
		//string json = JsonUtility.ToJson (dadoAR);
		string key = mDatabaseRef.Child("DadosAr").Push().Key;


	}

	public void Cadastro( string latitude, string longitude, string bebida) {
		writeNewUser (longitudeF.text, latitudeF.text,bebidaF.text);

	}
}
public class DadosAr {
	public string longitude;
	public string latitude;
	public string bebida;



	public DadosAr(string longitude, string latitude,string bebida) {
		this.longitude = longitude;
		this.latitude = latitude;
		this.bebida = bebida;
	}
}
