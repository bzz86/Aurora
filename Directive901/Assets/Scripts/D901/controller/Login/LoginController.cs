using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Sun.DTO.Responses;

public class LoginController : D901Controller {

	LoginIO loginService = LoginIO.getInstance();

	public override void OnServerNotification (string commandName, string data)
	{
		switch(commandName)
		{
		case ServerNotification.PLAYER_REGISTRATION:
			//app.model.bounces++;
			Debug.Log ("LoginController caught PLAYER_REGISTRATION");
			RegistrationResponseDTO registrationResponse = JsonConvert.DeserializeObject<RegistrationResponseDTO> (data);

			Debug.LogWarning (registrationResponse.Success);

			if (registrationResponse.Success) {
				app.view.loginPanels.SetState (LoginPanelsUI.State.Login);
			}
			/*if(app.model.bounces >= app.model.winCondition)
			{
				app.view.ball.enabled = false;
				app.view.ball.GetComponent<RigidBody>().isKinematic=true; // stops the ball
				// Notify itself and other controllers possibly interested in the event
				app.Notify(BounceNotification.GameComplete,this);            
			}*/
			break;

		case ServerNotification.PLAYER_AUTH:
			Debug.Log ("LoginController caught PLAYER_AUTH");

			AuthorizationResponseDTO authResponse = JsonConvert.DeserializeObject<AuthorizationResponseDTO> (data);

			Debug.LogWarning (authResponse.Success);

			if (authResponse.Success) {
				//PlayerData.token = authResponse.Payload.Token;
				//LoginPanelsUI.SetState(LoginPanelsUI.State.Dummy);

				TaskExecutorScript.getInstance ().ScheduleTask (new Task (delegate {
					PlayerData.Save ();

					//success auth => call research list and deck list
					/*ResearchListRequestDTO researchReq = new ResearchListRequestDTO ();
					researchReq.Command = "research/list";
					researchReq.Token = token;

					Debug.Log (JsonConvert.SerializeObject (researchReq));

					ws.Send (JsonConvert.SerializeObject (researchReq));


					DecksListRequestDTO deckListReq = new DecksListRequestDTO ();
					deckListReq.Command = "decks/list";
					deckListReq.Token = token;

					Debug.Log (JsonConvert.SerializeObject (deckListReq));

					ws.Send (JsonConvert.SerializeObject (deckListReq));
*/

					SceneManager.LoadScene (1);
				}
				));
			}
			break;

		case ServerNotification.PLAYER_EXIT:
			Debug.Log("exit");
			break;
		} 
	}

	public override void OnNotification (string p_event_path, Object p_target, params object[] p_data)
	{
		throw new System.NotImplementedException ();
	}


}
