using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeckListElement : D901BaseObject {

	[SerializeField] Image deckPicture;
	[SerializeField] Text deckName;
	[SerializeField] Text deckPower;
	public long? ID {
		get;
		private set;
	}

	void OnClickDo()
	{
		Debug.Log("click on deck " + this.ID);
		app.view.deckBuilder.selectDeck (this.ID);
	}

	public void Init(long? id, Sprite s, string name, int power)
	{
		this.ID = id;
		//deckPicture.sprite = s;
		this.deckName.text = name;
		//deckPower.text = power.ToString();
	}

}