using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Sun.CardProtos;
using Sun.DTO.Entities;

public class DeckBuilderView : D901BaseObject {

	[SerializeField] public InputField ifDeckName;
	private CardProtosRepository protoRepository = new CardProtosRepository();
	//public HangarUI hangarUi;

	public Transform deckPrefab;
	public Transform deckList;
	public Transform bigCardPrefab;
	public Transform hqContainer;

	private List<CardItem> hqs;
	public int selectedHq;

	Card hqCard;

	void Start () {
		hqs = PlayerData.Saved.hqList;

		Transform hqInstance;
		if (bigCardPrefab != null && hqContainer != null) {
			hqInstance = Instantiate (bigCardPrefab);
			hqCard = hqInstance.GetComponent<Card> ();
			refreshHq ();
			hqInstance.SetParent(hqContainer, false);
		}


		/*
		 for ( int i = 0; i < 3; i++ ) {
			if (deckPrefab != null && deckList != null) {
				deckInstance = Instantiate (deckPrefab);

				//instance.GetComponent<Card>().Title = "TEST";
				deckInstance.SetParent(deckList, false);
			}
		} 
		 */
	}

	public CardItem getSelectedHq(){
		return hqs[selectedHq];
	}

	public void btnHqMoveLeft()
	{
		if ( selectedHq <= 0 ) {
			selectedHq = hqs.Count-1;			
		} else {
			selectedHq--;
		}
		Debug.Log (selectedHq);

		refreshHq ();
	}

	public void btnHqMoveRight()
	{
		if ( selectedHq >= hqs.Count-1 ) {
			selectedHq = 0;			
		} else {
			selectedHq++;
		}
		Debug.Log (selectedHq);

		refreshHq ();
	}


	private void refreshHq(){
		CardItem item = getSelectedHq();
		Proto cardProto = protoRepository [item.ProtoID];
		hqCard.Title = cardProto.ID;
		hqCard.updateValues ();
	}

	public void addDeckToList(DeckDTO deck){
		if (deckPrefab != null && deckList != null) {
			Transform deckInstance = Instantiate (deckPrefab);
			deckInstance.GetComponentInChildren<Text> ().text = deck.Title;
			//deckInstance.GetComponent<Card>().Title = deck.Title;
			deckInstance.SetParent(deckList, false);
		}
	}
}
