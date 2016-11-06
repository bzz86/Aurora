using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeckInfo : D901BaseObject {
	[SerializeField] Text deckName;
	[SerializeField] Card hqCard;

	private long? id;


	public void Init(long? id,
		string protoId,
		Sprite art, 
		string title, 
		string description,
		string deckName)
	{
		Debug.Log ("DeckInfo init: id = " + id + ", protoId = " + protoId + ", art=" + art + ", title =" + title + ", description =" + description);
		this.deckName.text = deckName;
		hqCard.Init (protoId,
			art,
			title,
			description
		);

	}

	public void btnEditClick(){
		app.view.deckBuilder.editDeck (id);
	}


}
