using UnityEngine;
using System.Collections;

public class TestController : D901Controller {

	//TestService testService = TestService.getInstance();

	public override void OnServerNotification (string commandName, string data)
	{
		Debug.Log ("TestController received ServerNotification: " + commandName + "; data = " + data);
		switch(commandName)
		{
		case ServerNotification.PLAYER_REGISTRATION:
			Debug.Log ("LoginController caught PLAYER_REGISTRATION");
			app.view.testView.testText.text = "LoginController caught PLAYER_REGISTRATION";
			/*Debug.Log (app);
			Debug.Log (app.model);
			Debug.Log (app.model.testModel);
			app.model.testModel.TestMessage = "LoginController caught PLAYER_REGISTRATION";
			Debug.Log (app);
			Debug.Log (app.view);
			Debug.Log (app.view.testView);
			app.view.testView.refreshView ();*/
				break;
		}
	}


	public override void OnNotification (string p_event_path, Object p_target, params object[] p_data)
	{
		throw new System.NotImplementedException ();
	}

	public void btnTestClick(){
		Debug.Log ("btnTestClick");
		TestService.getInstance().SendRegistration ();
	}
}
