using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Newtonsoft.Json;

public class DeckSample {

	[JsonProperty ("cards")]
	public List<CardItem> Cards;

	[JsonProperty ("hq")]
	public string HQ;

	[JsonProperty ("deck_id")]
	public long? ID;

	[JsonProperty ("title")]
	public string Title;

	public DeckSample () {}

	public DeckSample (long? iD, string title, string hQ, List<CardItem> cards)
	{
		this.Cards = cards;
		this.HQ = hQ;
		this.ID = iD;
		this.Title = title;
	}
	
}
