using UnityEngine;
using System.Collections;

public class TestSave : D901BaseObject {

//	string id="data";
//
//	// Use this for initialization
//	void Start () {
//		Debug.Log(PlayerPrefs.GetString(id,null));
//		PlayerPrefs.SetString(id,"newValue");
//		Debug.Log(PlayerPrefs.GetString(id,null));
//		PlayerPrefs.Save();
//	}


	void Start()
	{
		Debug.Log(PlayerData.Saved.name);
		PlayerData.Saved.name="newName";
		Debug.Log(PlayerData.Saved.name);
		PlayerData.Save();
	}

}
