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
	[SerializeField] public CardsCollection collectionComponent;
	[SerializeField] public Deck deckComponent; 


	private CardProtosRepository protoRepository = new CardProtosRepository();
	private List<DeckDTO> decks;
	private List<CardItem> hqs;
	private int selectedHq;

	private DeckDTO currentDeck;
	private long? selectedDeck; 

	private List<CardItem> collection;
	private List<CardItem> filteredCollection;


	void Awake (){
		
	}


	void Start () {
		DeckBuilderService.getInstance().GetResearchList();
		DeckBuilderService.getInstance().GetDeckList();
		loadHqs ();
		reloadDecks ();
		collection = PlayerData.Saved.cardList;
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
		if (hqs != null) {
			refreshHq ();
		}
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

	public void addOrUpdateDeckInList(DeckDTO deck){
		if (decksContainer != null) {
			bool deckFound = false;
			foreach (DeckListElement deckInstance in decksContainer.GetComponentsInChildren<DeckListElement>()) {
				if (deckInstance.ID == deck.ID) {
					deckFound = true;
					deckInstance.Init (deck.ID, null, deck.Title, 1234);
					break;
				}
			}

			if (!deckFound) {
				addDeckToList (deck);
			}	
		}
	}





	public void btnBackgroundClick(){
		Debug.Log ("bg click");
		deselectDeck ();
	}

	public void selectDeck(long? ID)
	{
		Debug.Log ("select Deck");
		if (selectedDeck != ID) {
			selectedDeck = ID;
			refreshDeckInfo (getDeckById(selectedDeck));
			hqSelector.gameObject.SetActive (false);
			deckInfo.gameObject.SetActive (true);
		}
	}

	public void deselectDeck()
	{
		Debug.Log ("deselect Deck");
		selectedDeck = null;
		deckInfo.gameObject.SetActive (false);
		hqSelector.gameObject.SetActive (true);
	}


	public void btnCreateDeckClick(){
		if (ifDeckName.text.Length > 0) {
			newDeck ();
			/*DeckBuilderService.getInstance ().SaveDeck (
				null,
				app.view.deckBuilder.ifDeckName.text,
				app.view.deckBuilder.getSelectedHq().ProtoID,
				new CardItem[] { }
			);*/
		} else {
			//TODO visualize the reason of the issue
			Debug.LogError("Deck name cant be empty");
		}
	}

	public void btnEditDeckClick(){
		editDeck (selectedDeck);
	}

	public void newDeck()
	{
		string name = ifDeckName.text;

		//TODO process error correctly
		if (name.Length == 0) {
			Debug.LogError ("Deck name cannot be empty!");
			return;
		}

		CardItem hq = getSelectedHq (); 

		currentDeck = new DeckDTO();
		currentDeck.HQ = hq.ProtoID;
		currentDeck.Title = name;

		filteredCollection = applyFilters (collection);
		collectionComponent.initCollection (filteredCollection);
		deckComponent.initDeck (currentDeck, currentDeck.Cards.Count, 1234);
		deckList.gameObject.SetActive (false);
		deckEditor.gameObject.SetActive (true);

	}

	public void editDeck(long? deckId)
	{
		currentDeck = getDeckById(deckId);
		filteredCollection = applyFilters (collection);
		collectionComponent.initCollection (filteredCollection);
		deckComponent.initDeck (currentDeck, currentDeck.Cards.Count, 1234);
		deckList.gameObject.SetActive (false);
		deckEditor.gameObject.SetActive (true);
	}

	public void addToDeck(string protoId)
	{
		Debug.Log ("addToDeck, protoId = " + protoId);

		//loop over filtered collection and find by proto
		for(int i = 0 ; i < filteredCollection.Count ; i++){
			if(filteredCollection[i].ProtoID == protoId){
				//remove from filtered collection or update quantity
				if (filteredCollection [i].Count > 1) {
					filteredCollection [i].Count--;
				} else {
					filteredCollection.RemoveAt (i);
					break;
				}				
			}
		}
		//loop over currentDeck.cards
		//add to currentDeck.cards or update quantity
		bool foundInDeck = false;
		for(int i = 0 ; i < currentDeck.Cards.Count ; i++){
			if(currentDeck.Cards[i].ProtoID == protoId){
				foundInDeck = true;
				if(currentDeck.Cards[i].Count < 3){
					currentDeck.Cards [i].Count++;	
				}
				break;
			}
		}
		if (!foundInDeck) {
			DeckDTO.DeckCard card = new DeckDTO.DeckCard ();
			card.Count = 1;
			card.ProtoID = protoId;
			currentDeck.Cards.Add (card);
		}

		Debug.Log ("collection :" + collection);
		Debug.Log ("filteredCollection :" + filteredCollection);
		Debug.Log ("deck :" + currentDeck);

		collectionComponent.initCollection (collection);
		deckComponent.initDeck (currentDeck, currentDeck.Cards.Count, 1234);
	}

	public void removeFromDeck(string protoId)
	{
		Debug.Log ("removeFromDeck, protoId = " + protoId);

		//loop over currentDeck.cards
		//remove to currentDeck.cards or update quantity
		for(int i = 0 ; i < currentDeck.Cards.Count ; i++){
			if(currentDeck.Cards[i].ProtoID == protoId){
				if (currentDeck.Cards [i].Count > 1) {
					currentDeck.Cards [i].Count--;	
				} else {
					currentDeck.Cards.RemoveAt (i);
				}
				break;
			}
		}


		//loop over filtered collection and find by proto
		bool foundInCollection = false;
		for(int i = 0 ; i < filteredCollection.Count ; i++){
			if(filteredCollection[i].ProtoID == protoId){
				//remove from filtered collection or update quantity
				foundInCollection = true;
				filteredCollection [i].Count++;
				break;
			}
		}

		if (!foundInCollection) {
			CardItem card = new CardItem (protoId, 1);
			filteredCollection.Add (card);
		}


		Debug.Log ("collection :" + collection);
		Debug.Log ("filteredCollection :" + filteredCollection);
		Debug.Log ("deck :" + currentDeck);

		collectionComponent.initCollection (collection);
		deckComponent.initDeck (currentDeck, currentDeck.Cards.Count, 1234);
	}



	public List<CardItem> applyFilters(List<CardItem> collection){
		//TODO implement filters
		return collection;
	}



	public void btnSaveDeck(){
		DeckBuilderService.getInstance ().SaveDeck(currentDeck);
	}

	public void backToSavedDeck(long? ID)
	{
		Debug.Log ("backToSavedDeck");

		selectedDeck = ID;
		refreshDeckInfo (getDeckById(selectedDeck));

		deckEditor.gameObject.SetActive (false);
		deckList.gameObject.SetActive (true);

	}

}
