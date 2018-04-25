using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class LendoDados: MonoBehaviour {

    public Text MA1Sensor1;

    void InitializeFirebase(string userId) {


        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://vhhikingfestival.firebaseio.com/");

        FirebaseDatabase.DefaultInstance
            .GetReference("Usuario").OrderByChild(userId)
            .ValueChanged += (object sender2, ValueChangedEventArgs e2) => {
                if (e2.DatabaseError != null) {
                    Debug.LogError(e2.DatabaseError.Message);
                }


                if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0) {

                    foreach (var childSnapshot in e2.Snapshot.Children) {
                        var name = childSnapshot.Child("name").Value.ToString();

                        MA1Sensor1.text = name.ToString();
                        Debug.Log(name.ToString());
                    //text.text = childSnapshot.ToString();
                    }
                }
            };
    }

    // Update is called once per frame
    void Upde() {

    }
}