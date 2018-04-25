using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class LerDados4 : MonoBehaviour
{
	private DatabaseReference reference;

	void Start()
    {
	    // ???????????????? Firebase Project
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://vhhikingfestival.firebaseio.com/");

	    // ????????????????????? Firebase
	    reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
	public class ToniData {
		public double body;
		public string uid;
	}

    public void WriteToniData()
    {
	string key = reference.Child("Usuario").Push().Key;
	//Dictionary<string, Object> childUpdates = new Dictionary<string, Object>();
	ToniData tData = new ToniData();
	tData.body = Random.Range(0f, 5f);
	tData.uid = "Tony";
	string json = JsonUtility.ToJson(tData);
		reference.Child("Usuario").Child(key).SetRawJsonValueAsync(json);

    }

    public void RaadAllData()
    {
		FirebaseDatabase.DefaultInstance.GetReference("Usuario")
		.ValueChanged += HandleValueChanged;
    }

    private void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
	    if (args.DatabaseError != null)
	    {
		    Debug.LogError(args.DatabaseError.Message);
		    return;
	    }
	    // ???? Key ??????????????
	    List<string> keys = args.Snapshot.Children.Select(s => s.Key).ToList();
	    foreach (var key in keys)
	    {
		    DisplayData(args.Snapshot, key);
	    }
    }
    // ????????? ?????????????????????
    void DisplayData(DataSnapshot snapshot, string key)
    {
	    string j = snapshot.Child(key).GetRawJsonValue();
	    ToniData u = JsonUtility.FromJson<ToniData>(j);
	    Debug.Log(u.uid + " " + u.body);
    }
}