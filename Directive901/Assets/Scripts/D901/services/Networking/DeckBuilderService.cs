using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using Sun.DTO.Requests;
using Sun.DTO.Responses;
using Sun.DTO.Entities;
using Aurora.Networking.Converters;

public class DeckBuilderService {

	private static NetworkClient client = NetworkClient.getInstance();
	private static DeckBuilderService instance;


	private DeckBuilderService(){
		client = NetworkClient.getInstance ();
	}

	public static DeckBuilderService getInstance(){
		if (instance == null || client == null) {
			instance = new DeckBuilderService ();
		}		
		return instance;
	}

	public void GetDeckList()
	{
		var request = new DecksListRequestDTO ();
		request.Command = "decks/list";
		request.Token = client.getToken();

		Debug.Log(JsonConvert.SerializeObject(request));

		client.Send(JsonConvert.SerializeObject(request));
	}

	public void GetResearchList()
	{
		var request = new ResearchListRequestDTO ();
		request.Command = "research/list";
		request.Token = client.getToken();

		Debug.Log(JsonConvert.SerializeObject(request));

		client.Send(JsonConvert.SerializeObject(request));
	}

	public void SaveDeck(long? id, string title, string hq, CardItem[] cards)
	{
		var request = new DecksSaveRequestDTO ();
		request.Command = "decks/save";
		request.Token = client.getToken();
		request.Deck = new DeckDTO();
		request.Deck.ID = id; 
		request.Deck.HQ = hq;
		request.Deck.Title = title;
		request.Deck.Cards = DeckConverter.getDeckCardsFromItems (cards); 


		Debug.Log(JsonConvert.SerializeObject(request));

		client.Send(JsonConvert.SerializeObject(request));
	}

}
