using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game2 : MonoBehaviour {
    
	public InputField Nome;
    public InputField Senha;
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject[] targets;
    //private bool isPaused = false;
    public string[] lines;
    private string decryptedPass;

    string email ;
    string senha ;

    [System.Serializable]
    public class Save
    {
       public string Nome;
       public string Senha; 
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        save.Nome = Nome.text;
        save.Senha = Senha.text;

        return save;
    }

    public void SaveGame()
    {

        // 1
        Save save = CreateSaveGameObject();

        // 2
        string email = Nome.text;
        string senha = Senha.text;
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/" + email +  ".json" )){   
            print("já existe esse usuario");
            Debug.Log(Application.persistentDataPath);

        } else {

            FileStream file = File.Create(Application.persistentDataPath + "/" + email + ".json");
            bf.Serialize(file, save);
            file.Close();
            
            Debug.Log(Nome.text);

            string json = JsonUtility.ToJson(save);

            save = JsonUtility.FromJson<Save>(json);
            Debug.Log(Senha.text);
            Debug.Log("Saving as JSON: " + json);
            Debug.Log(Application.persistentDataPath);

            Debug.Log("Game Saved");
        }
    }
 
    public void LoadGame () {
            string email = Nome.text;
            string senha = Senha.text;
            if (File.Exists(Application.persistentDataPath + "/" + email + ".json")) {

                //BinaryFormatter bf = new BinaryFormatter();
                //FileStream file = File.Open(Application.persistentDataPath + "/" + email+ ".txt", FileMode.Open);
                lines = File.ReadAllLines(Application.persistentDataPath + "/" + email + ".json");
         //    Save save = (Save)bf.Deserialize(lines);
         //   lines.Close();


            Debug.Log("Game Loaded");
			SceneManager.LoadScene ("Menu_Scene", LoadSceneMode.Single);

            }

        else
        {
            Debug.Log("erro no login");

        }

    }
    public void SaveAsJSON()
    {

        Save save = CreateSaveGameObject();

        string json = JsonUtility.ToJson(save);
        save = JsonUtility.FromJson<Save>(json);

        Debug.Log("Saving as JSON: " + json);

    }
}
