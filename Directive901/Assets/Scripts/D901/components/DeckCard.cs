using UnityEngine;
using System.Collections;

public class DeckCard : Card {

	public override void OnClickDo()
	{
		Debug.Log("click on deck card " + this.ProtoId);
		//app.view.deckBuilder.selectDeck (this.ProtoId);
	}

}
