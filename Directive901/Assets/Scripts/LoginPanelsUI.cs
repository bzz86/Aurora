using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginPanelsUI : MonoBehaviour {
	[SerializeField] GameObject loginPanel=null,
							 registerPanel=null,
								dummyPanel=null;

	[SerializeField] InputField ifName,ifPass,ifEmail;

	[SerializeField] InputField ifLogin,ifPassword;

	public static LoginPanelsUI instance {get; private set;}

	public enum State {Login, Register, Dummy};
	static State state = State.Login;
	public static void SetState(State s){state=s;}

	void Awake(){instance=this;}

	void Start () {	btnShowLogin();	}

	void Update()
	{
		switch (state)
		{
		case State.Login:    btnShowLogin();    break;
		case State.Register: btnShowRegister(); break;
		case State.Dummy:    btnShowDummy();    break;
		}
	}

	public void btnShowRegister()
	{
		state=State.Register;
		loginPanel.SetActive(false);
		registerPanel.SetActive(true);
		dummyPanel.SetActive(false);
	}

	public void btnShowLogin()
	{		
		state=State.Login;
		loginPanel.SetActive(true);
		registerPanel.SetActive(false);
		dummyPanel.SetActive(false);
	}

	public void btnShowDummy()
	{	
		state=State.Dummy;
		loginPanel.SetActive(false);
		registerPanel.SetActive(false);
		dummyPanel.SetActive(true);
	}

	public void btnRegister()
	{
		LoginIO.SendRegistration(
			ifName.text,
			ifEmail.text,
			ifPass.text);
	}

	public void btnLogin()
	{
		LoginIO.SendAuthorization(
			ifLogin.text,
			ifPassword.text);
	}

	public void btnLogOut()
	{
		LoginIO.SendExit();
	}
}
