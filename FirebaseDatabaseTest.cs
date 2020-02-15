using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using Proyecto26;
using UnityEngine.UI;

[Serializable]
public class User
{
	public string Name = string.Empty;
	public int Score = 0;

	public User(string _name, int _score)
	{
		this.Name = _name;
		this.Score = _score;

	}
}

public class FirebaseDatabaseTest : MonoBehaviour {

	public const string myDBAddress = "https://clickerx.firebaseio.com/";
	public const string jsonExtention = ".json";

	public InputField inputFieldName;
	public InputField inputFieldScore;
	public Text labelScore;
	public Text labelName;

	private User user = null;

	private void PostToDatabase()
	{

		// 
		RestClient.Put(myDBAddress + inputFieldName.text + jsonExtention, this.user);
	}


	private void ReceivedFromDatabase()
	{
		RestClient.Get<User>(myDBAddress + inputFieldName.text + jsonExtention).Then(response =>
		  {
			  this.user = response;
		  });
	}

	public void OnCommit()
	{
		this.user = new User(inputFieldName.text, int.Parse(inputFieldScore.text));
		PostToDatabase();

	}

	public void OnReceive()
	{
		ReceivedFromDatabase();
		if(this.user != null)
		{
			labelName.text = this.user.Name;
			labelScore.text = this.user.Score.ToString();
		}
	}
}
