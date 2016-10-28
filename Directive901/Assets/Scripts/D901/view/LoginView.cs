using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using Sun.DTO.Helpers;

public class LoginView : D901BaseObject {
	[SerializeField]public GameObject loginPanel=null,
							 registerPanel=null,
								dummyPanel=null;

	[SerializeField]public InputField ifName,ifPass,ifEmail;

	[SerializeField]public InputField ifLogin,ifPassword;

	[SerializeField]public Toggle tglRemember;

	public static LoginView instance {get; private set;}

	public enum State {Login, Register, Dummy};
	static State state = State.Login;
	public void SetState(State s){state=s;}

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



}
