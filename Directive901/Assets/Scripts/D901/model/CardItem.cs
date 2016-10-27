using UnityEngine;
using System.Collections;

using Newtonsoft.Json;

public class CardItem
{
	[JsonProperty ("proto")]
	public string ProtoID;

	[JsonProperty ("count")]
	public int Count;

	public CardItem (){}

	public CardItem (string protoId, int count){
		ProtoID = protoId;
		Count = count;
	}
}
