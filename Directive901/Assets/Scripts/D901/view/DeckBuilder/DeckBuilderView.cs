using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Sun.CardProtos;
using Sun.DTO.Entities;

public class DeckBuilderView : D901BaseObject {

	[SerializeField] public InputField ifDeckName;

	[SerializeField] public Transform deckPrefab;
	[SerializeField] public Transform deckList;
	[SerializeField] public Transform bigCardPrefab;
	[SerializeField] public Transform hqContainer;
	[SerializeField] public Transform hqSelector;
	[SerializeField] public Transform deckInfo;

	private CardProtosRepository protoRepository = new CardProtosRepository();
	private List<DeckDTO> decks;
	private List<CardItem> hqs;
	private int selectedHq;

	private long? selectedDeck;

	Card hqCard;

	void Start () {
		
		loadHqs ();
		reloadDecks ();
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


	public void loadHqs(){
		hqs = PlayerData.Saved.hqList;

		Transform hqInstance;
		if (bigCardPrefab != null && hqContainer != null) {
			hqInstance = Instantiate (bigCardPrefab);
			hqCard = hqInstance.GetComponent<Card> ();
			refreshHq ();
			hqInstance.SetParent(hqContainer, false);
		}
	}

	private void refreshHq(){
		CardItem item = getSelectedHq();
		Proto cardProto = protoRepository [item.ProtoID];
		//TODO fix it
		/*hqCard.Init (cardProto.ID,
			null,
			cardProto.ID,
			cardProto.ID
		);*/
	}

	private void refreshDeckInfo(DeckDTO deck){
		Proto cardProto = protoRepository [deck.HQ];
		//TODO correct use of deckInfo prefab necessary
		Card hqCard = deckInfo.GetComponentInChildren<Card> ();
		hqCard.Init (cardProto.ID,
			null,
			cardProto.ID,
			cardProto.ID
		);
	}

	public void reloadDecks(){
		decks = PlayerData.Saved.deckList;
		Transform deckInstance;
		if (decks != null) {
			Debug.Log ("decks number = " + decks.Count);

			removeChildren (deckList);
			foreach (DeckDTO deck in decks) {
				addDeckToList (deck);	
			}
		}
	}

	private DeckDTO getDeckById(long? id){
		if (id != null) {
			foreach (DeckDTO deck in decks) {
				if (deck.ID == id) {
					return deck;
				}
			}		
		}
		return null;
	}

	private void removeChildren(Transform parent){
		if (parent != null) {
			foreach (Transform child in parent) {
				GameObject.Destroy (child.gameObject);
			}
		}
	}

	public void addDeckToList(DeckDTO deck){
		if (deckPrefab != null && deckList != null) {
			Transform deckInstance = Instantiate (deckPrefab);
			//deckInstance.GetComponentInChildren<Text> ().text = deck.Title;
			deckInstance.GetComponent<DeckListElement> ().Init(deck.ID, null, deck.Title, 1234);
			//deckInstance.GetComponent<Card>().Title = deck.Title;
			deckInstance.SetParent(deckList, false);
		}
	}

	public void btnBackgroundClick(){
		Debug.Log ("bg click");
		deselectDeck ();
	}

	public void selectDeck(long? ID)
	{
		Debug.Log ("select Deck");
		if (this.selectedDeck != ID) {
			this.selectedDeck = ID;
			refreshDeckInfo (getDeckById(this.selectedDeck));
			this.hqSelector.gameObject.SetActive (false);
			this.deckInfo.gameObject.SetActive (true);
		}
	}

	public void deselectDeck()
	{
		Debug.Log ("deselect Deck");
		selectedDeck = null;
		this.deckInfo.gameObject.SetActive (false);
		this.hqSelector.gameObject.SetActive (true);
	}
}
