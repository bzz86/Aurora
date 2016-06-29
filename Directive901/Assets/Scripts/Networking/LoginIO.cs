using UnityEngine;
using System.Collections;
using WebSocketSharp;
using Newtonsoft.Json;
using Sun.DTO.Requests;
using Sun.DTO.Helpers;
using System;
using Sun.DTO.Responses;
using Newtonsoft.Json.Linq;

public class LoginIO : MonoBehaviour {

	static WebSocket ws;
	static Guid token;
	static LoginIO instance;

	void Awake()
	{
		instance=this;
	}

	void Start () {
		CreateWebSoket();
	}

	void CreateWebSoket()
	{
		ws = new WebSocket("ws://localhost:9010/game");
			ws.OnMessage += (s, e) =>
			{
				Debug.Log(string.Format("Response: {0}", e.Data));

				var response = JObject.Parse(e.Data);
				
				Debug.LogWarning("C: " + response["command"] + "S: " + response["sucess"] );

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

	public static void SendRegistration(string name="test", string email="test", string password="test")
	{
		var request = new RegistrationRequestDTO();
		request.Command = "player/registration";
		request.Payload = new RegistrationRequestDTO.Data();
		request.Payload.Name = name;
		request.Payload.Email = email;
		request.Payload.Password = SHA256Hash.HashString(password);

		Debug.Log(JsonConvert.SerializeObject(request));

		ws.Send(JsonConvert.SerializeObject(request));
	}

	public static void SendAuthorization(string name="test", string password="test")
	{
		var request = new AuthorizationRequestDTO();
		request.Command = "player/authorization";
		request.Payload = new AuthorizationRequestDTO.Data();
		request.Payload.Name = name;
		request.Payload.Password = SHA256Hash.HashString(password);

		Debug.Log(JsonConvert.SerializeObject(request));

		ws.Send(JsonConvert.SerializeObject(request));
	}

	public static void SendExit()
	{
		var request = new RequestWithToken();
		request.Command = "player/exit";
		request.Token = token;

		Debug.Log(JsonConvert.SerializeObject(request));

		ws.Send(JsonConvert.SerializeObject(request));
	}
}
