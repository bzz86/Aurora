using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Newtonsoft.Json;

public class Deck {

	[JsonProperty ("cards")]
	public List<CardItem> Cards;

	[JsonProperty ("hq")]
	public CardItem HQ;

	[JsonProperty ("deck_id")]
	public long? ID;

	[JsonProperty ("title")]
	public string Title;

	public Deck () {}
}
