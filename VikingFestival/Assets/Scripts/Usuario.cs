using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

using Firebase.Database;

using Firebase;
using Firebase.Unity.Editor;
public class Usuario : MonoBehaviour {

		public string senha;
		public string nome;

		public string email;

		public Usuario(string senha, string email, string nome) {
			this.senha = senha;
			this.email = email;
			this.nome = nome;

	}

	private void writeNewUser(string userId, string email, string senha, string nome) {
		//User user = new User(email, senha, nome);
		//string json = JsonUtility.ToJson(user);

	}


}
