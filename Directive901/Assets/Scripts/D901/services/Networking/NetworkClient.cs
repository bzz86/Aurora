using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using WebSocketSharp;
using Newtonsoft.Json;
using Sun.DTO.Requests;
using Sun.DTO.Helpers;
using Sun.DTO.Entities;
using System;
using Sun.DTO.Responses;
using Newtonsoft.Json.Linq;
using Sun.CardProtos;
using Sun.CardProtos.Enums;


public class NetworkClient : D901BaseObject{
	private static NetworkClient instance;
	private WebSocket ws;
	private Guid token;
	//private TaskExecutorScript taskExecutorScript;
	//private CardProtosRepository protoRepository = new CardProtosRepository ();

	void Awake(){
		if (instance != null && instance != this) {
			DestroyImmediate (gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad(gameObject);
	}

	void Start(){
		CreateWebSoket();
		//token = ;
		//taskExecutorScript = GameObject.Find("####TaskExecutor").transform.GetComponent<TaskExecutorScript> ();
		//protoRepository = new CardProtosRepository ();
	}

	public static NetworkClient getInstance()
	{
		return instance;
	}

	public WebSocket getWs () {
		if (this.ws == null) {
			CreateWebSoket();
		}
		return this.ws;
	}

	public Guid getToken () {
		return this.token;
	}

	private void CreateWebSoket()
	{
		Debug.Log ("socket creation start");
		ws = new WebSocket("ws://localhost:9010/game");

		ws.OnMessage += (s, e) =>
		{
			Debug.Log(string.Format("Response: {0}", e.Data));

			try
			{
				var br = JObject.Parse(e.Data);

				Debug.LogWarning("C: " + br["command"] + " S: " + br["sucess"] +"." );

				string command=br["command"].ToString();

				TaskExecutorScript.getInstance().ScheduleTask(new Task(delegate
					{
						app.ServerNotification(command, e.Data);
					}
				));

				/*switch (command)
				{
				case "player/registration": 
					if (JsonConvert.DeserializeObject<RegistrationResponseDTO>(e.Data).Success)	LoginPanelsUI.SetState(LoginPanelsUI.State.Login);
					break;	

				case "player/authorization":

					var authResponse=JsonConvert.DeserializeObject<AuthorizationResponseDTO>(e.Data);

					Debug.LogWarning(authResponse.Success);

					if (authResponse.Success)
					{
						token = authResponse.Payload.Token;
						//LoginPanelsUI.SetState(LoginPanelsUI.State.Dummy);

						TaskExecutorScript.getInstance().ScheduleTask(new Task(delegate
							{
								PlayerData.Save();

								//success auth => call research list and deck list
								ResearchListRequestDTO researchReq = new ResearchListRequestDTO ();
								researchReq.Command = "research/list";
								researchReq.Token = token;

								Debug.Log(JsonConvert.SerializeObject(researchReq));

								ws.Send(JsonConvert.SerializeObject(researchReq));


								DecksListRequestDTO deckListReq = new DecksListRequestDTO ();
								deckListReq.Command = "decks/list";
								deckListReq.Token = token;

								Debug.Log(JsonConvert.SerializeObject(deckListReq));

								ws.Send(JsonConvert.SerializeObject(deckListReq));


								SceneManager.LoadScene(1);
							}
						));

					}

					break;

				case "decks/list":
					DecksListResponseDTO deckListResponse = JsonConvert.DeserializeObject<DecksListResponseDTO>(e.Data);
					if(deckListResponse.Success){
						TaskExecutorScript.getInstance().ScheduleTask(new Task(delegate
							{
								foreach(DeckDTO deck in deckListResponse.Payload.Decks){
									DeckBuilderUI.getInstance().addDeckToList(deck);	
								}
							}
						));	
					}
					break;

				case "research/list":
					ResearchListResponseDTO researchListResponse = JsonConvert.DeserializeObject<ResearchListResponseDTO>(e.Data);
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
					DecksSaveResponseDTO deckSaveResponse = JsonConvert.DeserializeObject<DecksSaveResponseDTO>(e.Data);
					if(deckSaveResponse.Success){
						TaskExecutorScript.getInstance().ScheduleTask(new Task(delegate
							{
								DeckBuilderUI.getInstance().addDeckToList(deckSaveResponse.Deck);
							}
						));
					}
					break;

				default:
					Debug.LogError("UNCNOWN C: " + br["command"]);
					break;
				}*/

			}

			catch (Exception ex)

			{
				Debug.LogError(ex.ToString());
			}

		};
		ws.OnOpen += (s, e) =>
		{
			Debug.Log("Open WS");
		};
		ws.OnClose += (s, e) =>
		{
			Debug.Log("Close WS");
		};
		ws.Connect();
	}

	public void Send(string data){
		ws.Send (data);
	}

}