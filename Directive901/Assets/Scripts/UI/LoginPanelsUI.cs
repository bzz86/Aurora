using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Sun.DTO.Helpers;

public class LoginPanelsUI : MonoBehaviour {
	[SerializeField] GameObject loginPanel=null,
							 registerPanel=null,
								dummyPanel=null;

	[SerializeField] InputField ifName,ifPass,ifEmail;

	[SerializeField] InputField ifLogin,ifPassword;

	[SerializeField] Toggle tglRemember;

	public static LoginPanelsUI instance {get; private set;}

	public enum State {Login, Register, Dummy};
	static State state = State.Login;
	public static void SetState(State s){state=s;}

	void Awake(){instance=this;}

	void Start () {
		
		ifLogin.text = PlayerData.Saved.name;	
		tglRemember.isOn= PlayerData.Saved.remember;

		if (tglRemember.isOn) 
			ifPassword.text=GC.PASS_DUMMY;

		btnShowLogin();	
	}

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
		if ((ifLogin.text.Length>=GC.MIN_LENGTH) 
			&& (ifPassword.text.Length>=GC.MIN_LENGTH))
		{

			PlayerData.Saved.name=ifLogin.text;

			if (tglRemember.isOn)
			{
				if (ifPassword.text!=GC.PASS_DUMMY)
				{
					string hash=SHA256Hash.HashString(ifPassword.text);
					if (PlayerData.Saved.passwordHash != hash)
						PlayerData.Saved.passwordHash=hash;
				}
			}

			PlayerData.Saved.remember=tglRemember.isOn;	

			PlayerData.Save();

			LoginIO.SendAuthorization(
				ifLogin.text,
				ifPassword.text);

		}
		else
		{
			//TODO visualize the reason of the issue
			Debug.LogError("Login and/or password to short! Please check if it is OK.");
		}
	}

	public void btnLogOut()
	{
		LoginIO.SendExit();
	}
}
