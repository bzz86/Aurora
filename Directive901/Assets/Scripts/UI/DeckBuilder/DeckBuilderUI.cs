using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sun.CardProtos;
using UnityEngine.UI;
using Sun.DTO.Entities;

public class DeckBuilderUI : MonoBehaviour {


	[SerializeField] InputField ifDeckName;
	public static DeckBuilderUI instance;

	//public HangarUI hangarUi;

	public Transform deckPrefab;
	public Transform deckList;
	public Transform bigCardPrefab;
	public Transform hqContainer;

	public List<Item> hqs;
	public int selectedHq;
	private CardProtosRepository protoRepository;

	Card hqCard;


	void Awake(){instance=this;}

	void Start () {
		protoRepository = new CardProtosRepository ();
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


	public static DeckBuilderUI getInstance()
	{
		return instance;
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


	public void btnCreateDeckClick(){
		if (ifDeckName.text.Length > 0) {
			DeckBuilderIO.getInstance ().SaveDeck (
				null,
				ifDeckName.text,
				hqs [selectedHq].ProtoID,
				new Item[] { }
			);
		} else {
			//TODO visualize the reason of the issue
			Debug.LogError("Deck name cant be empty");
		}
	}



	private void refreshHq(){
		Item item = hqs[selectedHq];
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
