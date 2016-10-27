using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using Newtonsoft.Json;

[Serializable]
public class PlayerData {
		static string version="0.0.1";

		
	[JsonProperty]public string name="";
	[JsonProperty]public string passwordHash="";
	[JsonProperty]public string email="";
	[JsonProperty]public bool remember=false;
	[JsonProperty]public  Guid token;
	[JsonProperty]public List<Deck> deckList;
	[JsonProperty]public List<CardItem> hqList;
	[JsonProperty]public List<CardItem> cardList;




	public static PlayerData Saved
	{
		get {
			Debug.LogWarning("PlayerData.Saved>Get()");
			return Get();
		}
	}

	static PlayerData instance=null;

	static PlayerData Get()
	{
	Debug.LogWarning(">Get()");
		if (instance==null)
		{
			Debug.LogWarning("instance==null");
			string json=PlayerPrefs.GetString(version,null);
			
			Debug.LogWarning(json);

			if (!string.IsNullOrEmpty(json))
			{
				instance = JsonConvert.DeserializeObject<PlayerData>(json);
			}
			
			Debug.LogWarning(instance);
			
			if (instance==null)
			{
				Reset();
				instance = new PlayerData();
			}
		}

		return instance;
	}

	public static void Reset()
	{
	Debug.LogWarning(">Reset()");
		PlayerPrefs.DeleteKey(version);
		PlayerPrefs.Save();
	}

	public static void Save()
	{
	Debug.LogWarning(">Save()");
		string json=JsonConvert.SerializeObject(instance);

		Debug.LogWarning(json);

		PlayerPrefs.SetString(version,json);
		PlayerPrefs.Save();
	}

}

