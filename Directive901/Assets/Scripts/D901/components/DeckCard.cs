using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeckCard : Card {

	[SerializeField] Text weight;
	[SerializeField] Text quantity;

	public void Init(string protoId,
		Sprite art, 
		string title, 
		string description,
		int weight,
		int quantity
	)
	{
		base.Init (protoId, art, title, description);
		this.weight.text = weight.ToString();
		this.quantity.text = 'x' + quantity.ToString();
	}

	public override void OnClickDo()
	{
		Debug.Log("click on deck card " + this.ProtoId);
		//app.view.deckBuilder.selectDeck (this.ProtoId);
	}

}
