using UnityEngine;
using System.Collections;

using Newtonsoft.Json;

public class Item
{
	[JsonProperty ("proto")]
	public string ProtoID;

	[JsonProperty ("count")]
	public int Count;

	public Item (){}

	public Item (string protoId, int count){
		ProtoID = protoId;
		Count = count;
	}
}
