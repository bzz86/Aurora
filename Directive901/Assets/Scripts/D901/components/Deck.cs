using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Sun.DTO.Entities;

public class Deck : D901BaseObject {
	[SerializeField] DeckCard cardPrefab;
	[SerializeField] Transform cardsContainer;
	[SerializeField] Text cardsNumber;
	[SerializeField] Text weight;


	public void initDeck(DeckDTO currentDeck, int count, int weight){
		ViewUtils.removeChildrenFromTransform(cardsContainer);
		foreach(DeckDTO.DeckCard item in currentDeck.Cards){
			DeckCard cardInstance = Instantiate (cardPrefab);
			cardInstance.Init(item.ProtoID, null, item.ProtoID, item.ProtoID, 1234, item.Count);
			cardInstance.transform.SetParent(cardsContainer, false);
		}
		updateShortInfo(count, weight);
	}

	public void addCardToDeck(string protoId, int position){
		DeckCard cardInstance = Instantiate (cardPrefab);
		cardInstance.Init(protoId, null, protoId, protoId, 1234, 1);
		cardInstance.transform.SetSiblingIndex(position);
		cardInstance.transform.SetParent(cardsContainer, false);
	}

	public void removeCardFromDeck(int position){
		ViewUtils.removeChildByIndex (cardsContainer, position);
	}

	public void updateCardAtPosition(int count, int position){
		Transform cardInstance = cardsContainer.GetChild (position);
		if(cardInstance != null){
			cardInstance.GetComponent<DeckCard> ();			
		}
	}

	public void updateShortInfo(int cardsNumber, int weight){
		this.cardsNumber.text = cardsNumber.ToString() + "/40";
		this.weight.text = weight.ToString();		
	}

}
