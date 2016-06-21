using UnityEngine;
using System.Collections;

public class LoginPanelsUI : MonoBehaviour {
	[SerializeField] GameObject loginPanel,registerPanel;

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

}
