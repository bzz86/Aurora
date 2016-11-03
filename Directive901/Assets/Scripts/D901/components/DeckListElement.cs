using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeckListElement : D901BaseObject {

	[SerializeField] Image deckPicture;
	[SerializeField] Text deckName;
	[SerializeField] Text deckPower;
	private long? id;

	void OnClickDo()
	{
		//Здесь будет твой код что надо делать на клике.
		Debug.Log("click on deck " + this.id);
		app.view.deckBuilder.selectDeck (this.id);
	}

	public void Init(long? id, Sprite s, string name, int power)
	{
		this.id = id;
		//deckPicture.sprite = s;
		this.deckName.text = name;
		//deckPower.text = power.ToString();
	}

}