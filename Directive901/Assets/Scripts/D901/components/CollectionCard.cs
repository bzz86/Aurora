using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollectionCard : Card {

	[SerializeField] Text quantity;

	public override void OnClickDo(){
		Debug.Log("click on collection card " + this.ProtoId);
		app.view.deckBuilder.addToDeck (this.ProtoId);
	}
}
