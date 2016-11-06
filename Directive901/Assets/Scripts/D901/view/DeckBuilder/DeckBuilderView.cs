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
	[SerializeField] public Transform decksContainer;
	//[SerializeField] public Transform bigCardPrefab;
	//[SerializeField] public Transform hqContainer;
	//[SerializeField] public Transform hqSelector;
	//[SerializeField] public Transform deckInfo;

	[SerializeField] public HqSelector hqSelector;
	[SerializeField] public DeckInfo deckInfo;
	[SerializeField] public Transform deckEditor;

	private CardProtosRepository protoRepository = new CardProtosRepository();
	private List<DeckDTO> decks;
	private List<CardItem> hqs;
	private int selectedHq;

	private DeckDTO currentDeck;
	private long? selectedDeck;

	private List<CardItem> collection;
	private List<CardItem> filteredCollection;



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
		refreshHq ();
		/*Transform hqInstance;
		if (bigCardPrefab != null && hqContainer != null) {
			hqInstance = Instantiate (bigCardPrefab);
			hqCard = hqInstance.GetComponent<Card> ();
			refreshHq ();
			hqInstance.SetParent(hqContainer, false);
		}*/
	}

	private void refreshHq(){
		CardItem item = getSelectedHq();
		Proto cardProto = protoRepository [item.ProtoID];
		hqSelector.Init (cardProto.ID,
			null,
			cardProto.ID,
			cardProto.ID
		);
	}

	private void refreshDeckInfo(DeckDTO deck){
		Proto cardProto = protoRepository [deck.HQ];
		//TODO correct use of deckInfo prefab necessary
		deckInfo.Init (deck.ID,
			cardProto.ID,
			null,
			cardProto.ID,
			cardProto.ID,
			deck.Title
		);
		//Card hqCard = deckInfo.GetComponentInChildren<Card> ();
		/*hqCard.Init (cardProto.ID,
			null,
			cardProto.ID,
			cardProto.ID
		);*/
	}

	public void reloadDecks(){
		decks = PlayerData.Saved.deckList;
		Transform deckInstance;
		if (decks != null) {
			Debug.Log ("decks number = " + decks.Count);

			removeChildren (decksContainer);
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
		if (deckPrefab != null && decksContainer != null) {
			Transform deckInstance = Instantiate (deckPrefab);
			//deckInstance.GetComponentInChildren<Text> ().text = deck.Title;
			deckInstance.GetComponent<DeckListElement> ().Init(deck.ID, null, deck.Title, 1234);
			//deckInstance.GetComponent<Card>().Title = deck.Title;
			deckInstance.SetParent(decksContainer, false);
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



	public void newDeck()
	{
		string name = this.ifDeckName.text;

		//TODO process error correctly
		if (name.Length == 0) {
			Debug.LogError ("Deck name cannot be empty!");
			return;
		}

		CardItem hq = getSelectedHq (); 

		this.currentDeck = new DeckDTO();
		this.currentDeck.HQ = hq.ProtoID;
		this.currentDeck.Title = name;

		this.deckList.gameObject.SetActive (false);
		this.deckEditor.gameObject.SetActive (true);

	}

	public void editDeck(long? deckId)
	{
		this.currentDeck = getDeckById(deckId);

		this.deckList.gameObject.SetActive (false);
		this.deckEditor.gameObject.SetActive (true);
	}

	public void addToDeck(string protoId)
	{
		//loop over filtered collection and find by proto
		for(int i = 0 ; i < this.filteredCollection.Count ; i++){
			if(this.filteredCollection[i].ProtoID == protoId){
				//remove from filtered collection or update quantity
				if (this.filteredCollection [i].Count > 1) {
					this.filteredCollection [i].Count--;
				} else {
					this.filteredCollection.RemoveAt (i);
					break;
				}				
			}
		}
		//loop over currentDeck.cards
		//add to currentDeck.cards or update quantity
		bool foundInDeck = false;
		for(int i = 0 ; i < this.currentDeck.Cards.Count ; i++){
			if(this.currentDeck.Cards[i].ProtoID == protoId){
				foundInDeck = true;
				if(this.currentDeck.Cards[i].Count < 3){
					this.currentDeck.Cards [i].Count++;	
				}
				break;
			}
		}
		if (!foundInDeck) {
			DeckDTO.DeckCard card = new DeckDTO.DeckCard ();
			card.Count = 1;
			card.ProtoID = protoId;
			this.currentDeck.Cards.Add (card);
		}




	}

	public void removeFromDeck(string protoId)
	{
		
	}

}
