using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class LerDado2: MonoBehaviour {
	public Text referencia01;

	void Start() {
		// Set up the Editor before calling into the realtime database.
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://vhhikingfestival.firebaseio.com/");

		/// Get the root reference location of the database.
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

		reference.ValueChanged += HandleValueChanged;
	}

	void HandleValueChanged(object sender, ValueChangedEventArgs args) {

		if (args.DatabaseError != null) {
			Debug.LogError(args.DatabaseError.Message);
			return;
		}
		// DO SOMETHING WITH THE DATA IN ARGS.SNAPSHOT
		var getTask = FirebaseDatabase.DefaultInstance.GetReference ("Usuario").Child ("nome").GetValueAsync ();

		StartCoroutine (ReadyData ());

        if (getTask.IsCompleted) {
            Debug.Log(getTask.Result.Value.ToString());
        } else {
            Debug.Log("If Error");
        }
	}

	IEnumerator ReadyData()  { 
		var getTask = FirebaseDatabase.DefaultInstance.GetReference ("Usuario").Child ("nome").GetValueAsync ();
		yield return new WaitUntil (() => getTask.IsCompleted);
	}

	// Update is called once per frame

	void Update () 
	{

	}
}  
