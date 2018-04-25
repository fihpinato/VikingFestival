using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Analytics;
using Firebase.Unity.Editor;

public class LerDados3: MonoBehaviour {
	public string jsonData;

	public Text MA1Sensor2;
	public Text MA1Sensor1;

	DatabaseReference myReference;
	Dictionary<string, object> dataMap;

	void Start() 
	{
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://vhhikingfestival.firebaseio.com/");
		myReference = FirebaseDatabase.DefaultInstance.RootReference;
	}
	public void botaoTestes(){
		Firebase.Analytics.FirebaseAnalytics.LogEvent("Refrigerante_Guarana", "Guarana 600 ML", 0.6f);



	}
	public void botaoTestes1(){
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Refrigerante_Guarana", "Guarana  2 L", 2.0f);



    }
    public void botaoTestes2()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Refrigerante_Pepsi", "Pepsi 600 ML", 0.6f);


    }
    public void botaoTestes4()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Refrigerante_Pepsi", "Pepsi  2 L", 2.0f);



    }

}