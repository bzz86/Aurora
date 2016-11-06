using System;
using Sun.DTO.Entities;
using System.Collections.Generic;
using System.Collections;
using Sun.CardProtos;

namespace Aurora.Networking.Converters
{
	public class DeckConverter
	{

		private CardProtosRepository protoRepository = new CardProtosRepository();

		public DeckConverter ()
		{
		}

	
		public static List<DeckDTO.DeckCard> getDeckCardsFromItems(CardItem[] items){
			List<DeckDTO.DeckCard> cardList = new List<DeckDTO.DeckCard>();
			foreach (CardItem item in items){
				cardList.Add (getDeckCardFromItem(item));
			}
			return cardList;
		}

		public static List<CardItem> getCardItemsFromDeckCards(List<DeckDTO.DeckCard> deckCards){
			List<CardItem> itemList = new List<CardItem>();
			foreach (DeckDTO.DeckCard deckCard in deckCards){
				itemList.Add (getItemFromDeckCard(deckCard));
			}
			return itemList;
		}


		public static CardItem getItemFromDeckCard(DeckDTO.DeckCard card){
			return new CardItem (card.ProtoID, card.Count);
		}

		public static DeckDTO.DeckCard getDeckCardFromItem(CardItem item){
			DeckDTO.DeckCard card = new DeckDTO.DeckCard ();
			card.ProtoID = item.ProtoID;
			card.Count = item.Count;
			return card;
		}

		/*public static Deck getDeckFromDeckDTO(DeckDTO deckDto){
			return new Deck (
				deckDto.ID,
				deckDto.Title,
				deckDto.HQ,
				getCardItemsFromDeckCards(deckDto.Cards)
			);
		}*/

	}
}

