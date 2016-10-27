using UnityEngine;
using System.Collections;
using Sun.DTO.Requests;
using Sun.DTO.Helpers;
using Newtonsoft.Json;

public class TestService {

	private static NetworkClient client = NetworkClient.getInstance();
	private static TestService instance;


	private TestService(){
		Debug.Log ("TestService constructor");
		client = NetworkClient.getInstance ();
	}

	public static TestService getInstance(){
		if (instance == null || client == null) {
			instance = new TestService ();
		}		
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
		Debug.Log (client);
		client.Send(JsonConvert.SerializeObject(request));
	}

}
