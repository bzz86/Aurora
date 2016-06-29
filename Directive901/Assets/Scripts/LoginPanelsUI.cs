using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginPanelsUI : MonoBehaviour {
	[SerializeField] GameObject loginPanel=null,
							 registerPanel=null;

	[SerializeField] InputField ifName,ifPass,ifEmail;

	[SerializeField] InputField ifLogin,ifPassword;

	void Start () {
		btnShowLogin();
	}

	public void btnShowRegister()
	{
		loginPanel.SetActive(false);
		registerPanel.SetActive(true);
	}

	public void btnShowLogin()
	{		
		loginPanel.SetActive(true);
		registerPanel.SetActive(false);
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
