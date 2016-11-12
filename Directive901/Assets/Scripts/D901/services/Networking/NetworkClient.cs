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


public class NetworkClient : D901Singleton<NetworkClient>{
	private WebSocket ws;
	private Guid token;

	void Awake(){
		Debug.Log ("Network Client awake");

		base.onAwake (this);
		if (NetworkClient.getInstance() != null && 
			(NetworkClient.getInstance().checkStatus() == null || NetworkClient.getInstance().checkStatus() == WebSocketState.Closed)
		) {
			Debug.Log ("CreateWebSoket");
			CreateWebSoket();
			reloadToken ();
		}
	}

	void Start(){
		Debug.Log ("Network Client start");
	}


	public WebSocket getWs () {
		if (this.ws == null) {
			CreateWebSoket();
		}
		return this.ws;
	}

	public WebSocketState? checkStatus(){
		if (this.ws == null) {
			return null;
		}
		return this.ws.ReadyState;
	}

	public Guid getToken () {
		return this.token;
	}

	public void reloadToken() {
		this.token = PlayerData.Saved.token;
	}

	private void CreateWebSoket()
	{
		Debug.Log ("socket creation start");
		ws = new WebSocket("ws://127.0.0.1:9010/game");

		ws.OnMessage += (s, e) =>
		{
			Debug.Log(string.Format("Response: {0}", e.Data));

			try
			{
				var br = JObject.Parse(e.Data);

				Debug.LogWarning("C: " + br["command"] + " S: " + br["success"] +"." );

				string command=br["command"].ToString();

				TaskExecutorScript.getInstance().ScheduleTask(new Task(delegate
					{
						app.ServerNotification(command, e.Data);
					}
				));

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
		ws.OnError += (s, e) =>
		{
			Debug.Log("WS error: " + e.Message);
		};
		ws.OnClose += (s, e) =>
		{
			string errorMessage = "Code: " + e.Code + "; reason: " + e.Reason;
			ConnectionProblems.error = errorMessage;
			Debug.Log("Close WS. " + errorMessage);

			TaskExecutorScript.getInstance().ScheduleTask(new Task(delegate
				{
					SceneManager.LoadScene("ConnectionProblemScene");
				}
			));

		};
		ws.Connect();
	}

	public void Send(string data){
		Debug.Log ("token=" + token);
		Debug.Log (PlayerData.Saved);
		if (token == Guid.Empty) {
			token = PlayerData.Saved.token;
		}
		ws.Send (data);
	}

}
