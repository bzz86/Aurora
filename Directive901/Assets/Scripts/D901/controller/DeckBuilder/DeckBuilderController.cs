using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sun.CardProtos;
using UnityEngine.UI;
using Sun.DTO.Entities;
using Sun.DTO.Responses;
using Newtonsoft.Json;

public class DeckBuilderController : D901Controller {

	private CardProtosRepository protoRepository = new CardProtosRepository();

	public void btnCreateDeckClick(){
		if (app.view.deckBuilder.ifDeckName.text.Length > 0) {
			DeckBuilderService.getInstance ().SaveDeck (
				null,
				app.view.deckBuilder.ifDeckName.text,
				app.view.deckBuilder.getSelectedHq().ProtoID,
				new CardItem[] { }
			);
		} else {
			//TODO visualize the reason of the issue
			Debug.LogError("Deck name cant be empty");
		}
	}

	public override void OnServerNotification (string commandName, string data)
	{
		switch (commandName) {
			case "decks/list":
				DecksListResponseDTO deckListResponse = JsonConvert.DeserializeObject<DecksListResponseDTO>(data);
				if(deckListResponse.Success){
					TaskExecutorScript.getInstance().ScheduleTask(new Task(delegate
						{
							foreach(DeckDTO deck in deckListResponse.Payload.Decks){
								app.view.deckBuilder.addDeckToList(deck);	
							}
						}
					));	
				}
				break;

			case "research/list":
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
						//SceneManager.LoadScene(1);	
					}
				));

				break;

			case "decks/save":
				DecksSaveResponseDTO deckSaveResponse = JsonConvert.DeserializeObject<DecksSaveResponseDTO>(data);
				if(deckSaveResponse.Success){
					TaskExecutorScript.getInstance().ScheduleTask(new Task(delegate
						{
							app.view.deckBuilder.addDeckToList(deckSaveResponse.Deck);
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
