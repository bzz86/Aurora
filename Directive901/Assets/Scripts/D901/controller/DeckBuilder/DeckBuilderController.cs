using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sun.CardProtos;
using UnityEngine.UI;
using Sun.DTO.Entities;
using Sun.DTO.Responses;
using Newtonsoft.Json;
using Aurora.Networking.Converters;

public class DeckBuilderController : D901Controller {

	private CardProtosRepository protoRepository = new CardProtosRepository();

	public override void OnServerNotification (string commandName, string data)
	{
		Debug.Log ("DeckBuilderController caught OnServerNotification: " + commandName);

		switch (commandName) {
			case ServerNotification.DECKS_LIST:
				Debug.Log ("DeckBuilderController caught DECK_LIST");
				DecksListResponseDTO deckListResponse = JsonConvert.DeserializeObject<DecksListResponseDTO>(data);
				if(deckListResponse.Success){
					
					TaskExecutorScript.getInstance().ScheduleTask(new Task(delegate
						{
							List<DeckDTO> decks = new List<DeckDTO>(deckListResponse.Payload.Decks);
							Debug.Log ("decks number before save = " + decks.Count);
							PlayerData.Saved.deckList = decks;
							PlayerData.Save();
							app.view.deckBuilder.reloadDecks();
							/*foreach(DeckDTO deck in deckListResponse.Payload.Decks){
								app.view.deckBuilder.addDeckToList(deck);	
							}*/
						}
					));	
				}
				break;

			case ServerNotification.RESEARCH_LIST:
				Debug.Log ("DeckBuilderController caught RESEARCH_LIST");
				ResearchListResponseDTO researchListResponse = JsonConvert.DeserializeObject<ResearchListResponseDTO>(data);
				ResearchListResponseDTO.Item[] items = researchListResponse.Payload.Items;

				Proto cardProto;


				//ArrayList hqList = protoRepository[x => x.Type == Sun.CardProtos.Enums.Type.HQ];
				//ArrayList cardList = protoRepository[x => x.Type != Sun.CardProtos.Enums.Type.HQ];


				List<CardItem> hqList = new List<CardItem>();
				List<CardItem> cardList = new List<CardItem>();

				foreach(ResearchListResponseDTO.Item item in items){
					cardProto = protoRepository[item.ProtoID];
					if(cardProto.Type == Sun.CardProtos.Enums.Type.HQ){
						hqList.Add(new CardItem(item.ProtoID, item.Count));
					}else{
						cardList.Add(new CardItem(item.ProtoID, item.Count));
					}

				}

				TaskExecutorScript.getInstance().ScheduleTask(new Task(delegate
					{
						PlayerData.Saved.hqList = hqList;
						PlayerData.Saved.cardList = cardList;
						PlayerData.Save();
						app.view.deckBuilder.loadHqs();
					}
				));

				break;

			case ServerNotification.DECKS_SAVE:
				Debug.Log ("DeckBuilderController caught DECK_SAVE");
				DecksSaveResponseDTO deckSaveResponse = JsonConvert.DeserializeObject<DecksSaveResponseDTO>(data);
				if(deckSaveResponse.Success){
					TaskExecutorScript.getInstance().ScheduleTask(new Task(delegate
					{
						DeckDTO deck = deckSaveResponse.Deck;

						bool deckFound = false;
						List<DeckDTO> deckList = PlayerData.Saved.deckList;
						for(int i = 0; i < deckList.Count; i++){
							if(deckList[i].ID == deck.ID){
								deckFound = true;
								deckList[i] = deck;
								break;
							}
						}

						if(!deckFound){
							deckList.Add(deckSaveResponse.Deck);
						}

						PlayerData.Save();

						app.view.deckBuilder.addOrUpdateDeckInList(deck);
						app.view.deckBuilder.backToSavedDeck(deck.ID);
					}
					));
				}
				break;
		}
	}

	public override void OnNotification (string p_event_path, Object p_target, params object[] p_data)
	{
		throw new System.NotImplementedException ();
	}

}
