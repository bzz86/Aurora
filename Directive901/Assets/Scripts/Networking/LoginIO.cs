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

	private static LoginIO instance;
	private NetworkClient client;

	void Awake(){
		instance = this;
	}

	void Start(){
		client = NetworkClient.getInstance();
	}

	public static LoginIO getInstance()
	{
		return instance;
	}


	public void SendRegistration(string name="test", string email="test", string password="test")
	{
		var request = new RegistrationRequestDTO();
		request.Command = "player/registration";
		request.Payload = new RegistrationRequestDTO.Data();
		request.Payload.Name = name;
		request.Payload.Email = email;
		request.Payload.Password = SHA256Hash.HashString(password);

		Debug.Log(JsonConvert.SerializeObject(request));

		client.Send(JsonConvert.SerializeObject(request));
	}

	public void SendAuthorization(string name="test", string password="test")
	{

		Debug.Log ("ws =" + client.getWs());
		var request = new AuthorizationRequestDTO();
		request.Command = "player/authorization";
		request.Payload = new AuthorizationRequestDTO.Data();
		request.Payload.Name = name;

		Debug.Log(password.Length);

		if (password==GC.PASS_DUMMY)
			request.Payload.Password = PlayerData.Saved.passwordHash;
		else
			request.Payload.Password = SHA256Hash.HashString(password);

		Debug.Log(JsonConvert.SerializeObject(request));

		client.Send(JsonConvert.SerializeObject(request));
	}

	public void SendExit()
	{
		var request = new RequestWithToken();
		request.Command = "player/exit";
		request.Token = client.getToken();

		Debug.Log(JsonConvert.SerializeObject(request));

		client.Send(JsonConvert.SerializeObject(request));

		Application.Quit();
	}
}
