using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectionProblems : MonoBehaviour {

	public static string error;

	[SerializeField] Text errorMessage;


	void Awake(){
		this.errorMessage.text = error;
	}

	public void Init(string errorMessage){
		this.errorMessage.text = errorMessage;		
	}

	public void btnReconnectClick(){
		SceneManager.LoadScene ("LoginScene");
	}

}
