using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Deck : D901BaseObject {
	[SerializeField]Transform cardPrefab;
	[SerializeField]Transform cardsContainer;
	[SerializeField]Text cardsNumber;
	[SerializeField]Text weight;


	public void addCardToDeck(string protoId, int position){
		Transform cardInstance = Instantiate (cardPrefab);
		cardInstance.GetComponent<DeckCard> ().Init(protoId, null, protoId, protoId, 1234, 1);
		cardInstance.SetSiblingIndex(position);
		cardInstance.SetParent(cardsContainer, false);
	}


	public void removeCardFromDeck(){
		
	}

	public void updateCardAtPosition(){
		
	}

}
