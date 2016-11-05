using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Card : D901BaseObject {
	[SerializeField] Image art;
	[SerializeField] Text title;
	[SerializeField] Text description;

	protected string ProtoId;

	public virtual void OnClickDo()
	{
		Debug.Log("click on card " + this.ProtoId);
		//app.view.deckBuilder.selectDeck (this.ProtoId);
	}

	public void Init(string protoId,
		Sprite art, 
		string title, 
		string description)
	{
		this.ProtoId = protoId;
		//this.art.sprite = art;
		this.title.text = name;
		//this.description = 
	}


}
