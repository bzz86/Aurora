using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Card : D901BaseObject {
	[SerializeField] Image art;
	[SerializeField] Text title;
	[SerializeField] Text description;

	protected string ProtoId;

	public virtual void Init(string protoId,
		Sprite art, 
		string title, 
		string description)
	{
		Debug.Log ("Card init: protoId = " + protoId + ", art=" + art + ", title =" + title + ", description =" + description);
		this.ProtoId = protoId;
		//this.art.sprite = art;
		this.title.text = title;
		//this.description = 
	}

	public virtual void OnClickDo()
	{
		Debug.Log("click on card " + this.ProtoId);
		//app.view.deckBuilder.selectDeck (this.ProtoId);
	}




}
