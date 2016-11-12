using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Sun.DTO.Requests;
using Sun.DTO.Responses;
using Sun.DTO.Helpers;

public class LoginController : D901Controller {

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
				app.view.loginPanels.SetState (LoginView.State.Login);
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
				PlayerData.Saved.token = authResponse.Payload.Token;
				//LoginPanelsUI.SetState(LoginPanelsUI.State.Dummy);
				Debug.LogWarning("PlayerData.Saved.token=" + PlayerData.Saved.token);

				TaskExecutorScript.getInstance ().ScheduleTask (new Task (delegate {
					PlayerData.Save ();

					//success auth => call research list and deck list


					//DeckBuilderService.getInstance().GetResearchList();
					//DeckBuilderService.getInstance().GetDeckList();

					SceneManager.LoadScene ("DeckBuilderScene");
					//SceneLoader.getInstance().loadSceneById(1);
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


	public void btnRegister()
	{
		//app.controller.loginController.S
		LoginService.getInstance().SendRegistration(
			app.view.loginPanels.ifName.text,
			app.view.loginPanels.ifEmail.text,
			app.view.loginPanels.ifPass.text);
	}

	public void btnLogin()
	{
		if ((app.view.loginPanels.ifLogin.text.Length>=GC.MIN_LENGTH) 
			&& (app.view.loginPanels.ifPassword.text.Length>=GC.MIN_LENGTH))
		{

			PlayerData.Saved.name=app.view.loginPanels.ifLogin.text;

			if (app.view.loginPanels.tglRemember.isOn)
			{
				if (app.view.loginPanels.ifPassword.text!=GC.PASS_DUMMY)
				{
					string hash=SHA256Hash.HashString(app.view.loginPanels.ifPassword.text);
					if (PlayerData.Saved.passwordHash != hash)
						PlayerData.Saved.passwordHash=hash;
				}
			}

			PlayerData.Saved.remember=app.view.loginPanels.tglRemember.isOn;	

			PlayerData.Save();

			LoginService.getInstance().SendAuthorization(
				app.view.loginPanels.ifLogin.text,
				app.view.loginPanels.ifPassword.text);

		}
		else
		{
			//TODO visualize the reason of the issue
			Debug.LogError("Login and/or password too short! Please check if it is OK.");
		}
	}

	public void btnLogOut()
	{
		LoginService.getInstance().SendExit();
	}

}
